using System.Threading.Tasks;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogItemService
    {
        Task<int> CreateProductAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
        Task<int> UpdateProductAsync(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
        Task<int> DeleteProductAsync(int id);
    }
}