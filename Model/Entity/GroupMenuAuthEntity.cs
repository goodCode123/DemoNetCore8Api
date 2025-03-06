using Dapper;

namespace Repository.Model
{
    [Table("GroupMenuAuth")]
    public class GroupMenuAuthEntity
    {
        [Key]
        [Column("Id")]
        public int? Id { get; set; } 
        public required int GroupMasterId { get; set; }

        public int? MenuItemId { get; set; }

        [IgnoreInsert]
        public DateTime? CreateDate { get; set; }

    }
}
