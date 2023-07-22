using FoodApp.Models.Identity;
using FoodApp.Service.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Service.Implementation
{
    public class RemoteAuthenticationService : IRemoteAuthenticationService
    {
        private readonly UserManager<FoodAppUser> _userManager;
        private readonly SignInManager<FoodAppUser> _signInManager;

        public RemoteAuthenticationService(UserManager<FoodAppUser> userManager, SignInManager<FoodAppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> UserExistsWithRoleAsync(string username, string password, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return false;
            }

            var signInResult = await _signInManager.PasswordSignInAsync(username, password, false, false);
            if (!signInResult.Succeeded)
            {
                return false;
            }

            return await _userManager.IsInRoleAsync(user, role);
        }
    }
}
