using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class RestApiServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Rest API services.
        /// </summary>
        internal static IServiceCollection AddRestApi(this IServiceCollection services)
        {
            services
                .AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                })
                .AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VV";
                    options.SubstituteApiVersionInUrl = true;
                })
                .AddControllers(options =>
                {
                    options.Filters.Add(new ResponseCacheAttribute { Location = ResponseCacheLocation.None, NoStore = true });
                    options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.Converters.Add(new JsonTimeSpanConverter());
                    options.JsonSerializerOptions.Converters.Add(new JsonNullableTimeSpanConverter());
                });

            return services;
        }
    }
}
