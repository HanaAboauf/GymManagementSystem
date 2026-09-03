using GYMManagementDL.Enitities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Data.DataSeeding
{
    public static class IdentityDbContextseeding
    {
        public static  bool IsSeeding(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                var HasRoles=  roleManager.Roles.Any();
                var HasUsers=  userManager.Users.Any();
                if (HasRoles && HasUsers) return false;
                if (!HasRoles)
                {
                    var roles = new List<IdentityRole>
                    {
                        new IdentityRole { Name = "SuperAdmin" },
                        new IdentityRole { Name = "Admin" }
                    };
                    foreach (var role in roles)
                    {
                        if (! roleManager.RoleExistsAsync(role.Name!).Result){

                            roleManager.CreateAsync(role).Wait();
                        }
                    }
                }
                if (!HasUsers)
                {
                    var superAdminUser = new ApplicationUser
                    {
                        FirstName = "Hana",
                        LastName = "Abuauf",
                        UserName = "hanahany",
                        Email = "hana.abuauf@gmail.com",
                        PhoneNumber = "01097261273"

                    };
                    userManager.CreateAsync(superAdminUser, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(superAdminUser, "SuperAdmin").Wait();
                    var AdminUser = new ApplicationUser
                    {
                        FirstName = "Lojyn",
                        LastName = "Elkashef",
                        UserName = "lojynelkashef",
                        Email = "lojyn.elkashef@gmail.com",
                        PhoneNumber = "01097261273"

                    };
                    userManager.CreateAsync(AdminUser, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(AdminUser, "Admin").Wait();

                }
                     

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
