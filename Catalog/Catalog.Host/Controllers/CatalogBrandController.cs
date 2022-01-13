using Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
    public class CatalogBrandController : ControllerBase
    {
        private readonly ILogger<CatalogBrandController> _logger;

        public CatalogBrandController(ILogger<CatalogBrandController> logger)
        {
            _logger = logger;
        }
    }
}