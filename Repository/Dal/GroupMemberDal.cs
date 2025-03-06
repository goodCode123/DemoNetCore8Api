using Infrastructure.Attributes;
using Repository.InterFace;
using Repository.MergeModel;
using Repository.Model;

namespace Repository.Dal
{
    [PerLifetimeScopeService]
    public class GroupMemberDal : GenericCrudDal<GroupMemberEntity>, IGroupMemberDal
    {
        public int BatchAdd(List<GroupMemberEntity> groupMenuAuthEntities)
        {
            var sql = @"IF NOT EXISTS (
                                SELECT 1
                                FROM GroupMember 
                                WHERE GroupMasterId = @GroupMasterId
                                  AND UserMasterId = @UserMasterId
                            )
                            BEGIN
                                INSERT INTO dbo.GroupMember (
                                    GroupMasterId,
                                    UserMasterId,
                                    CreateUser
                                )
                                VALUES (
                                    @GroupMasterId,
                                    @UserMasterId,
                                    @CreateUser
                                );
                            END ";
            return NDRS.BatchExecute(sql, groupMenuAuthEntities);
        }

        public List<DeleteGroupMemberModel> GetNonGroupMembers(GroupMemberEntity groupMenuAuthEntities)
        {
            const string sql = @"SELECT um.*
                                 	,gm.Id AS GroupMemberId
                                 FROM UserMaster um
                                 LEFT JOIN GroupMember gm ON um.Id = gm.UserMasterId
                                 	AND gm.GroupMasterId = @GroupMasterId
                                 WHERE um.Id NOT IN (
                                 		SELECT UserMasterId
                                 		FROM GroupMember
                                 		WHERE GroupMasterId = @GroupMasterId
                                 		)";

            return NDRS.GetList<DeleteGroupMemberModel>(sql, groupMenuAuthEntities).ToList();
        }

        public List<DeleteGroupMemberModel> GetGroupMembersById(GroupMemberEntity groupMenuAuthEntities)
        {
            const string sql = @"SELECT um.*
                                	,gm.Id AS GroupMemberId
                                FROM UserMaster um
                                INNER JOIN GroupMember gm ON um.Id = gm.UserMasterId
                                WHERE gm.GroupMasterId = @GroupMasterId";

            return NDRS.GetList<DeleteGroupMemberModel>(sql, groupMenuAuthEntities).ToList();
        }

        /// <summary>
        /// 根據提供的 ID 列表，從資料庫中刪除多個群組成員。
        /// </summary>
        /// <param name="Ids">要刪除的群組成員 ID 列表。</param>
        /// <returns>刪除操作影響的行數。</returns>
        public int DeleteGroupMembers(List<int> Ids)
        {
            const string sql = @"DELETE
                                 FROM GroupMember
                                 WHERE Id in @Ids";

            return NDRS.BatchExecute(sql, new { Ids = Ids });
        }

        public int DeleteGroupMembersByGroupMasterId(int id)
        {
            const string sql = @"DELETE
                                FROM GroupMember
                                WHERE GroupMasterId = @id";

            return NDRS.BatchExecute(sql, new { id = id }); 
        }
    }
}
