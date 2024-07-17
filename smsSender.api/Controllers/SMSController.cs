using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smsSender.Core.AppService;
using smsSender.Core.IAppService;
using smsSender.Data.Infrastructure.Dto;

namespace smsSender.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        private readonly ISMSSenderAppService SMSSenderAppService;

        public SMSController(ISMSSenderAppService sMSSenderAppService)
        {
            SMSSenderAppService = sMSSenderAppService;
        }

        #region Actions
        [HttpPost]
        public async Task<IActionResult> Send(SMSDto sms)
        {
            return Ok(await SMSSenderAppService.Send(sms));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string PhoneNumberSearch)
        {
            return Ok(await SMSSenderAppService.GetAll(PhoneNumberSearch));
        }
        #endregion
    }
}
