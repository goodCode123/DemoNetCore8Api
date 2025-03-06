using Infrastructure.Attributes;
using Repository.InterFace;
using Repository.Model;

namespace Repository.Dal
{

    [PerLifetimeScopeService]
    public class GroupMenuAuthDal : GenericCrudDal<GroupMenuAuthEntity>, IGroupMenuAuthDal
    {
        /// <summary>
        /// 若存在不新增，不存在則新增
        /// </summary>
        /// <param name="groupMenuAuthEntities"></param>
        /// <returns></returns>
        public int BatchAdd(List<GroupMenuAuthEntity> groupMenuAuthEntities)
        {
            var sql = @"IF NOT EXISTS (
                           SELECT 1
                           FROM GroupMenuAuth
                           WHERE GroupMasterId = @GroupMasterId
                             AND MenuItemId = @MenuItemId
                       )
                       BEGIN
                           INSERT INTO GroupMenuAuth (
                               GroupMasterId
                               ,MenuItemId
                           )
                           VALUES (
                               @GroupMasterId
                               ,@MenuItemId
                           );
                       END;
                    ";
            return NDRS.BatchExecute(sql, groupMenuAuthEntities);
        }


        /// <summary>
        /// 取得刪除已經選擇，後來取消的GroupMeau
        /// </summary>
        /// <returns></returns>
        public List<int> GetNotExistMenuAuth(List<GroupMenuAuthEntity> groupMenuAuthEntities)
        {
            const string sql = @"SELECT Id
                                 FROM GroupMenuAuth
                                 WHERE GroupMasterId = @GroupId
                                 	AND MenuItemId NOT IN @MenuItemIds";

            var menuItemIds = groupMenuAuthEntities.Select(x => x.MenuItemId).ToList();

            var result = NDRS.GetList<int>(sql, new { GroupId = groupMenuAuthEntities[0].GroupMasterId, MenuItemIds = menuItemIds });

            return result;
        }

        /// <summary>
        /// 刪除已經選擇，後來取消的GroupMeau
        /// </summary>
        /// <returns></returns>
        public int BeatchDeleteForCanelGroupMenuAuth(List<int> Ids)
        {
            const string sql = @"DELETE
                                 FROM GroupMenuAuth
                                 WHERE Id IN @Ids";
            var result = NDRS.BatchExecute(sql, new { Ids=Ids });

            return result;
        }

        /// <summary>
        /// 若使用將全部的選單取消，將此groupId全部刪除
        /// </summary>
        /// <returns></returns>
        public int BeatchDeleteForGroup(int groupId)
        {
            var sql = @"DELETE
                        FROM GroupMenuAuth
                        WHERE GroupMasterId = @GroupId";

            var result = NDRS.BatchExecute(sql, new { GroupId = groupId});

            return result;
        }

        public List<int> GetGroupMenuAuthByGroup(GroupMenuAuthEntity data)
        {
            const string sql = @"SELECT MenuItemId
                                 FROM GroupMenuAuth
                                 WHERE GroupMasterId = @GroupId"
            ;

            var result = NDRS.GetList<int>(sql, new { GroupId = data.GroupMasterId });

            return result;
        }
    }
}
