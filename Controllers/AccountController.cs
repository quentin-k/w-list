using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using w_list.ViewModels;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace w_list.Controllers
{
    public class AccountController : Controller
    {
        //these privates need to be fixed
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;
        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Username, Email = model.Email};
                var result = await userManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confimationLink = Url.Action("ConfirmEmail", "Account",
                    new { userID = user.Id, token = token}, Request.Scheme);        
                    SmtpClient client = new SmtpClient();
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate(configuration["EmailUsernameSecret"], configuration["EmailPasswordSecret"]);
                    MimeMessage message = new MimeMessage();
                    MailboxAddress from = new MailboxAddress("w-list app", "wlistwebapp@gmail.com");
                    message.From.Add(from);
                    MailboxAddress to = new MailboxAddress(user.UserName, user.Email);
                    message.To.Add(to);
                    message.Subject = "Confirm Email";
                    BodyBuilder bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = $"To confirm your email click on the following link: {confimationLink}";
                    message.Body = bodyBuilder.ToMessageBody();

                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();
                    await signInManager.SignInAsync(user, isPersistent: false);
                    
                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
                return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.PesrsistLogin, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userID, string token)
        {
            if (userID == null || token == null)
            {
                return View("Error");
            }
            IdentityResult result;
            try
            {
                var user = await userManager.FindByIdAsync(userID);
                result = await userManager.ConfirmEmailAsync(user, token);
            }
            catch (InvalidOperationException ioe)
            {
                // ConfirmEmailAsync throws when the userId is not found.
                ViewBag.errorMessage = ioe.Message;
                return View("Error");
            }

            if (result.Succeeded)
            {
                return View();
            }

            // If we got this far, something failed.
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            ViewBag.errorMessage = "ConfirmEmail failed";
            return View("Error");
        }
        
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);
                if (user != null && await userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);

                    SmtpClient client = new SmtpClient();
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate(configuration["EmailUsernameSecret"], configuration["EmailPasswordsecret"]);

                    MimeMessage message = new MimeMessage();
                    MailboxAddress from = new MailboxAddress("w-list app", "wlistwebapp@gmail.com");
                    message.From.Add(from);
                    MailboxAddress to = new MailboxAddress(user.UserName, user.Email);
                    message.To.Add(to);
                    message.Subject = "Reset Password";
                    BodyBuilder bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = $"To reset your password, click on the link: {passwordResetLink}";
                    message.Body = bodyBuilder.ToMessageBody();

                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();

                    return View("ForgotPasswordConfirmation");
                }
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
                return View("ResetPasswordConfirmation");
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }
}