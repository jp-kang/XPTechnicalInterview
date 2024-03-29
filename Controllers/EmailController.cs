﻿using Microsoft.AspNetCore.Mvc;
using XPTechnicalInterview.Domain;
using XPTechnicalInterview.Services;

namespace XPTechnicalInterview.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly EmailService emailService;
        public EmailController(EmailService _EmailService)
        {
            emailService = _EmailService;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)] // Expected response type for success
        public IActionResult SendEmail(Email email)
        {
            emailService.SendEmail(email);
            return Ok();//200
        }
    }
}
