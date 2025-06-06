﻿using Auth.Api.Models.Dto;
using Auth.Api.Services;
using Dine.MessageBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Auth.Api.Controllers
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMessageBus _messageBus;
        private readonly IConfiguration _confiuration; 

        protected ResponseDto _response;

        public AuthController(IAuthService authService, IMessageBus messageBus, IConfiguration confiuration)
        {
            _authService = authService;
            _messageBus = messageBus;
            _confiuration = confiuration;
            _response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if(!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            await _messageBus.PublishMessage(model.Email, 
                                            _confiuration.GetValue<string>("TopicAndQueueNames:RegisterUserQueue"));

            return Ok(_response);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);

            if(loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "UserName pr Password is incorrect";
                return BadRequest(_response);
            }

            _response.Result = loginResponse;

            return Ok(_response);
            
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assingRleSuccess = await _authService.AssignRole(model.Email, model.RoleName.ToUpper());

            if (!assingRleSuccess)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encounter";
                return BadRequest(_response);
            }


            return Ok(_response);


        }

    }
}
