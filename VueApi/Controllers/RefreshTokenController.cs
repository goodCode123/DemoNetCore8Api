using Microsoft.AspNetCore.Mvc;
using Service.Model;
using Services.InterFace;

namespace DemoNetCore8Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RefreshTokenController : ControllerBase
    {
        private IRefreshTokenService refreshTokenService;
        public RefreshTokenController(IRefreshTokenService _refreshTokenService) 
        {
            refreshTokenService = _refreshTokenService;
        }

        [HttpPost]
        public ApiResponseModel SetRefreshToken([FromBody] string userMasterId)
        {
           return  refreshTokenService.GenerateRefleshToken(userMasterId);
        }
    }
}
