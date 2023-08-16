using Microsoft.AspNetCore.Mvc;

namespace OtonaBookApi.Controllers
{
    [ApiController]
    public abstract class AreaControllerBase : ControllerBase
    {
        protected readonly ILogger _logger;
        public AreaControllerBase(ILogger logger)
        {
            _logger = logger;

        }
    }
}