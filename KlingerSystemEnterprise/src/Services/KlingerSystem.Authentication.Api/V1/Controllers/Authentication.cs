using KlingerSystem.Api.Controllers;
using KlingerSystem.Authentication.Api.Extensions;
using KlingerSystem.Authentication.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace KlingerSystem.Authentication.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:ApiVersion}/[controller]")]
    public class Authentication : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly GeneretorToken _token;
        private readonly IEmailSender _emailSender;
        public Authentication(SignInManager<IdentityUser> signInManager, 
                                GeneretorToken token, 
                                UserManager<IdentityUser> userManager, 
                                IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _token = token;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                //var clientResult = await RegisterClient(userRegister);

                //if (!clientResult.ValidationResult.IsValid)
                //{
                //    await _userManager.DeleteAsync(user);
                //    return CustomResponse(clientResult.ValidationResult);
                //}
                Url.Content("~/");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = new Uri($"https://localhost:44387/email-verification?userId={user.Id}&code={code}").ToString();

                await _emailSender.SendEmailAsync(userRegister.Email, "Confirmar seu e-mail",
                    $"Por favor, confirme sua conta <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui</a>.");

                return CustomResponse();
            }
            foreach (var item in result.Errors)
            {
                AddErros(item.Description);
            }
            return CustomResponse();
        }

        [AllowAnonymous]
        [HttpPost("FirstAccess")]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);
                        
            if (result.Succeeded)
            {
                return CustomResponse(await _token.TokenJwt(userLogin.Email));
            }
            else if (result.IsLockedOut)
            {
                AddErros("Usuario bloqueado por tentativas inválidas.");
            }
            else
            {
                AddErros("Usuario ou senha inválidos.");
            }
            return CustomResponse();
        }
    }
}
