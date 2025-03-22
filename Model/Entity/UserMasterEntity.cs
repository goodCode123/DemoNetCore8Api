using Dapper;
using System.Text.Json.Serialization;

namespace Repository.Model
{
    [Table("UserMaster")]
    public class UserMasterEntity
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }

        [IgnoreSelect]
        public string Pwd { get; set; }

        [IgnoreInsert]
        public int? Status { get; set; }

        [IgnoreInsert]
        public DateTime? CreateDate { get; set; }    
    }  
}
