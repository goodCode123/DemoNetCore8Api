using Infrastructure.Attributes;
using Repository.Dapper;
using Repository.InterFace;
using Repository.Model;

namespace Repository.Dal
{
    [PerLifetimeScopeService]
    public class LoginDal : DapperHelper, ILoginDal
    {
        public LoginDal() { }
       public List<UserMasterEntity> Login(UserMasterEntity userMasterEntity)
        {
            const string sql = @"SELECT *
                                 FROM UserMaster um
                                 WHERE Account = @Account";


            return NDRS.GetList<UserMasterEntity>(sql, userMasterEntity).ToList();
        }
    }
}
