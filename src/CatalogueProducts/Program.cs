using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.IO;
using Microsoft.Extensions.Configuration;

using CatalogueProducts.Drupal;

namespace CatalogueProducts
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var drupalConfig = new DrupalConfig();
            Configuration = builder.Build();

            Configuration.GetSection("DrupalConfig").Bind(drupalConfig);

            DrupalModelMapper.InitializeMapper();

            using (var httpClient = CreateDrupalClient(drupalConfig))
            {
                var drupalGateway = new DrupalCatalogueGateway(httpClient);

                var catalogue = drupalGateway.RetrieveCatalogue().ToList();
            }
        }

        private static HttpClient CreateDrupalClient(DrupalConfig drupalConfig)
        {
            var handler = new HttpClientHandler
            {
                Credentials = new NetworkCredential(drupalConfig.Username, drupalConfig.Password)
            };
            return new HttpClient(handler) { BaseAddress = new Uri(drupalConfig.BaseUrl) };
        }
    }

    public class DrupalConfig
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string BaseUrl { get; set; }
    }
}