using Infrastructure.Attributes;
using Repository.Dapper;
using Repository.InterFace;
using Repository.Model;

namespace Repository.Dal
{
    [PerLifetimeScopeService]
    public class LayOutDal : DapperHelper, ILayOutDal
    {
        public List<MenuItemEntity> GetSideBarMeauList()
        {
            return NDRS.GetAll<MenuItemEntity>(new { Disable  = false}).ToList();
        }

        public List<MenuItemEntity> GetSideBarMeauList(string Id)
        {
            const string sql = @"SELECT mi.*
                                 FROM GroupMember gm
                                 JOIN GroupMenuAuth gma ON gm.GroupMasterId = gma.GroupMasterId
                                 JOIN MenuItem mi ON mi.Id = gma.MenuItemId
                                 WHERE gm.UserMasterId = @Id
                                 	AND mi.Disable = 0;";
            return NDRS.GetList<MenuItemEntity>(sql, new { Id = Id}).ToList();
        }

        public List<MenuItemEntity> GetMenus()
        {
            return NDRS.GetAll<MenuItemEntity>().ToList();
        }

        public int UpdateMeauList(MenuItemEntity menuItem)
        {
            return NDRS.Update<MenuItemEntity>(menuItem);
        }

        public long AddMeauList(MenuItemEntity menuItem)
        {
            return NDRS.Insert<MenuItemEntity>(menuItem);
        }
    }
}
