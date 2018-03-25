using System;
using System.Collections.Generic;

namespace CatalogueProducts
{
    public class Catalogue
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdated { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Season> Seasons { get; set; }
    }

    public class Season
    {       
        public string Name { get; set; }
    }
}