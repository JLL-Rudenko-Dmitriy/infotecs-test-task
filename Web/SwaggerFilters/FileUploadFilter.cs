using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Http;

public class FileUploadFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!context.MethodInfo
                .GetParameters()
                .Any(p => p.ParameterType == typeof(IFormFile)))
            return;
        
        operation.Parameters?.Clear();

        operation.RequestBody = new OpenApiRequestBody
        {
            Content =
            {
                ["multipart/form-data"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type       = "object",
                        Properties =
                        {
                            ["file"] = new OpenApiSchema
                            {
                                Type        = "string",
                                Format      = "binary",
                                Description = "Upload CSV file"
                            }
                        },
                        Required = new HashSet<string> { "file" }
                    }
                }
            }
        };
    }
}
