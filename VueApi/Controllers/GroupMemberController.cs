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
    public class GroupMemberController : ControllerBase
    {
        private readonly ICrudService<GroupMemberEntity> _crudService;
        private readonly IGroupMemberService _groupMemberService;    

        public GroupMemberController(ICrudService<GroupMemberEntity> crudService, IGroupMemberService groupMemberService)
        {
            _crudService = crudService;
            _groupMemberService = groupMemberService;
        }

        [HttpGet]
        public ApiResponseModel GetAll()
        {
            return _crudService.GetAll();
        }

        [HttpPost]
        public ApiResponseModel Update(GroupMemberEntity data)
        {
            return _crudService.Update(data);
        }

        [HttpPost]
        public ApiResponseModel Add(GroupMemberEntity data)
        {
            return _crudService.Add(data);
        }

        [HttpPost]
        public ApiResponseModel Delete(GroupMemberEntity data)
        {
            return _crudService.Delete(data);
        }

        [HttpPost]
        public ApiResponseModel BatchAdd(List<GroupMemberEntity> data)
        {
            return _groupMemberService.BatchAdd(data);
        }

        [HttpPost]
        public ApiResponseModel GetNonGroupMembers(GroupMemberEntity data) 
        {
            return _groupMemberService.GetNonGroupMembers(data);
        }

        [HttpPost]
        public ApiResponseModel GetGroupMembersById(GroupMemberEntity data)
        {
            return _groupMemberService.GetGroupMembersById(data);
        }

        [HttpPost]
        public ApiResponseModel DeteleGroupMembers([FromBody]List<int> Ids)
        {
           
            return _groupMemberService.DeleteGroupMembers(Ids);
        }
    }
}
