using System;

using CatalogueProducts.Drupal;

namespace CatalogueProducts.Tests
{
    public class MapFixture : IDisposable
    {
        public MapFixture()
        {
            DrupalModelMapper.InitializeMapper();
        }

        public void Dispose()
        {
            DrupalModelMapper.ResetMapper();
        }
    }
}