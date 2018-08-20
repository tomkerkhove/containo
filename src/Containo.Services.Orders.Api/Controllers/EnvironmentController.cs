using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Containo.Services.Orders.Api.Controllers
{
    [Route("api")]
    public class EnvironmentController : Controller
    {
        /// <summary>
        ///     Provides information about the environment
        /// </summary>
        /// <param name="environmentVariableName">Name of the environment variable that you are interested in</param>
        [Route("environment/{environmentVariableName}")]
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Information about the environment",
            type: typeof(string))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Order was not found")]
        public IActionResult Get(string environmentVariableName)
        {
            if (string.IsNullOrWhiteSpace(environmentVariableName))
            {
                return BadRequest("No environment variable was specified");
            }

            var environmentVariable = Environment.GetEnvironmentVariable(environmentVariableName);
            if (string.IsNullOrWhiteSpace(environmentVariable))
            {
                return NotFound();
            }

            return Ok(environmentVariable);
        }
    }
}
