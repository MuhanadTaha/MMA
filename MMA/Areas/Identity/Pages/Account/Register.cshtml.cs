

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MMA.Models;
using MMA.Utility;

namespace MMA.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager
            )

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string Name { get; set; }
            [Display(Name = "Street Address")]
            public string StreetAddress { get; set; }
        //    [Display(Name = "Postal Code")]
         //  public string PostalCode { get; set; }
            public string City { get; set; }
           // public string state { get; set; }
            [Display(Name = "Phone Number")]
            [RegularExpression("^[0-9]+$", ErrorMessage = "Phone number must contain only digits.")]
            [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 digits.")]
            public string PhoneNumber { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Name = Input.Name,
                    PhoneNumber = Input.PhoneNumber,
                    StreetAddress = Input.StreetAddress,
                    City = Input.City,
                    
                };



                var result = await _userManager.CreateAsync(user, Input.Password); //هان بتم عمل كريت لليوزر لما يسجل
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(SD.ManagerUser)) // في حال كانت رووول المانيجار غير موجودة 
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.ManagerUser)); //رح ينتشا رول باسم مانيجيار
                    }

                    if (!await _roleManager.RoleExistsAsync(SD.TeacherUser)) // في حال كانت رووول المانيجار غير موجودة 
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.TeacherUser)); //رح ينتشا رول باسم مانيجيار
                    }

                    if (!await _roleManager.RoleExistsAsync(SD.CusotmerEndUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.CusotmerEndUser));
                    }

                    string role = HttpContext.Request.Form["rdUserRole"].ToString();

                    if (string.IsNullOrEmpty(role))
                    {
                        await _userManager.AddToRoleAsync(user, SD.CusotmerEndUser);
                        await _signInManager.SignInAsync(user, isPersistent: false); // في حال كان الكاستمار هو اللي بعمل ريجستريشن رح يرجعه للصفحة اللي كان بده يدخل عليها
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, role);

                    }

                    return RedirectToAction("Index", "Users", new { area = "Admin" }); //في حال اللي أنشأ اليوزر هو الآدمن رح ينتقل بعدها للاينديكس التابع للكونترولار يوزرس الموجود بالإيريا اللي اسمها آدمن 




                    /* _logger.LogInformation("User created a new account with password.");

                     var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                     code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                     var callbackUrl = Url.Page(
                         "/Account/ConfirmEmail",
                         pageHandler: null,
                         values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                         protocol: Request.Scheme);

                     await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                         $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."); */

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }

}
