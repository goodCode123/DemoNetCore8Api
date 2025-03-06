using Infrastructure.Attributes;
using Repository.InterFace;
using Repository.Model;
using Service.Model;
using Services.EnumsModel;
using Services.InterFace;

namespace Services.Service
{
    [PerLifetimeScopeService]
    public class GroupMasterService
    {
        private readonly ICrudService<GroupMasterEntity> crudService;
        private readonly IGroupMenuAuthDal groupMenuAuthDal;
        private readonly IGroupMemberDal groupMemberDal;
        public GroupMasterService(
              IGroupMenuAuthDal _groupMenuAuthDal,
              IGroupMemberDal _groupMemberDal,
              ICrudService<GroupMasterEntity> _crudService)
        {
            groupMenuAuthDal = _groupMenuAuthDal;
            groupMemberDal = _groupMemberDal;
            crudService = _crudService;
        }

        public ApiResponseModel Add(GroupMasterEntity data)
        {
            return crudService.Add(data);
        }

        public ApiResponseModel GetAll()
        {
            return crudService.GetAll();
        }

        public ApiResponseModel Update(GroupMasterEntity GroupMasterEntity)
        {
            return crudService.Update(GroupMasterEntity);
        }

        public ApiResponseModel Delete(GroupMasterEntity GroupMasterEntity)
        {
            var result = crudService.Delete(GroupMasterEntity);
            var groupMenuAuthResult = groupMenuAuthDal.BeatchDeleteForGroup(GroupMasterEntity.Id);
            var groupMemberResult = groupMemberDal.DeleteGroupMembersByGroupMasterId(GroupMasterEntity.Id);

            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = result,
                Msg = string.Empty
            };

            return apiResponse;
        }
    }
}
