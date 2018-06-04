using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Containo.Services.Orders.Validator.Controllers
{
    [Route(template: "api/orders/validation")]
    public class ValidationController : Controller
    {
        /// <summary>
        ///     Validates an order
        /// </summary>
        /// <param name="tenantName">Name of the tenant</param>
        /// <param name="amount">Amount of products bought</param>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Ordder was not valid")]
        public IActionResult Get(string tenantName, int amount)
        {
            return Ok();
        }
    }
}