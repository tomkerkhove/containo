using System;
using System.Threading.Tasks;
using Containo.Services.Orders.Storage.Contracts.v1;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Containo.Services.Orders.Storage
{
    public class OrdersRepository
    {
        private readonly CloudTable ordersTable;

        public OrdersRepository()
        {
            var connectionString = Environment.GetEnvironmentVariable(variable: "TableStorage_ConnectionString");
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var tableStorageClient = storageAccount.CreateCloudTableClient();
            ordersTable = tableStorageClient.GetTableReference(tableName: "Orders");
        }

        /// <summary>
        ///     Get order information
        /// </summary>
        /// <param name="customerName">Name of the customer</param>
        /// <param name="confirmationId">Confirmation id for the order</param>
        public async Task<OrderRecord> GetAsync(string customerName, string confirmationId)
        {
            await ordersTable.CreateIfNotExistsAsync();

            var retrieveOperation = TableOperation.Retrieve<OrderRecord>(customerName, confirmationId);
            var retrievedResult = await ordersTable.ExecuteAsync(retrieveOperation);

            return retrievedResult?.Result as OrderRecord;
        }

        /// <summary>
        ///     Stores a new order
        /// </summary>
        /// <param name="customerName">Name of the customer</param>
        /// <param name="confirmationId">Confirmation id for the order</param>
        /// <param name="productId">Id of the product</param>
        /// <param name="amount">Amount that was ordered</param>
        public async Task StoreAsync(string customerName, string confirmationId, int productId, int amount)
        {
            await ordersTable.CreateIfNotExistsAsync();

            var orderRecord = new OrderRecord(customerName, confirmationId)
            {
                Amount = amount,
                ProductId = productId
            };

            var insertOperation = TableOperation.Insert(orderRecord);
            await ordersTable.ExecuteAsync(insertOperation);
        }
    }
}