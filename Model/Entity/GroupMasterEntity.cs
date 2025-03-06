using Dapper;

namespace Repository.Model
{

    [Table("GroupMaster")]
    public class GroupMasterEntity
    {
        [Key]
        [IgnoreInsert]
        public int Id { get; set; }
        public string? GroupName { get; set; }

        [IgnoreInsert]
        [IgnoreUpdate]
        public DateTime CreateDate { get; set; }


        public bool Disable { get; set; }
    }
}
