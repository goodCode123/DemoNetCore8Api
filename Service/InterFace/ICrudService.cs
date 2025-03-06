using Service.Model;

namespace Services.InterFace
{
    public interface ICrudService<T> where T : class
    {
        public ApiResponseModel GetAll(object condition = null);

        public ApiResponseModel Update(T data);

        public ApiResponseModel Update(List<T> data);

        public ApiResponseModel Add(T data);

        public ApiResponseModel Add(List<T> datas);

        public ApiResponseModel Delete(T data);

        public ApiResponseModel Delete(List<T> data);
    }
}
