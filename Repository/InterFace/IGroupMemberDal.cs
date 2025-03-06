using Repository.MergeModel;
using Repository.Model;

namespace Repository.InterFace
{
    public interface IGroupMemberDal
    {
        public int BatchAdd(List<GroupMemberEntity> groupMenuAuthEntities);

        public List<DeleteGroupMemberModel> GetNonGroupMembers(GroupMemberEntity groupMenuAuthEntities);

        public List<DeleteGroupMemberModel> GetGroupMembersById(GroupMemberEntity groupMenuAuthEntities);

        public int DeleteGroupMembers(List<int> Ids);

        public int DeleteGroupMembersByGroupMasterId(int id);
    }
}
