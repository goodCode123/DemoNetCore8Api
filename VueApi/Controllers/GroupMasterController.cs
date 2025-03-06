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
    public class GroupMasterController : ControllerBase
    {
        private readonly ICrudService<GroupMasterEntity> _crudService;

        public GroupMasterController(ICrudService<GroupMasterEntity> crudService) {
            _crudService = crudService;
        }

        [HttpGet]
        public ApiResponseModel GetAll()
        {
            return _crudService.GetAll();
        }

        [HttpPost]
        public ApiResponseModel Update(GroupMasterEntity data)
        {
            return _crudService.Update(data);
        }

        [HttpPost]
        public ApiResponseModel Add(GroupMasterEntity data)
        {
            return _crudService.Add(data);
        }

        [HttpPost]
        public ApiResponseModel Delete(GroupMasterEntity data)
        {
            return _crudService.Delete(data);
        }
    }
}
