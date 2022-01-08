﻿using Domain.Users;
using Infra.Infra.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.Colegio.API.DTO;
using Nascimento.Software.Colegio.API.Services;

namespace Nascimento.Software.Colegio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagerController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("user-register")]
        public async Task<ActionResult> UserRegister
        ([FromServices] IUserRepository _userRepo,
        [FromBody] UserRegisterDTO userDTO,
        [FromServices] GenerateToken _tokenGenerator)
        {
            if (ModelState.IsValid)
            {
                var userExists = await _userRepo.GetUserByEmail(userDTO.Email);
                if (userExists != null) return BadRequest(new { Success = false, Error = "User already exists" });

                var user = new User()
                {
                    Name = userDTO.Name,
                    Birthdate = userDTO.Birthdate,
                    Email = userDTO.Email,
                    Phone = userDTO.Phone,
                };

                var userCreated = await _userRepo.Register(user);

                if (userCreated)
                {
                    var token = _tokenGenerator.GenerateJwtToken(user);
                    return Ok(new
                    {
                        Token = token,
                        Success = true,
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            return BadRequest(new { Success = false, Error = "Login unsuccessful" });

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("user-login")]
        public async Task<ActionResult> Login
            ([FromServices] IUserRepository _userRepo,
            [FromBody] UserLoginDTO userDTO,
            [FromServices] GenerateToken _tokenGenerator)
        {
            if (ModelState.IsValid)
            {
                var userExists = await _userRepo.GetUserByEmail(userDTO.Email);
                if (userExists != null)
                {
                    var userValid = await _userRepo.Login(new User()
                    {
                        Email = userDTO.Email,
                        Phone = userDTO.Phone,
                    });
                    if (userValid == null)
                    {
                        return BadRequest(new
                        {
                            success = false,
                            error = "Login unsuccessful",
                        });
                    }
                    var token = _tokenGenerator.GenerateJwtToken(userValid);
                    return Ok(new AuthResult()
                    {
                        Success = true,
                        Token = token,
                    });
                }
                return BadRequest(new
                {
                    success = false,
                    error = "User don't exists",
                });
            }

            return BadRequest();
        }
    }

}