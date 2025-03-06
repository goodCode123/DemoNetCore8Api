using Repository.Model;
using Service.Model;

namespace Services.InterFace
{
    public interface IGroupMemberService
    {
        ApiResponseModel BatchAdd(List<GroupMemberEntity> groupMemberEntity);

        ApiResponseModel GetNonGroupMembers(GroupMemberEntity data);

        ApiResponseModel GetGroupMembersById(GroupMemberEntity data);

        ApiResponseModel DeleteGroupMembers(List<int> Ids);

    }
}
