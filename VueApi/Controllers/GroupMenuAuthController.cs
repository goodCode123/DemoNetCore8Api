using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Model;
using Service.Model;
using Services.InterFace;
using DemoNetCore8Api.Service.Attributes;

namespace DemoNetCore8Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class GroupMenuAuthController : ControllerBase
    {
        private readonly IGroupMenuAuthService groupMenuAuthService;
        public GroupMenuAuthController(IGroupMenuAuthService _groupMenuAuthService )
        {
            groupMenuAuthService = _groupMenuAuthService;
        }

        [HttpPost]
        public ApiResponseModel BatchAdd(List<GroupMenuAuthEntity> data)
        {
            return groupMenuAuthService.BatchAdd(data);
        }

        [HttpPost]
        [AuthorizePermission("/GroupMaster")] // 指定所需權限
        public ApiResponseModel GetGroupMenuAuthByGroup(GroupMenuAuthEntity data)
        {
            return groupMenuAuthService.GetGroupMenuAuthByGroup(data);
        }
    }
}
