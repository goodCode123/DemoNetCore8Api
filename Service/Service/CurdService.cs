using Infrastructure.Attributes;
using Repository.InterFace;
using Service.Model;
using Services.EnumsModel;
using Services.InterFace;

namespace Services.Service
{
    [PerLifetimeScopeService]
    public  class CurdService<T> : ICrudService<T> where T : class
    {
        private readonly ICrudDal<T> crudDal;

        public CurdService(ICrudDal<T> _crudDal) {
            crudDal = _crudDal;
        }
        public ApiResponseModel Add(T data)
        {
            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = crudDal.Add(data),
                Msg = string.Empty
            }; 

            return apiResponse;
        }

        public ApiResponseModel Add(List<T> datas)
        {
            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = crudDal.Add(datas),
                Msg = string.Empty
            };

            return apiResponse;
        }

        public ApiResponseModel Delete(T data)
        {
            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = crudDal.Delete(data),
                Msg = string.Empty
            };

            return apiResponse;
        }

        public ApiResponseModel Delete(List<T> data)
        {
            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = crudDal.Delete(data),
                Msg = string.Empty
            };

            return apiResponse;
        }

        public ApiResponseModel Update(T data)
        {
            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = crudDal.Update(data),
                Msg = string.Empty
            };

            return apiResponse;
        }

        public ApiResponseModel Update(List<T> data)
        {
            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = crudDal.Update(data),
                Msg = string.Empty
            };

            return apiResponse;
        }

        public ApiResponseModel GetAll(object condition = null)
        {
            ApiResponseModel apiResponse = new ApiResponseModel()
            {
                Code = ApiCodeEnum.Success.ToString(),
                Data = crudDal.GetAll(condition),
                Msg = string.Empty
            };

            return apiResponse;
        }
    }
}
