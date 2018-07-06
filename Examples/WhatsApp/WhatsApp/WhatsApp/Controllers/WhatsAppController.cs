using Microsoft.AspNetCore.Mvc;
using WhatsApp.Model;
using WhatsApp.Repositories;
using WhatsApp.Services;

namespace WhatsApp.Controllers
{
    [ApiController]
    [Route("api/whatsapp")]
    public class WhatsAppController : ControllerBase
    {
        private readonly IChatService userService;

        public WhatsAppController(IChatService userService)
        {
            this.userService = userService ?? throw new System.ArgumentNullException(nameof(userService));
        }

        [Route("users/me")]
        public ActionResult<WhatsAppResponse> Me()
        {
            return Ok(userService.GetMe());
        }
    }
}
