using Xunit;

using CatalogueProducts.Drupal;
using System.Collections.Generic;
using System.Linq;

namespace CatalogueProducts.Tests
{
    [Collection("Mapping")]
    public class ProductModelFromDrupalMappingTests : IClassFixture<MapFixture>
    {
        [Fact]
        public void Given_a_product_it_maps_the_Id()
        {
            var productId = 123;
            var drupalProduct = ProductBuilder.Build().With_Id(productId);

            var result = DrupalModelMapper.MapProduct(drupalProduct);

            Assert.Equal(productId, result.Id);
        }

        [Fact]
        public void Given_a_product_it_maps_the_Name()
        {
            var name = "Apple";
            var drupalProduct = ProductBuilder.Build().With_Name(name);

            var result = DrupalModelMapper.MapProduct(drupalProduct);

            Assert.Equal(name, result.Name);
        }

        [Fact]
        public void Given_a_product_it_maps_the_Seasons_considering_just_the_name()
        {
            var seasons = new string[] { "spring", "summer" };
            var drupalProduct = ProductBuilder.Build().With_Seasons(seasons);

            var result = DrupalModelMapper.MapProduct(drupalProduct);

            Assert.Empty(result.Seasons.Select(season => season.Name).Except(seasons));
        }

        [Fact]
        public void Given_a_product_whe_the_seasons_are_missing_it_maps_to_empty_season_list()
        {
            var drupalProduct = ProductBuilder.Build();
            drupalProduct.Target = new TargetProduct();

            var result = DrupalModelMapper.MapProduct(drupalProduct);

            Assert.Empty(result.Seasons);
        }
    }

    internal static class ProductBuilder
    {
        public static FieldProduct Build()
        {
            return new FieldProduct
            {
                Id = 123,
                Name = "Apple",
                Target = new TargetProduct { }
            };
        }

        public static FieldProduct With_Id(this FieldProduct product, int id)
        {
            product.Id = id;
            return product;
        }

        public static FieldProduct With_Name(this FieldProduct product, string name)
        {
            product.Name = name;
            return product;
        }

        public static FieldProduct With_Seasons(this FieldProduct product, params string[] seasonsName)
        {
            var seasons = seasonsName.Select(season => new FieldTarget { Name = season });
            product.Target = new TargetProduct { Seasons = seasons };
            return product;
        }
    }
}