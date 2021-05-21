using Microsoft.OpenApi.Models;

namespace PhotoContest.Web
{
    internal class OpenApiSecurityScheme : Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string In { get; set; }
        public string Type { get; set; }
    }
}