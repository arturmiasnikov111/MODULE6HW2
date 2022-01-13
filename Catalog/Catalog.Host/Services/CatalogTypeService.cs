using System.Threading.Tasks;
using Catalog.Host.Data;
using Catalog.Host.Repositories;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Catalog.Host.Services
{
    public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
    {
        private readonly ICatalogTypeRepository _catalogTypeRepository;

        public CatalogTypeService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogTypeRepository catalogTypeRepository)
            : base(dbContextWrapper, logger)
        {
            _catalogTypeRepository = catalogTypeRepository;
        }

        public Task<int> CreateType(string typeName)
        {
            return ExecuteSafe(() => _catalogTypeRepository.Add(typeName));
        }

        public Task<int> UpdateType(int id, string typeName)
        {
            return ExecuteSafe(() => _catalogTypeRepository.Update(id, typeName));
        }

        public Task<int> DeleteType(int id)
        {
            return ExecuteSafe(() => _catalogTypeRepository.Delete(id));
        }
    }
}