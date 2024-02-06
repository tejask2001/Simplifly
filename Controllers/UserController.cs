﻿using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models.DTOs;
using Simplifly.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Simplifly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<LoginUserDTO> RegisterCustomer(RegisterCustomerUserDTO user)
        {
            var result = await _userService.RegisterCustomer(user);
            return result;
        }
        [Route("RegisterFlightOwner")]
        [HttpPost]
        public async Task<LoginUserDTO> RegisterFlightOwner(RegisterFlightOwnerUserDTO user)
        {
            var result = await _userService.RegisterFlightOwner(user);
            return result;
        }
        [Route("RegisterAdmin")]
        [HttpPost]
        public async Task<LoginUserDTO> RegisterAdmin(RegisterAdminUserDTO user)
        {
            var result = await _userService.RegisterAdmin(user);
            return result;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<LoginUserDTO>> Login(LoginUserDTO user)
        {
            try
            {
                var result = await _userService.Login(user);
                return Ok(result);
            }
            catch (InvlidUuserException iuse)
            {
                _logger.LogCritical(iuse.Message);
                return Unauthorized("Invalid username or password");
            }

        }
    }
}