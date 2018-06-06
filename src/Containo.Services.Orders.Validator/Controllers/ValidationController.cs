using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Containo.Services.Orders.Validator.Controllers
{
    [Route("api/orders/validation")]
    public class ValidationController : Controller
    {
        /// <summary>
        ///     Validates an order
        /// </summary>
        /// <param name="amount">Amount of products bought</param>
        [HttpGet]
        [SwaggerResponse((int) HttpStatusCode.OK)]
        [SwaggerResponse((int) HttpStatusCode.NotFound, description: "Ordder was not valid")]
        public IActionResult Get(int amount)
        {
            if (amount > 100)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}