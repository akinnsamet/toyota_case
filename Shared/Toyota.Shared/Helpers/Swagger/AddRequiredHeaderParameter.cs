using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Toyota.Shared.Helpers.Swagger
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            //operation.Parameters.Add(new OpenApiParameter
            //{
            //    Name = "device-id",
            //    In = ParameterLocation.Header,
            //    Required = false,
            //    Schema = new OpenApiSchema
            //    {
            //        Type = "string"
            //    }
            //});

            //operation.Parameters.Add(new OpenApiParameter
            //{
            //    Name = "user-agent",
            //    In = ParameterLocation.Header,
            //    Required = false,
            //    Schema = new OpenApiSchema
            //    {
            //        Type = "string"
            //    }
            //});
        }
    }
}
