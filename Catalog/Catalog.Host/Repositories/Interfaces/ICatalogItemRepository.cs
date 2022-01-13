using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogItemRepository
    {
        Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize);
        Task<int> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
        Task<int> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
        Task<int> Delete(int id);
        Task<CatalogItem> GetByIdAsync(int id);
        Task<IEnumerable<CatalogItem>> GetByBrandAsync(string brand);
        Task<IEnumerable<CatalogItem>> GetByTypeAsync(string type);
        Task<IEnumerable<CatalogBrand>> GetBrandsAsync();
        Task<IEnumerable<CatalogType>> GetTypesAsync();
    }
}