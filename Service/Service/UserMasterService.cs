using Infrastructure.Attributes;
using Microsoft.AspNetCore.Identity;
using Repository.InterFace;
using Repository.Model;
using Service.Model;
using Services.InterFace;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services.Service
{
    [PerLifetimeScopeService]
    public class UserMasterService : IUserMasterService
    {
        private readonly ICrudService<UserMasterEntity> crudService;
        private readonly IUserMasterDal masterDal;

        public UserMasterService(IUserMasterDal _masterDal, ICrudService<UserMasterEntity> _crudService)
        {
            masterDal = _masterDal;
            crudService = _crudService;
        }

        public ApiResponseModel Add(UserMasterEntity data)
        {
            data.Pwd = HashPassword(data.Pwd);
            return crudService.Add(data);
        }

        public ApiResponseModel GetAll()
        {
            return crudService.GetAll();
        }

        public ApiResponseModel Update(UserMasterEntity data)
        {
            data.Pwd = HashPassword(data.Pwd);
            return crudService.Update(data);
        }

        public ApiResponseModel Delete(UserMasterEntity userMasterEntity)
        {
            return crudService.Delete(userMasterEntity);
        }

        public ApiResponseModel GetUserMasters()
        {
            return  crudService.GetAll(new { Status = 1 });
        }

        public ApiResponseModel DeleteUser(UserMasterEntity userMasterEntity)
        {
            userMasterEntity.Status = 99;
            return crudService.Update(userMasterEntity);
        }

        private string HashPassword(string pwd)
        {
            var passwordHasher = new PasswordHasher<string>();
            string hashedPassword = passwordHasher.HashPassword(null, pwd);

            return hashedPassword;
        }
    }
}
