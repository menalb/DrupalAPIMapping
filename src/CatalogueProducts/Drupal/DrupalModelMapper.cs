using AutoMapper;
using System;
using System.Linq;

namespace CatalogueProducts.Drupal
{
    public static class DrupalModelMapper
    {
        public static void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Catalogue, CatalogueProducts.Catalogue>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id.Single().Value))
                .ForMember(dest => dest.Name, m => m.MapFrom(src => src.Name.Single().Value))
                .ForMember(dest => dest.LastUpdated, m => m.MapFrom(src => ParseFromUnixTimeSeconds(src.LastUpdated.Single().Value)))
                .ForMember(dest => dest.Type, m => m.MapFrom(src => src.Type.Single().Label));

                cfg.CreateMap<FieldProduct, Product>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id))
                .ForMember(dest => dest.Seasons, m => m.MapFrom(src => src.Target.Seasons.Select(season => new Season { Name = season.Name })));
            });
        }

        public static void ResetMapper()
        {
            Mapper.Reset();
        }

        private static DateTime ParseFromUnixTimeSeconds(long unixValue)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixValue).DateTime;
        }
        public static CatalogueProducts.Catalogue MapCatalogue(Catalogue drupalCatalogue)
        {
            return Mapper.Map<CatalogueProducts.Catalogue>(drupalCatalogue);
        }
        public static Product MapProduct(FieldProduct drupalProduct)
        {
            return Mapper.Map<Product>(drupalProduct);
        }
    }
}