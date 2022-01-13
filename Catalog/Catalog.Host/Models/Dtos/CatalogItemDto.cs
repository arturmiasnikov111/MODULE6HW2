namespace Catalog.Host.Models.Dtos
{
#pragma warning disable CS8618

    public class CatalogItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public CatalogTypeDto CatalogType { get; set; }

        public CatalogBrandDto CatalogBrand { get; set; }

        public int AvailableStock { get; set; }
    }
}