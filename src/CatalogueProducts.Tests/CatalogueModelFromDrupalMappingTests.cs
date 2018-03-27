using System;
using System.Collections.Generic;
using Xunit;

using CatalogueProducts.Drupal;

namespace CatalogueProducts.Tests
{
    [Collection("Mapping")]
    public class CatalogueModelFromDrupalMappingTests : IClassFixture<MapFixture>
    {
        [Fact]
        public void Given_a_catalogue_it_maps_the_Id_in_asingle_flat_Id()
        {
            var catalogueId = 123;
            var drupalCatalogue = CatalogueBuilder.Build().With_Id(catalogueId);

            var result = DrupalModelMapper.MapCatalogue(drupalCatalogue);

            Assert.Equal(catalogueId, result.Id);
        }

        [Fact]
        public void Given_a_catalogue_When_the_Id_is_empty_it_fails()
        {
            var drupalCatalogue = CatalogueBuilder.Build();
            drupalCatalogue.Id = new List<FieldInt>();

            Assert.ThrowsAny<Exception>(() => DrupalModelMapper.MapCatalogue(drupalCatalogue));
        }

        [Fact]
        public void Given_a_catalogue_it_maps_the_Name_in_asingle_flat_Name()
        {
            var name = string.Empty;
            var drupalCatalogue = CatalogueBuilder.Build().With_Name(name);

            var result = DrupalModelMapper.MapCatalogue(drupalCatalogue);

            Assert.Equal(name, result.Name);
        }

        [Fact]
        public void Given_a_catalogue_When_the_name_is_empty_it_fails()
        {
            var drupalCatalogue = CatalogueBuilder.Build();
            drupalCatalogue.Name = new List<FieldString>();

            Assert.ThrowsAny<Exception>(() => DrupalModelMapper.MapCatalogue(drupalCatalogue));
        }

        [Fact]
        public void Given_a_catalogue_it_maps_the_Label_field_in_the_Type_in_as_the_Type_of_the_catalogue()
        {
            var type = "OnlineSpecial";
            var drupalCatalogue = CatalogueBuilder.Build().With_Type(type);

            var result = DrupalModelMapper.MapCatalogue(drupalCatalogue);

            Assert.Equal(type, result.Type);
        }

        [Fact]
        public void Given_a_catalogue_it_maps_the_LastUpdated_from_UnixTimeSeconds_to_the_currespondent_DateTime()
        {
            var dateTime = DateTime.UtcNow;
            var drupalCatalogue = CatalogueBuilder.Build().With_LastUpdateDateTime(dateTime);

            var result = DrupalModelMapper.MapCatalogue(drupalCatalogue);

            Assert.Equal(0, (int)(dateTime - result.LastUpdated).TotalSeconds);
        }
    }

    internal static class CatalogueBuilder
    {
        public static Drupal.Catalogue Build()
        {
            return new Drupal.Catalogue
            {
                Id = new List<FieldInt> { new FieldInt { Value = 123 } },
                Name = new List<FieldString> { new FieldString { Value = "Apples" } },
                LastUpdated = new List<FieldLong> { new FieldLong { Value = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds() } }
            };
        }

        public static Drupal.Catalogue With_Id(this Drupal.Catalogue catalogue, int catalogueId)
        {
            catalogue.Id = new List<FieldInt> { new FieldInt { Value = catalogueId } };

            return catalogue;
        }

        public static Drupal.Catalogue With_Name(this Drupal.Catalogue catalogue, string catalogueName)
        {
            catalogue.Name = new List<FieldString> { new FieldString { Value = catalogueName } };

            return catalogue;
        }

        public static Drupal.Catalogue With_Type(this Drupal.Catalogue catalogue, string catalogueType)
        {
            catalogue.Type = new List<FieldType> { new FieldType { Label = catalogueType } };

            return catalogue;
        }

        public static Drupal.Catalogue With_LastUpdateDateTime(this Drupal.Catalogue catalogue, DateTime lastUpdateDateTime)
        {
            catalogue.LastUpdated = new List<FieldLong> { new FieldLong { Value = ((DateTimeOffset)lastUpdateDateTime).ToUnixTimeSeconds() } };

            return catalogue;
        }
    }
}