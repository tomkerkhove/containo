using System;
using System.Net.Http;
using System.Threading.Tasks;
using Containo.Services.Orders.Api.Contracts.v1;
using Flurl;

namespace Containo.Services.Orders.Api.Validation
{
    internal class OrderValidationService
    {
        public async Task<bool> ValidateAsync(OrderRequest orderRequest)
        {
            var validationBaseUrl = Environment.GetEnvironmentVariable("Services_Validation_Url");
            var validationEndpoint = validationBaseUrl.AppendPathSegment("api")
                .AppendPathSegment("orders")
                .AppendPathSegment("validation")
                .SetQueryParam("amount", orderRequest.Amount);
            
            var httpClient = new HttpClient();
            var validationResponse = await httpClient.GetAsync(validationEndpoint.ToString());
            return validationResponse.IsSuccessStatusCode;
        }
    }
}