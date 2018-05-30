using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage.Table;

namespace Containo.Services.Orders.Contracts.Storage
{
    public class OrderRecord : TableEntity
    {
        public OrderRecord()
        {

        }

        public OrderRecord(string customerName, string confirmationId) : base(customerName, confirmationId)
        {
            CustomerName = customerName;
            ConfirmationId = confirmationId;
        }

        public int Amount { get; set; }
        public string ConfirmationId { get; set; }
        public string CustomerName { get; set; }
        public int ProductId { get; set; }
    }
}
