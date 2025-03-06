using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Model;
using Service.Model;
using Services.InterFace;

namespace DemoNetCore8Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class UserMasterController : ControllerBase
    {
        private readonly IUserMasterService _userMasterService;

        public UserMasterController(ICrudService<UserMasterEntity> crudService, IUserMasterService userMasterService) 
        {
            _userMasterService = userMasterService;
        }

        [HttpGet]
        public ApiResponseModel GetAll()
        {
            return _userMasterService.GetUserMasters();
        }

        [HttpPost]
        public ApiResponseModel Update(UserMasterEntity data)
        {
            return _userMasterService.Update(data);
        }

        [HttpPost]
        public ApiResponseModel Add(UserMasterEntity data)
        {
            return _userMasterService.Add(data);
        }

        [HttpPost]
        public ApiResponseModel Delete(UserMasterEntity userMasterEntity)
        {
            return _userMasterService.DeleteUser(userMasterEntity);
        }
    }
}
