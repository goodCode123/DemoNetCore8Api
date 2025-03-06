using Infrastructure.Attributes;
using Repository.InterFace;
using Repository.Model;

namespace Repository.Dal
{
    [PerLifetimeScopeService]
    public class UserMasterDal : GenericCrudDal<UserMasterEntity>, IUserMasterDal
    {
    }
}
