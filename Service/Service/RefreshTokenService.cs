using Infrastructure.Attributes;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.InterFace;
using Repository.Model;
using Service.Model;
using Services.InterFace;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Service
{
    [PerLifetimeScopeService]
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly ICrudService<RefreshTokenEntity> crudService;
        private readonly ICrudDal<RefreshTokenEntity> crudDal;
        private readonly ICrudDal<UserMasterEntity> userCrudDal;


        public RefreshTokenService(ICrudService<RefreshTokenEntity> _crudService
            , ICrudDal<RefreshTokenEntity> _crudDal, ICrudDal<UserMasterEntity> _userCrudDal) 
        {
            crudService = _crudService;
            crudDal = _crudDal;
            userCrudDal = _userCrudDal;
        }

        public ApiResponseModel Add(RefreshTokenEntity refreshToken)
        {
            return crudService.Add(refreshToken);
        }

        public ApiResponseModel GetAll(int UserMasterId , string Token)
        {
            return crudService.GetAll(new { UserMasterId = UserMasterId, Token= Token });
        }

        public ApiResponseModel Delete(List<RefreshTokenEntity> data)
        {
            return crudService.Delete(data);
        }

        public List<RefreshTokenEntity> Get(int userMasterId) 
        {
            return crudDal.GetAll(new { UserMasterId = userMasterId });
        }

        public ApiResponseModel Update(RefreshTokenEntity data)
        {
            return crudService.Update(data);
        }

        public string GenerateToken(int id, string username)
        {
            var claims = new[]
            {
             new Claim(JwtRegisteredClaimNames.Sub, username),
             new Claim("Id", id.ToString()),
             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JwtSettings.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: AppSettings.JwtSettings.Issuer,
                audience: AppSettings.JwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(int.Parse(AppSettings.JwtSettings.Expires)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public ApiResponseModel GenerateRefleshToken(string userMasterId)
        {
            ApiResponseModel apiResponseData = new ApiResponseModel();
            //檢查RefleshToken是否過期
            var result =  crudDal.GetAll(new { UserMasterId = userMasterId });
        
            if (result.Count > 0 && result[0].IsRevoked == false && result[0].ExpiryDate >DateTime.Now) 
            {
                var userResult = userCrudDal.GetAll(new { Id = userMasterId });
                apiResponseData.Code = "200";
                apiResponseData.Msg = string.Empty;
                apiResponseData.Data = GenerateToken(result[0].Id, userResult[0].Account);
            }
            else
            {
                apiResponseData.Code = "201";
                apiResponseData.Msg = "RefleshToken過期";
                apiResponseData.Data = string.Empty;
            }

            return apiResponseData;
        }
    }
}
