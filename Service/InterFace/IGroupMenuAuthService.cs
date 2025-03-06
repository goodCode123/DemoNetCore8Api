using Repository.Model;
using Service.Model;

namespace Services.InterFace
{
    public interface IGroupMenuAuthService
    {
        ApiResponseModel BatchAdd(List<GroupMenuAuthEntity> groupMenuAuthEntities);

        ApiResponseModel GetGroupMenuAuthByGroup(GroupMenuAuthEntity data);
    }
}
