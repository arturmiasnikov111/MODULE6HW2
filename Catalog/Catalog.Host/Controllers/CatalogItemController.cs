using System.Net;
using System.Threading.Tasks;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
    public class CatalogItemController : ControllerBase
    {
        private readonly ILogger<CatalogItemController> _logger;
        private readonly ICatalogItemService _catalogItemService;

        public CatalogItemController(
            ILogger<CatalogItemController> logger,
            ICatalogItemService catalogItemService)
        {
            _logger = logger;
            _catalogItemService = catalogItemService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddItemResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateProduct(CreateProductRequest request)
        {
            var result = await _catalogItemService.CreateProductAsync(request.Name, request.Description, request.Price, request.AvailableStock, request.CatalogBrandId, request.CatalogTypeId, request.PictureFileName);

            return Ok(new AddItemResponse<int>() { Id = result });
        }

        [HttpPost]
        [ProducesResponseType(typeof(DeleteItemResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteProduct(DeleteProductRequest request)
        {
            var result = await _catalogItemService.DeleteProductAsync(request.Id);

            return Ok(new DeleteItemResponse<int>() { Id = result });
        }

        [HttpPost]
        [ProducesResponseType(typeof(UpdateItemResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateProduct(UpdateProductRequest request)
        {
            var result = await _catalogItemService.UpdateProductAsync(request.Id, request.Name, request.Description, request.Price, request.AvailableStock, request.CatalogBrandId, request.CatalogTypeId, request.PictureFileName);

            return Ok(new UpdateItemResponse<int>() { Id = result });
        }
    }
}