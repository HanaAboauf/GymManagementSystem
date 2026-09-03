using GYMManagementBLL.Services.Interfaces;
using GYMManagementBLL.ViewModel.AccountViewModels;
using GYMManagementDL.Enitities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GYMManagementPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<ApplicationUser> _SignInManager;

        public AccountController( IAccountService accountService, SignInManager<ApplicationUser> signInManager) 
        { 
            _accountService = accountService;
            _SignInManager = signInManager;
        }
        #region Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = _accountService.ValidateUser(model);
            if (user is null){ 
                ModelState.AddModelError("InvalidLogin", "Your email or password is incorrect.");
                return View(model); 
            }
            var result = _SignInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false).Result;
            if(result.IsNotAllowed)
                ModelState.AddModelError("InvalidLogin", "Your account is not allowed to sign in.");
            if(result.IsLockedOut)
                ModelState.AddModelError("InvalidLogin", "Your account is locked out.");
            if(result.Succeeded)
                return RedirectToAction("Index", "Home");
            return View(model);


        }
        #endregion

        #region Logout

        public ActionResult Logout()
        {
            _SignInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction("Login", "Account");
        }
        #endregion
        #region AccessDenied

        public ActionResult AccessDenied()
        {
            return View();
        }
        #endregion

    }
}
