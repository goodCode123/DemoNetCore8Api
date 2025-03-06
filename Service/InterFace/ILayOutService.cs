using Repository.Model;
using Service.Model;

namespace Services.InterFace
{
    public interface ILayOutService
    {
        public ApiResponseModel GetSideBarMeauList(string Id);

        public ApiResponseModel GetMenus();

        public ApiResponseModel UpdateMeauList(MenuItemEntity menuItem);

        public ApiResponseModel AddMeauList(MenuItemEntity menuItem);

        public ApiResponseModel GetNoDisableMeaus();

        public List<MenuItemEntity> GetSideBarMeauListAuthorize(string Id);
    }
}
