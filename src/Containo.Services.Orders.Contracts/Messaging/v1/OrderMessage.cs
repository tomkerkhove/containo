namespace Containo.Services.Orders.Api.Contracts
{
    public class OrderMessage
    {
        public int Amount { get; set; }
        public string ConfirmationId { get; set; }
        public string CustomerName { get; set; }
        public int ProductId { get; set; }
    }
}