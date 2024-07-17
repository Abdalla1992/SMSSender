using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smsSender.Core.IAppService;
using smsSender.Data.Infrastructure.Dto;

namespace smsSender.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderAppService ProviderAppService;

        public ProviderController(IProviderAppService providerAppService)
        {
            ProviderAppService = providerAppService;
        }

        #region Actions
        [HttpPost]
        public async Task<IActionResult> Create(AddProviderDto provider)
        {
            return Ok(await ProviderAppService.Create(provider));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string providerNameSearch)
        {
            return Ok(await ProviderAppService.GetAll(providerNameSearch));
        }
        #endregion
    }
}
