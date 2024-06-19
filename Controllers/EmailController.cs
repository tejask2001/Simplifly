using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simplifly.Interfaces;
using Simplifly.Models.DTO_s;
using Simplifly.Services;

namespace Simplifly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task<ActionResult> SendEmail(EmailDTO emailDTO)
        {
            try
            {
                await _emailSender.SendEmailAsync(emailDTO.Email, emailDTO.Subject, emailDTO.Message);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            
        }
    }
}
