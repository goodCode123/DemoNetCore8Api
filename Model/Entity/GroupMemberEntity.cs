using Dapper;

namespace Repository.Model
{

    [Table("GroupMember")]
    public class GroupMemberEntity
    {
        [Key]
        public int Id { get; set; }
        public int GroupMasterId { get; set; }

        public int UserMasterId { get; set; }

        public string? CreateUser { get; set; }

        [IgnoreInsert]
        public DateTime? CreateDate { get; set; } 
    }
}
