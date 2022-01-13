using System.Net;
using System.Threading.Tasks;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.TypeRequests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
    public class CatalogTypeController : ControllerBase
    {
        private readonly ILogger<CatalogTypeController> _logger;
        private readonly ICatalogTypeService _catalogTypeService;

        public CatalogTypeController(ILogger<CatalogTypeController> logger, ICatalogTypeService catalogTypeService)
        {
            _logger = logger;
            _catalogTypeService = catalogTypeService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddItemResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddType(CreateTypeRequest request)
        {
            var result = await _catalogTypeService.CreateType(request.TypeName);

            return Ok(new AddItemResponse<int> { Id = result });
        }

        [HttpPost]
        [ProducesResponseType(typeof(UpdateItemResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateType(UpdateTypeRequest request)
        {
            var result = await _catalogTypeService.UpdateType(request.Id, request.TypeName);

            return Ok(new UpdateItemResponse<int>() { Id = result });
        }

        [HttpPost]
        [ProducesResponseType(typeof(DeleteItemResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteType(DeleteTypeRequest request)
        {
            var result = await _catalogTypeService.DeleteType(request.Id);

            return Ok(new DeleteItemResponse<int>() { Id = result });
        }
    }
}