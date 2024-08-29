﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ToDoListApp.Models;

namespace ToDoListApp.Areas.Identity.Pages.Account
{
    //    public class ChangePasswordModel : PageModel
    //    {
    //        private readonly UserManager<User> _userManager;
    //        private readonly SignInManager<User> _signInManager;
    //        private readonly ILogger<ChangePasswordModel> _logger;

    //        public ChangePasswordModel(
    //            UserManager<User> userManager,
    //            SignInManager<User> signInManager,
    //            ILogger<ChangePasswordModel> logger)
    //        {
    //            _userManager = userManager;
    //            _signInManager = signInManager;
    //            _logger = logger;
    //        }

    //        /// <summary>
    //        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //        ///     directly from your code. This API may change or be removed in future releases.
    //        /// </summary>
    //        [BindProperty]
    //        public InputModel Input { get; set; }

    //        /// <summary>
    //        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //        ///     directly from your code. This API may change or be removed in future releases.
    //        /// </summary>
    //        [TempData]
    //        public string StatusMessage { get; set; }

    //        /// <summary>
    //        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //        ///     directly from your code. This API may change or be removed in future releases.
    //        /// </summary>
    //        public class InputModel
    //        {
    //            /// <summary>
    //            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //            ///     directly from your code. This API may change or be removed in future releases.
    //            /// </summary>
    //            [Required]
    //            [DataType(DataType.Password)]
    //            [Display(Name = "Current password")]
    //            public string OldPassword { get; set; }

    //            /// <summary>
    //            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //            ///     directly from your code. This API may change or be removed in future releases.
    //            /// </summary>
    //            [Required]
    //            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    //            [DataType(DataType.Password)]
    //            [Display(Name = "New password")]
    //            public string NewPassword { get; set; }

    //            /// <summary>
    //            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //            ///     directly from your code. This API may change or be removed in future releases.
    //            /// </summary>
    //            [DataType(DataType.Password)]
    //            [Display(Name = "Confirm new password")]
    //            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    //            public string ConfirmPassword { get; set; }
    //        }

    //        public async Task<IActionResult> OnGetAsync()
    //        {
    //            var user = await _userManager.GetUserAsync(User);
    //            if (user == null)
    //            {
    //                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
    //            }

    //            var hasPassword = await _userManager.HasPasswordAsync(user);
    //            if (!hasPassword)
    //            {
    //                return RedirectToPage("./SetPassword");
    //            }

    //            return Page();
    //        }

    //        public async Task<IActionResult> OnPostAsync()
    //        {
    //            if (!ModelState.IsValid)
    //            {
    //                return Page();
    //            }

    //            var user = await _userManager.GetUserAsync(User);
    //            if (user == null)
    //            {
    //                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
    //            }

    //            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
    //            if (!changePasswordResult.Succeeded)
    //            {
    //                foreach (var error in changePasswordResult.Errors)
    //                {
    //                    ModelState.AddModelError(string.Empty, error.Description);
    //                }
    //                return Page();
    //            }

    //            await _signInManager.RefreshSignInAsync(user);
    //            _logger.LogInformation("User changed their password successfully.");
    //            StatusMessage = "Your password has been changed.";

    //            return RedirectToPage();
    //        }
    //    }

    [ApiController]
    [Route("api/[controller]")]
    public class ChangePasswordController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ChangePasswordController> _logger;

        public ChangePasswordController(
            UserManager<User> userManager,
            ILogger<ChangePasswordController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        ///     Change the user's password.
        /// </summary>
        /// <param name="model">The change password model.</param>
        /// <returns>The result of the password change attempt.</returns>
        [HttpPost]
        [AllowAnonymous] // Adjust if authentication is required
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Assuming the email is passed in the model
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }

            _logger.LogInformation("User changed their password successfully.");
            return Ok(new { message = "Password changed successfully" });
        }
    }

    public class ChangePasswordModel
    {
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The new password must be at least 6 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

}
