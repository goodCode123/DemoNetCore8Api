using Infrastructure.Attributes;
using Microsoft.AspNetCore.Http;
using Repository.InterFace;
using Repository.Model;
using Service.Model;
using Services.InterFace;
using Services.Model.Login;
using Microsoft.AspNetCore.Identity;

namespace Services.Service
{
    [PerLifetimeScopeService]
    public class LoginService : ILoginService
    {
        private readonly ILoginDal ILogin;
        private readonly IRefreshTokenService IRefreshToken;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IRefreshTokenService refreshTokenService;
        public LoginService(ILoginDal _ILogin, IRefreshTokenService _iRefreshToken
            , IHttpContextAccessor _httpContextAccessor, IRefreshTokenService _refreshTokenService)
        {
            ILogin = _ILogin;
            IRefreshToken = _iRefreshToken;
            httpContextAccessor = _httpContextAccessor;
            refreshTokenService = _refreshTokenService;
        }
        public ApiResponseModel Login(LoginModel data)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            UserMasterEntity userMasterEntity = new UserMasterEntity
            {
                Account = data.Account,
                Pwd = data.Pwd,
            };       

            var userMasterResult = ILogin.Login(userMasterEntity);

            if (userMasterResult.Count == 1 && VerifyHashedPassword(data.Pwd, userMasterResult[0].Pwd))
            {
                if (userMasterResult[0].Status == 99)
                {
                    apiResponse.Code = "202";
                    apiResponse.Data = string.Empty;
                    apiResponse.Msg = "帳號已停用";

                    return apiResponse;
                }

                string result =  refreshTokenService.GenerateToken(userMasterResult[0].Id, userMasterResult[0].Name);
                var tokenResult =  IRefreshToken.Get(userMasterResult[0].Id);
                string refreshToken = Guid.NewGuid().ToString();
                if (tokenResult.Count == 0)
                {
                    IRefreshToken.Add(new RefreshTokenEntity()
                    {
                        UserMasterId = userMasterResult[0].Id,
                        Token = result,
                        RefreshToken = refreshToken,
                        ExpiryDate = DateTime.Now.AddDays(2),

                    });
                }
                else
                {
                    IRefreshToken.Update(new RefreshTokenEntity() {
                        Id = tokenResult[0].Id,
                        UserMasterId = userMasterResult[0].Id,
                        Token = result,
                        RefreshToken = refreshToken,
                        ExpiryDate = DateTime.Now.AddDays(2),
                        ModifyDate = DateTime.Now.AddDays(2),
                    });
                }
                apiResponse.Code = "200";
                apiResponse.Data = new { Token = result , RefreshToken = refreshToken };
                apiResponse.Msg = string.Empty;
            }
            else
            {
                apiResponse.Code = "201";
                apiResponse.Data = string.Empty;
                apiResponse.Msg = "登入失敗";
            }

            return apiResponse;
        }

        public ApiResponseModel Logout(int userMasterId )
        {
            string token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var tokenResult = IRefreshToken.Get(userMasterId);
            IRefreshToken.Delete(tokenResult);
            ApiResponseModel apiResponseModel = new ApiResponseModel()
            {
                Code = "200",
                Data = string.Empty,
                Msg = string.Empty
            };

            return apiResponseModel;
        }


        private bool VerifyHashedPassword(string pwd , string passwordHash)
        {
            bool result = false;
            var passwordHasher = new PasswordHasher<string>();
            var verifyResult = passwordHasher.VerifyHashedPassword(null, passwordHash, pwd);
            if (verifyResult == PasswordVerificationResult.Success)
            {
                result = true;
            }

            return result;
        }
    }
}
