using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalog.Host.Repositories
{
    public class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogItemRepository> _logger;

        public CatalogItemRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogItemRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize)
        {
            var totalItems = await _dbContext.CatalogItems
                .LongCountAsync();

            var itemsOnPage = await _dbContext.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
        }

        public async Task<int> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
        {
            var item = _dbContext.CatalogItems.Add(new CatalogItem()
            {
                CatalogBrandId = catalogBrandId,
                CatalogTypeId = catalogTypeId,
                Description = description,
                Name = name,
                PictureFileName = pictureFileName,
                Price = price
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<int> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
        {
            var item = _dbContext.CatalogItems.First(i => i.Id == id);

            item.Name = name;
            item.Description = description;
            item.Price = price;
            item.AvailableStock = availableStock;
            item.CatalogBrandId = catalogBrandId;
            item.CatalogTypeId = catalogTypeId;
            item.PictureFileName = pictureFileName;

            _dbContext.Update(item);

            await _dbContext.SaveChangesAsync();

            return item.Id;
        }

        public async Task<int> Delete(int id)
        {
            var item = _dbContext.CatalogItems.First(i => i.Id == id);

            _dbContext.CatalogItems.Remove(item);

            await _dbContext.SaveChangesAsync();

            return item.Id;
        }

        public async Task<CatalogItem> GetByIdAsync(int id)
        {
            var result = await _dbContext.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .FirstAsync(i => i.Id == id);

            return result;
        }

        public async Task<IEnumerable<CatalogItem>> GetByBrandAsync(string brand)
        {
            var result = await _dbContext.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .Where(i => i.CatalogBrand.Brand.ToLower() == brand.ToLower())
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<CatalogItem>> GetByTypeAsync(string type)
        {
            var result = await _dbContext.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .Where(i => i.CatalogType.Type.ToLower() == type.ToLower())
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<CatalogBrand>> GetBrandsAsync()
        {
            var result = await _dbContext.CatalogBrands
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<CatalogType>> GetTypesAsync()
        {
            var result = await _dbContext.CatalogTypes
                .ToListAsync();

            return result;
        }
    }
}