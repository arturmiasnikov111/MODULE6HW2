using System.Threading.Tasks;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Catalog.Host.Services
{
    public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
    {
        private readonly ICatalogItemRepository _catalogItemRepository;

        public CatalogItemService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogItemRepository catalogItemRepository)
            : base(dbContextWrapper, logger)
        {
            _catalogItemRepository = catalogItemRepository;
        }

        public Task<int> CreateProductAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
        {
            return ExecuteSafe(() => _catalogItemRepository.Add(name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName));
        }

        public Task<int> UpdateProductAsync(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
        {
            return ExecuteSafe(() => _catalogItemRepository.Update(id, name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName));
        }

        public Task<int> DeleteProductAsync(int id)
        {
            return ExecuteSafe(() => _catalogItemRepository.Delete(id));
        }
    }
}