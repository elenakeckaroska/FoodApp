using FoodApp.Models.Dtos;
using FoodApp.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Web.Controllers.rest
{
    [Route("api/admin")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        public readonly IRemoteAuthenticationService remoteAuthenticationService;

        public AdminsController(IRemoteAuthenticationService remoteAuthenticationService)
        {
            this.remoteAuthenticationService = remoteAuthenticationService;
        }

        [HttpPost("[action]")]
        public bool Index(UserLoginDto model)
        {
            return remoteAuthenticationService.UserExistsWithRoleAsync(model.Email,
                model.Password, "Admin").Result;
        }
    }
}
