using Microsoft.AspNetCore.Mvc;
using Service.Model;
using Services.InterFace;
using Services.Model.Login;

namespace DemoNetCore8Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;
        public LoginController(ILoginService _loginService) 
        {
            loginService = _loginService;
        }

        [HttpPost]
        public ApiResponseModel Login(LoginModel data)
        {
            return loginService.Login(data);
        }

        [HttpPost]
        public ApiResponseModel Logout([FromBody]string userMasterId)
        {
            return loginService.Logout(int.Parse(userMasterId));
        }
    }
}
