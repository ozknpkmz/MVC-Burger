using HamburgerMVC.Models.DTOs;
using HamburgerMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Text;

namespace HamburgerMVC.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<AppUser> signInManager;
        private readonly Context context;
        public AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<IdentityRole> _roleManager, Context _context)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            context = _context;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(CustormerDTO custormerDTO)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    UserName = custormerDTO.UserName,
                    Email = custormerDTO.Email,

                };

                IdentityRole role = await roleManager.FindByNameAsync("Customer");
                if (role == null)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole("Customer"));
                    if (!result.Succeeded)
                    {
                        Errors(result);
                    }
                }

                IdentityResult identityResult = await userManager.CreateAsync(appUser, custormerDTO.Password);

                if (identityResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(appUser, "Customer");
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (IdentityError error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(custormerDTO);
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            returnUrl = returnUrl is null ? "/Home/Index" : returnUrl;
            return View(new Login()
            {
                ReturnUrl = returnUrl
            });

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await userManager.FindByEmailAsync(login.Email);
                if (appUser != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, login.Password, false, false);
                    if (result.Succeeded)
                    {
                        if (await userManager.IsInRoleAsync(appUser, "Customer"))
                            return RedirectToAction("HomePage", "Order");
                        else
                            return RedirectToAction("ExtraIngredientList", "ExtraIngredient");
                    }
                    ModelState.AddModelError("", "Invalid password or username");
                }
            }
            return View(login);

        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        //[HttpPost]
       //public async Task<IActionResult> ForgotPassword(string email)
        //{
        //    AppUser appUser = await userManager.FindByEmailAsync(email);
        //    string saltPassword= "";
        //    string _FromMail = "mucidim59@gmail.com";
        //    string _FromPassword = "izjudylxzdsrngvk";

        //    string _ToMail = email;
        //    string _ToPassword = context.Users.Where(x => x.Email == email)
        //                                            .Select(x => x.PasswordHash)
        //                                            .First();

        //    //byte[] passwordHashBytes = Convert.FromBase64String(_ToPassword); 
        //    //byte[] passwordSaltBytes = Convert.FromBase64String(saltPassword); 

        //    var passwordHasher = new PasswordHasher<AppUser>(); 

        //    PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(null, (_ToPassword), (saltPassword));

        //    //saltPassword = Encoding.UTF8.GetString(passwordSaltBytes);

        //    if (result == PasswordVerificationResult.Success)
        //    {
        //        string _MailMessage = "<!DOCTYPE html>\r\n" +
        //                          "<html>\r\n  " +
        //                          "<head>\r\n    " +
        //                          "<meta charset=\"utf-8\">\r\n    " +
        //                          "<title>Password Information</title>\r\n  " +
        //                          "</head>\r\n  " +
        //                          "<body>\r\n    " +
        //                          "<h2>Hello Dear User,</h2>\r\n    " +
        //                          $"<p>Your password is: <strong>{saltPassword}</strong></p>\r\n    " +
        //                          "<p>Have a nice day!</p>\r\n  " +
        //                          "</body>\r\n" +
        //                          "</html>";

        //        MailMessage mail = new MailMessage();
        //        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

        //        mail.From = new MailAddress(_FromMail);
        //        mail.To.Add(_ToMail);
        //        mail.Subject = "Doe Brothers Password Reminder";
        //        mail.IsBodyHtml = true;
        //        mail.Body = _MailMessage;

        //        SmtpServer.Port = 587;
        //        SmtpServer.Credentials = new System.Net.NetworkCredential(_FromMail, _FromPassword);
        //        SmtpServer.EnableSsl = true;

        //        SmtpServer.Send(mail);
        //        return RedirectToAction("Login"); 
        //    }
        //    else
        //    {
        //        Errors(result);
        //    }
        //}

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                TempData["Error"] = $"{error.Code} - {error.Description}";
            }
        }

    }
}
