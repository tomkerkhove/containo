using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Containo.Services.Orders.QueueProcessor
{
    public abstract class MessagePump<TMessage> where TMessage : class, new()
    {
        private static readonly ManualResetEvent shutdown = new ManualResetEvent(initialState: false);

        protected abstract Task ProcessMessageAsync(string correlationId, string cycleId, TMessage message);

        protected async Task ReceiveMessagesAsync()
        {
            var connectionString = Environment.GetEnvironmentVariable(variable: "ServiceBus_ConnectionString");
            var queueName = Environment.GetEnvironmentVariable(variable: "Orders_Queue_Name");
            var queueClient = new QueueClient(connectionString, queueName);

            var messageHandlerOptions = new MessageHandlerOptions(HandleMessageProcessingExceptionAsync)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 10
            };

            queueClient.RegisterMessageHandler(async (message, cancellationToken) => await HandleNewMessageAsync(queueClient, message), messageHandlerOptions);

            shutdown.WaitOne();

            Console.WriteLine(value: "Exit command issued to container, disconnecting listener");

            await queueClient.CloseAsync();
        }

        private static Task HandleMessageProcessingExceptionAsync(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Exception occured: {exceptionReceivedEventArgs.Exception}");

            return Task.CompletedTask;
        }

        private async Task HandleNewMessageAsync(QueueClient queueClient, Message message)
        {
            var cycleId = Guid.NewGuid().ToString();
            var correlationId = message.CorrelationId;

            var rawBody = Encoding.UTF8.GetString(message.Body);
            var parsedMessage = JsonConvert.DeserializeObject<TMessage>(rawBody);

            await ProcessMessageAsync(correlationId, cycleId, parsedMessage);

            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
            Trace.WriteLine($"Message '{correlationId}' processed");
        }
    }
}