using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Catalog.Host.Services
{
    public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
    {
        private readonly ICatalogItemRepository _catalogItemRepository;
        private readonly IMapper _mapper;

        public CatalogService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogItemRepository catalogItemRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogItemRepository = catalogItemRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsAsync(int pageSize, int pageIndex)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _catalogItemRepository.GetByPageAsync(pageIndex, pageSize);
                return new PaginatedItemsResponse<CatalogItemDto>()
                {
                    Count = result.TotalCount,
                    Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            });
        }

        public async Task<CatalogItemDto> GetByIdAsync(int id)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _catalogItemRepository.GetByIdAsync(id);

                return _mapper.Map<CatalogItemDto>(result);
            });
        }

        public async Task<IEnumerable<CatalogItemDto>> GetByBrandAsync(string brand)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _catalogItemRepository.GetByBrandAsync(brand);

                return result.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList();
            });
        }

        public async Task<IEnumerable<CatalogItemDto>> GetByTypeAsync(string type)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _catalogItemRepository.GetByTypeAsync(type);

                return result.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList();
            });
        }

        public async Task<IEnumerable<CatalogBrandDto>> GetBrandsAsync()
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _catalogItemRepository.GetBrandsAsync();

                return result.Select(s => _mapper.Map<CatalogBrandDto>(s)).ToList();
            });
        }

        public async Task<IEnumerable<CatalogTypeDto>> GetTypesAsync()
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _catalogItemRepository.GetTypesAsync();

                return result.Select(s => _mapper.Map<CatalogTypeDto>(s)).ToList();
            });
        }
    }
}