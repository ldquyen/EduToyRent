using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

public class CustomDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var sortedPaths = swaggerDoc.Paths
            .OrderBy(p => p.Key, new PathComparer())
            .ToDictionary(p => p.Key, p => p.Value);

        swaggerDoc.Paths = new OpenApiPaths();
        foreach (var path in sortedPaths)
        {
            swaggerDoc.Paths.Add(path.Key, path.Value);
        }
    }

    private class PathComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            var xParts = x.Split('/');
            var yParts = y.Split('/');
            int minLength = Math.Min(xParts.Length, yParts.Length);

            for (int i = 0; i < minLength; i++)
            {
                int comparison = string.Compare(xParts[i], yParts[i], StringComparison.Ordinal);
                if (comparison != 0)
                {
                    return comparison;
                }
            }

            return xParts.Length.CompareTo(yParts.Length);
        }
    }
}
