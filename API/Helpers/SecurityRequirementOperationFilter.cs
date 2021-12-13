using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Helpers
{
    public class SecurityRequirementOperationFilter : IOperationFilter
    {
        private readonly bool _includeUnauthorizedAndForbiddenResponses;
        private readonly string _securitySchemeName;

        public SecurityRequirementOperationFilter(string securitySchemeName, bool includeUnauthorizedAndForbiddenResponses = true)
        {
            _includeUnauthorizedAndForbiddenResponses = includeUnauthorizedAndForbiddenResponses;
            _securitySchemeName = securitySchemeName;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (GetAttributes<AllowAnonymousAttribute>(context).Any())
                return;

            var attributes = GetAttributes<AuthorizeAttribute>(context);

            if (!attributes.Any())
                return;

            if (_includeUnauthorizedAndForbiddenResponses)
            {
                var key = StatusCodes.Status401Unauthorized.ToString();

                if (!operation.Responses.ContainsKey(key))
                    operation.Responses.Add(key, new OpenApiResponse { Description = "Unauthorized" });

                key = StatusCodes.Status403Forbidden.ToString();

                if (!operation.Responses.ContainsKey(key))
                    operation.Responses.Add(key, new OpenApiResponse { Description = "Forbidden" });
            }

            var policies = attributes.Where(a => !string.IsNullOrEmpty(a.Policy)).Select(a => a.Policy)
                ?? Enumerable.Empty<string>();

            operation.Security.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = _securitySchemeName,
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        policies.ToList()
                    }
                }
            );
        }

        private IEnumerable<T> GetAttributes<T>(OperationFilterContext context) where T : Attribute
        {
            var controllerAttributes = context.MethodInfo.DeclaringType?.GetTypeInfo().GetCustomAttributes<T>();
            var actionAttributes = context.MethodInfo.GetCustomAttributes<T>();

            return controllerAttributes?.Union(actionAttributes) ?? actionAttributes;
        }
    }
}