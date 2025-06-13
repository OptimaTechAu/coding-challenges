using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class TextPlainRequestFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.HttpMethod == "POST")
        {
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["text/plain"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "string"
                        }
                    }
                }
            };
        }
    }
}
