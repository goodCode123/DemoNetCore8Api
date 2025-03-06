using Infrastructure.Attributes;
using Repository.Model;

namespace Repository.Dal
{
    [PerLifetimeScopeService]
    public class GroupMasterDal : GenericCrudDal<GroupMasterEntity>
    {

    }
}
