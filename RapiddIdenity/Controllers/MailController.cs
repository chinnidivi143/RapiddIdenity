using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RapiddIdenity.Services;
using System.Net;
using System.Net.Mail;

namespace RapiddIdenity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MailController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("send")]
        public async Task<ActionResult> Send(MailDetails mailDetails)
        {
            try
            {
                SmtpClient smtpClient = new(_configuration.GetValue<string>("EmailConfig:SmtpServer"),
                                                  _configuration.GetValue<Int32>("EmailConfig:port"))
                {
                    Credentials = new NetworkCredential(_configuration.GetValue<string>("EmailConfig:From"),
                                                       _configuration.GetValue<string>("EmailConfig:Password")),
                    EnableSsl = true
                };

                MailMessage mailMessage = new MailMessage(_configuration.GetValue<string>("EmailConfig:From"), mailDetails.To)
                {
                    Subject = mailDetails.Subject,
                    Body = mailDetails.Body,
                };

                string mailAddresses = string.Empty;
                mailDetails.CC.ForEach(c => { mailAddresses = mailAddresses.ToString() + "," + c.ToString().Trim(); }) ;
                mailMessage.CC.Add(mailAddresses.Trim().Trim(',').Trim());
                await smtpClient.SendMailAsync(mailMessage);
                return Ok(mailMessage);
                   
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.StackTrace);

            }
            finally
            {

            }
            return Ok();
        }
    }
}
