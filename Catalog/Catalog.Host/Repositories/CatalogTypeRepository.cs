using System.Linq;
using System.Threading.Tasks;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogTypeRepository> _logger;

        public CatalogTypeRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogTypeRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<int> Add(string typeName)
        {
            var item = _dbContext.CatalogTypes.Add(new CatalogType()
            {
                Type = typeName
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<int> Update(int id, string typeName)
        {
            var item = _dbContext.CatalogTypes.First(i => i.Id == id);

            item.Type = typeName;

            _dbContext.CatalogTypes.Update(item);
            await _dbContext.SaveChangesAsync();

            return item.Id;
        }

        public async Task<int> Delete(int id)
        {
            var item = _dbContext.CatalogTypes.First(i => i.Id == id);

            _dbContext.CatalogTypes.Remove(item);

            await _dbContext.SaveChangesAsync();

            return item.Id;
        }
    }
}