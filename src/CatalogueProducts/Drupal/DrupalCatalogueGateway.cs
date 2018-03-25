using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace CatalogueProducts.Drupal
{
    public class DrupalCatalogueGateway : ICatalogueGateway
    {
        private readonly HttpClient _client;

        public DrupalCatalogueGateway(HttpClient client)
        {
            _client = client;
        }

        public IEnumerable<CatalogueProducts.Catalogue> RetrieveCatalogue()
        {

            var drupalCatalogue = Retrieve();
            var catalogue = drupalCatalogue.Select(DrupalModelMapper.MapCatalogue);
            return catalogue;
        }

        private IEnumerable<Catalogue> Retrieve()
        {
            var result = _client.GetAsync("roadmap-data").Result;

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IList<Catalogue>>(result.Content.ReadAsStringAsync().Result);
            }

            throw new Exception("Oops, something whent wrong with the retrieving of the catalogue...");
        }
    }
}