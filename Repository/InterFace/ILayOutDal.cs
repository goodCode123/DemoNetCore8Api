using Repository.Model;

namespace Repository.InterFace
{
    public interface ILayOutDal
    {
        public List<MenuItemEntity> GetSideBarMeauList();

        public List<MenuItemEntity> GetMenus();
        public int UpdateMeauList(MenuItemEntity menuItem);

        public long AddMeauList(MenuItemEntity menuItem);

        public List<MenuItemEntity> GetSideBarMeauList(string Id);
    }
}
