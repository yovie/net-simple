using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NVTemplate.Core.Configuration;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCommonOpenApi(this IApplicationBuilder app)
        {
            var conf = app.ApplicationServices.GetService<IOptions<BackendOptions>>();

            if (conf?.Value.EnableSwagger ?? false)
            {
                app.UseOpenApi();
                app.UseSwaggerUi3(configure =>
                {
                    configure.DocExpansion = "list";
                    configure.OAuth2Client = app.ApplicationServices.GetRequiredService<IOptions<NSwag.AspNetCore.OAuth2ClientSettings>>().Value;
                });
            }

            return app;
        }
    }
}
