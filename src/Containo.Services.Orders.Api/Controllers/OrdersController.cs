using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Containo.Services.Orders.Api.Contracts;
using Containo.Services.Orders.Api.Contracts.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Containo.Services.Orders.Api.Controllers
{
    [Route(template: "api/orders")]
    public class OrdersController : Controller
    {
        private static readonly Dictionary<string, OrderRequest> orders = new Dictionary<string, OrderRequest>();

        /// <summary>
        ///     Provides details about an order that was made
        /// </summary>
        /// <param name="confirmationId">Id of the confirmation when the order was made</param>
        [Route(template: "{confirmationId}")]
        [HttpGet]
        [SwaggerResponse((int) HttpStatusCode.OK, description: "Information about the order")]
        [SwaggerResponse((int) HttpStatusCode.NotFound, description: "Order was not found")]
        public IActionResult Get(string confirmationId)
        {
            if (orders.ContainsKey(confirmationId))
            {
                var order = orders[confirmationId];
                return Ok(order);
            }

            return NotFound();
        }

        /// <summary>
        ///     Provides details about an order that was made
        /// </summary>
        [HttpPost]
        [SwaggerResponse((int) HttpStatusCode.Created, description: "Information about the order", type: typeof(OrderConfirmation))]
        public async Task<IActionResult> Post([FromBody] OrderRequest orderRequest)
        {
            var confirmationId = Guid.NewGuid().ToString();
            var orderConfirmation = new OrderConfirmation
            {
                ConfirmationId = confirmationId,
                Order = orderRequest
            };

            await QueueOrderAsync(confirmationId, orderRequest);

            orders.Add(confirmationId, orderRequest);

            return Created($"/orders/{orderConfirmation.ConfirmationId}", orderConfirmation);
        }

        private async Task QueueOrderAsync(string confirmationId, OrderRequest order)
        {
            var orderMessage = new OrderMessage
            {
                ConfirmationId = confirmationId,
                Amount = order.Amount,
                CustomerName = order.CustomerName,
                ProductId = order.ProductId
            };

            var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(orderMessage)))
            {
                ContentType = "application/json",
                CorrelationId = Guid.NewGuid().ToString()
            };

            var connectionString = Environment.GetEnvironmentVariable(variable: "Orders_ConnectionString");
            var queueName = Environment.GetEnvironmentVariable(variable: "Orders_Queue_Name");
            var queueClient = new QueueClient(connectionString, queueName);
            await queueClient.SendAsync(message);
        }
    }
}