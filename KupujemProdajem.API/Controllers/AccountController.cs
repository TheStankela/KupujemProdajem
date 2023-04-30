using KupujemProdajem.API.Models;
using KupujemProdajem.Domain.Models;
using KupujemProdajem.Infrastructure.Context;
using KupujemProdajem.Infrastructure.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;

        public AccountController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(userLoginModel.Email);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, userLoginModel.Password);

                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Ok("Login successfull.");
                    }
                }
                return BadRequest("Wrong credentials");
            }
            return BadRequest(userLoginModel);
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterModel userRegisterModel)
        {
            if (!ModelState.IsValid) { return BadRequest(userRegisterModel); }

            var user = await _userManager.FindByEmailAsync(userRegisterModel.Email);

            if (user != null)
            {
                return BadRequest("Email already used.");
            }
            var newUser = new UserModel()
            {
                Email = userRegisterModel.Email,
                UserName = userRegisterModel.UserName
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, userRegisterModel.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return Ok("Register successfull.");
            }
            return Ok();

        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Signed out.");
        }
    }
}

