using Infrastructure.Attributes;
using Newtonsoft.Json;
using Repository.InterFace;
using Repository.Model;
using Service.Model;
using Services.EnumsModel;
using Services.InterFace;

namespace Services.Service
{
    [PerLifetimeScopeService]
    public class GroupMemberService : IGroupMemberService
    {
        private readonly ICrudService<GroupMemberEntity> crudService;
        private readonly IGroupMemberDal groupMemberDal ;
        public GroupMemberService( IGroupMemberDal _groupMemberDal
            , ICrudService<GroupMemberEntity> _crudService)
        {
            groupMemberDal = _groupMemberDal;
            crudService = _crudService;
        }

        public ApiResponseModel Add(GroupMemberEntity data)
        {
            return crudService.Add(data);
        }

        public ApiResponseModel Delete(GroupMemberEntity data)
        {
            return crudService.Delete(data);
        }

        public ApiResponseModel GetAll()
        {
            return crudService.GetAll();
        }

        public ApiResponseModel Update(GroupMemberEntity data)
        {
            return crudService.Update(data);
        }

        public ApiResponseModel BatchAdd(List<GroupMemberEntity> groupMemberEntity)
        {
            groupMemberDal.BatchAdd(groupMemberEntity);
            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = JsonConvert.SerializeObject(""),
                Msg = string.Empty
            };

            return apiResponse;
        }

        public ApiResponseModel GetNonGroupMembers(GroupMemberEntity data)
        {
            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = groupMemberDal.GetNonGroupMembers(data),
                Msg = string.Empty
            };

            return apiResponse;
        }

        public ApiResponseModel GetGroupMembersById(GroupMemberEntity data)
        {
            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = groupMemberDal.GetGroupMembersById(data),
                Msg = string.Empty
            };

            return apiResponse;
        }

        public ApiResponseModel DeleteGroupMembers(List<int> Ids)
        {
            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = groupMemberDal.DeleteGroupMembers(Ids),
                Msg = string.Empty
            };

            return apiResponse;
        }
    }
}
