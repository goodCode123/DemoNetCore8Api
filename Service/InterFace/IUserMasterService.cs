using Repository.Model;
using Service.Model;

namespace Services.InterFace
{
    public interface IUserMasterService
    {
        public ApiResponseModel GetUserMasters();

        public ApiResponseModel DeleteUser(UserMasterEntity userMasterEntity );

        public ApiResponseModel Add(UserMasterEntity data);

        ApiResponseModel Update(UserMasterEntity userMasterEntity);
    }
}
