using Repository.Model;

namespace Repository.InterFace
{
    public interface ILoginDal
    {
        public List<UserMasterEntity> Login(UserMasterEntity userMasterEntity);
    }
}
