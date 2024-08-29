using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ToDoListApp.Models;
using ToDoListApp.POCO;

namespace ToDoListApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost("api/register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            if (ModelState.IsValid)
            {
                StatusMessage statusMessage = new StatusMessage();

                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                {
                    statusMessage.Status = "Failed";
                    statusMessage.Message = "Email and password are required.";
                    return BadRequest(statusMessage);
                }
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    statusMessage.Status = "Failed";
                    statusMessage.Message = "Email is already in use.";
                    return Conflict(statusMessage);
                }
                try
                {
                    var user = new User
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        statusMessage.Status = "Success";
                        statusMessage.Message = "User registered successfully.";
                        return Ok(statusMessage);
                    }
                }
                catch (Exception ex)
                {
                    statusMessage.Status = "Failed";
                    statusMessage.Message = ex.Message;
                    return StatusCode(500, statusMessage);
                }
            }
            return BadRequest(ModelState);
        }

        //[HttpPost("api/login")]
        //public async Task<IActionResult> Login([FromBody] Login model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        //        if (result.Succeeded)
        //        {
        //            return Ok(new { message = "Login successful" });
        //        }
        //        return Unauthorized(new { message = "Invalid login attempt" });
        //    }
        //    return BadRequest(ModelState);
        //}

        //[HttpPost("api/login")]
        //public async Task<IActionResult> Login([FromBody] Login model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var user = await _userManager.FindByEmailAsync(model.Email);
        //    if (user == null)
        //    {
        //        return Unauthorized();
        //    }
        //    await _userManager.SetLockoutEnabledAsync(user, false);

        //    // Clear lockout if the user is currently locked out
        //    await _userManager.SetLockoutEndDateAsync(user, null);
        //    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        //    if (result.Succeeded)
        //    {
        //        return Ok(new { message = "Login successful" });
        //    }
        //    // Additional checks for lockout or other conditions
        //    if (result.IsLockedOut)
        //    {
        //        return Unauthorized(new { message = "User is locked out" });
        //    }
        //    if (result.IsNotAllowed)
        //    {
        //        return Unauthorized(new { message = "User is not allowed to sign in" });
        //    }
        //    return Unauthorized(new { message = "Invalid login attempt" });
        //}

        [Route("api/Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            StatusMessage statusMessage = new StatusMessage();

            if (string.IsNullOrEmpty(model.Email))
            {
                statusMessage.Status = "Failed";
                statusMessage.Message = "Enter a valid email";
                return Ok(statusMessage);
            }
            else if (string.IsNullOrEmpty(model.Password))
            {
                statusMessage.Status = "Failed";
                statusMessage.Message = "Enter a valid password";
                return Ok(statusMessage);
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    statusMessage.Status = "Failed";
                    statusMessage.Message = "Invalid email or password";
                    return Unauthorized(statusMessage);
                }
                // Disable lockout
                await _userManager.SetLockoutEnabledAsync(user, false);

                // Clear lockout if the user is currently locked out
                await _userManager.SetLockoutEndDateAsync(user, null);
                // Check if the user is locked out
                if (await _userManager.IsLockedOutAsync(user))
                {
                    statusMessage.Status = "Failed";
                    statusMessage.Message = "User is locked out";
                    return Unauthorized(statusMessage);
                }
                if (await _userManager.CheckPasswordAsync(user, model.Password))
                {
                   // var tokenString = generateToken(user.Email); // Implement this method

                    statusMessage.Status = "True";
                    statusMessage.Message = "Success";
                    statusMessage.data = new { redirectUrl = "/ToDoItems/Index" };
                    //statusMessage.data = user;
                    // statusMessage.auth_token = tokenString;
                    return Ok(statusMessage);
                }
                else
                {
                    statusMessage.Status = "Failed";
                    statusMessage.Message = "Invalid email or password";
                    return Unauthorized(statusMessage);
                }
            }
            catch (Exception ex)
            {
                statusMessage.Status = "Failed";
                statusMessage.Message = ex.Message;
                return StatusCode(500, statusMessage);
            }
        }

        //[AllowAnonymous]
        //[Route("api/change-password")]
        //[HttpPost]
        //public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        //{
        //    Login loginModel = new Login();
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var user = await _userManager.FindByEmailAsync(loginModel.Email);
        //    if (user == null)
        //    {
        //        return NotFound(new { message = "User not found" });
        //    }

        //    var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        //    if (result.Succeeded)
        //    {
        //        return Ok(new { message = "Password changed successfully" });
        //    }

        //    return BadRequest(result.Errors);
        //}

            [HttpPost("api/change-password")]
            public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Invalid input" });
                }

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return NotFound(new { success = false, message = "User not found" });
                }

                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    return Ok(new { success = true, message = "Password changed successfully" });
                }

                return BadRequest(new { success = false, message = "Password change failed" });
            }
        

        //[HttpPost("api/change-password")]
        //public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.GetUserAsync(User);
        //        if (user == null)
        //        {
        //            return Unauthorized();
        //        }

        //        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        //        if (result.Succeeded)
        //        {
        //            await _signInManager.RefreshSignInAsync(user);
        //            return Ok(new { message = "Password changed successfully" });
        //        }

        //        return BadRequest(result.Errors);
        //    }

        //    return BadRequest(ModelState);
        //}

        [HttpPost("api/forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // Do not reveal that the user does not exist
                return Ok(new { Message = "If your email is in our database, you'll receive a password reset link." });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ResetPassword",
                pageHandler: null,
                values: new { area = "Identity", token, email = user.Email },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(
                request.Email,
                "Reset Password",
                $"Please reset your password by <a href='{callbackUrl}'>clicking here</a>.");

            return Ok(new { Message = "If your email is in our database, you'll receive a password reset link." });
        }
        public class ForgotPasswordRequest
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }
    }

    

}

