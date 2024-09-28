using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Cursus_API.Helper
{
    public class SimpleEnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Enum != null && context.Type.IsEnum)
            {
                schema.Enum.Clear();
                foreach (var value in Enum.GetValues(context.Type))
                {
                    schema.Enum.Add(new OpenApiString(value.ToString()));
                }
            }
        }
    }
}
  