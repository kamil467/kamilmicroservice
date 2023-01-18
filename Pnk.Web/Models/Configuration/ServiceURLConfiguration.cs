

using System.Text.Json.Serialization;

namespace Pnk.Web.Models.Configuration
{
    public class ServiceURLConfiguration
    {
        public const string ServiceURLSectionName = "ServiceUrls";

        [JsonPropertyName("ProductAPIBaseUrl")]
        public string ProductAPIBaseUrl { get; set; }

        [JsonPropertyName("CreateProduct")]
        public string CreateProduct { get; set; }

        [JsonPropertyName("UpdateProduct")]
        public string UpdateProduct { get; set; }

        [JsonPropertyName("DeleteProduct")]
        public string DeleteProduct { get; set; }

        [JsonPropertyName("ProductList")]
        public string ProductList { get; set; }

        [JsonPropertyName("GetProductById")]
        public string GetProductById { get; set; }
    }
}
