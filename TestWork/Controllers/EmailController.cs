using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWork.Models;
using TestWork.Reposirory;
using TestWork.Services;

namespace TestWork.Controllers
{
    /// <summary>
    /// Mail message Controller
    /// </summary>
    [ApiController]
    [Route("api/")]
    public class EmailController : Controller
    {
        private readonly IEmailServices _emailService;
        private readonly IEmailRepository _emailRepository;
        protected Response _response;
        public EmailController(IEmailServices emailServices, IEmailRepository emailRepository)
        {
            _emailService = emailServices;
            _emailRepository = emailRepository;
            this._response = new Response();
        }

        /// <summary>
        /// Get request to received all messages
        /// </summary>
        /// <returns>The list messages</returns>

        [HttpGet("mails")]
        public async Task<IActionResult> Mails()
        {
            try
            {
                IEnumerable<EmailLog> EmailLogs = await _emailRepository.GetAllLogs();
                _response.Result = EmailLogs;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return Json(_response);
        }


        /// <summary>
        /// POST request for a new message
        /// </summary>
        /// <param name="message"> This is JSON object </param>
        /// <returns>Response model JSON with information about request execution</returns>
        /// 
        [HttpPost("mails")]
        public async Task<IActionResult> Mails(Message message)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _emailService.SendAndLogEmail(message);
                    _response.Result = Ok();
                }
                catch (Exception ex)
                {
                    _response.Result = StatusCode(500, ex);
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { ex.ToString() };
                } 
            }
            return Json(_response);
        }
    }
}
