using Service.Model;
using Services.Model.Login;

namespace Services.InterFace
{
    public interface ILoginService
    {
        public ApiResponseModel Login(LoginModel data);

        ApiResponseModel Logout(int id);
    }
}
