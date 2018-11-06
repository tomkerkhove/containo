using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Containo.Core.Api.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        ///     Add support for Open API & Develerop UI
        /// </summary>
        public static void UseOpenApiUi(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(swaggerUiOptions =>
            {
                swaggerUiOptions.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Containo API");
                swaggerUiOptions.DisplayOperationId();
                swaggerUiOptions.EnableDeepLinking();
                swaggerUiOptions.DocumentTitle = "Containo API";
                swaggerUiOptions.DocExpansion(DocExpansion.List);
                swaggerUiOptions.DisplayRequestDuration();
                swaggerUiOptions.EnableFilter();
            });
        }
    }
}
