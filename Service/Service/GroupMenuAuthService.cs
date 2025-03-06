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
    public class GroupMenuAuthService : IGroupMenuAuthService
    {
        private readonly ICrudService<GroupMenuAuthEntity> crudService;
        private readonly IGroupMenuAuthDal groupMenuAuthDal;

        public GroupMenuAuthService(
            IGroupMenuAuthDal _groupMenuAuthDal,
            ICrudService<GroupMenuAuthEntity> _crudService)
        {
            groupMenuAuthDal = _groupMenuAuthDal;
            crudService = _crudService;
        }
        public ApiResponseModel Add(GroupMenuAuthEntity data)
        {
            return crudService.Add(data);
        }

        public ApiResponseModel Delete(GroupMenuAuthEntity data)
        {
            return crudService.Delete(data);
        }

        public ApiResponseModel GetAll()
        {
            return crudService.GetAll();
        }

        public ApiResponseModel Update(GroupMenuAuthEntity data)
        {
            return crudService.Update(data);
        }

        public ApiResponseModel BatchAdd(List<GroupMenuAuthEntity> groupMenuAuthEntities)
        {
         
            if (groupMenuAuthEntities.Count == 1 & groupMenuAuthEntities[0].MenuItemId == null)
            {
                groupMenuAuthDal.BeatchDeleteForGroup(groupMenuAuthEntities[0].GroupMasterId);
            }
            else
            {
                groupMenuAuthDal.BatchAdd(groupMenuAuthEntities);
                List<int> notExistResult =  groupMenuAuthDal.GetNotExistMenuAuth(groupMenuAuthEntities);
                if (notExistResult.Any())
                {
                    groupMenuAuthDal.BeatchDeleteForCanelGroupMenuAuth(notExistResult);
                }          
            }
            
            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = JsonConvert.SerializeObject(""),
                Msg = string.Empty
            };

            return apiResponse;
        }

        public ApiResponseModel GetGroupMenuAuthByGroup(GroupMenuAuthEntity data)
        {
           List<int> result =   groupMenuAuthDal.GetGroupMenuAuthByGroup(data);
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
