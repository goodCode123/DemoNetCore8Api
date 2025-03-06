using Dapper;

namespace Repository.Model
{
    [Table("MenuItem")]
    public class MenuItemEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Url { get; set; }

        public string Icon { get; set; }

        public int? ParentId { get; set; }

        public bool Disable{ get; set; }

    }
}
