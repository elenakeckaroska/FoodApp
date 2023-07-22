using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Service.Interface
{
    public interface IRemoteAuthenticationService
    {
        Task<bool> UserExistsWithRoleAsync(string username, string password, string role);

    }
}
