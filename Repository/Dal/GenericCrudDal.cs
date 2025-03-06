using Infrastructure.Attributes;
using Repository.Dapper;
using Repository.InterFace;

namespace Repository.Dal
{
    [PerLifetimeScopeService]
    public class GenericCrudDal<T> : DapperHelper, ICrudDal<T> where T : class
    {
        public long Add(T data)
        {
            return NDRS.Insert<T>(data);
        }

        public long Add(List<T> datas)
        {
            return NDRS.Insert<T>(datas);
        }
   
        public int Update(T data)
        {
            return NDRS.Update<T>(data);
        }

        public int Update(List<T> data)
        {
            return NDRS.Update<T>(data);
        }

        public int Delete(T data)
        {
            return NDRS.Delete<T>(data);
        }

        public int Delete(List<T> data)
        {
            return NDRS.Delete<T>(data);
        } 

        public List<T> GetAll(object condition = null)
        {
            return NDRS.GetAll<T>(condition).ToList();
        }
    }
}
