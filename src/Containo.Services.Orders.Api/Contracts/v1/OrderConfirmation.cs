namespace Containo.Services.Orders.Api.Contracts.v1
{
    public class OrderConfirmation
    {
        public string ConfirmationId { get; set; }
        public OrderRequest Order { get; set; }
    }
}