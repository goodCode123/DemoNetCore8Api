namespace Repository.InterFace
{
    public interface ICrudDal<T> where T : class
    {
        public List<T> GetAll(object condition = null);

        public int Update(T data);

        public int Update(List<T> data);

        public long Add(T data);

        public long Add(List<T> datas);

        public int Delete(T data);

        public int Delete(List<T> data);
    }
}
