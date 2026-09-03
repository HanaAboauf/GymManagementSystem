using GYMManagementBLL.Services.Interfaces;
using GYMManagementBLL.ViewModel.AccountViewModels;
using GYMManagementDL.Enitities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _UserManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            _UserManager = userManager;
        }

        public ApplicationUser? ValidateUser(LoginViewModel loginViewModel)
        {
            try
            {
                var user = _UserManager.FindByEmailAsync(loginViewModel.Email).Result;
                if (user is null) return null;
                var validPassword = _UserManager.CheckPasswordAsync(user, loginViewModel.Password).Result;
                return validPassword ? user : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
        }
    }
}
