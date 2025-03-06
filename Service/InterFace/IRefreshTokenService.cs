using Repository.Model;
using Service.Model;

namespace Services.InterFace
{
    public interface IRefreshTokenService
    {
      public ApiResponseModel Add(RefreshTokenEntity refreshToken);

       public ApiResponseModel GetAll(int UserMasterId, string Token);

        public ApiResponseModel Delete(List<RefreshTokenEntity> data);

        public List<RefreshTokenEntity> Get(int userMasterId);

        public ApiResponseModel Update(RefreshTokenEntity data);

        public ApiResponseModel GenerateRefleshToken(string userMasterId);

        public string GenerateToken(int id, string username);
    }
}
