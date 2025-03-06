using Dapper;

namespace Repository.Model
{
    [Table("RefreshToken")]
    public class RefreshTokenEntity
    {

        [Key]
        public int Id { get; set; }

        public required int UserMasterId { get; set; }

        public required string RefreshToken { get; set; }

     
        public required string Token { get; set; }

        public required DateTime ExpiryDate { get; set; }

        public bool IsRevoked { get; set; } = false;

        [IgnoreInsert]
        [IgnoreUpdate]
        public DateTime CreateDate { get; set; }

        [IgnoreInsert]
        public DateTime? ModifyDate { get; set; }

    }
}
