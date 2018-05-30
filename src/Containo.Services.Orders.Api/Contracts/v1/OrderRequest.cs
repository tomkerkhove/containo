namespace Containo.Services.Orders.Api.Contracts.v1
{
    public class OrderRequest
    {
        public int Amount { get; set; }
        public string CustomerName { get; set; }
        public int ProductId { get; set; }
    }
}