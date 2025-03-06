using Repository.Model;

namespace Repository.InterFace
{
    public interface IGroupMenuAuthDal
    {
        public int BatchAdd(List<GroupMenuAuthEntity> groupMenuAuthEntities);

        public List<int> GetNotExistMenuAuth(List<GroupMenuAuthEntity> groupMenuAuthEntities);

        public int BeatchDeleteForCanelGroupMenuAuth(List<int> Ids);

        public int BeatchDeleteForGroup(int groupId);

        public List<int> GetGroupMenuAuthByGroup(GroupMenuAuthEntity data);
    }
}
