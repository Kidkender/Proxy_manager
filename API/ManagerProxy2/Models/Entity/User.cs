using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManagerProxy2.Models.Entity
{
    [Table("User")]
    public class User
    {
        [Key]
        [Required]
        [Column("Id")]
        public int Id { get; set; } = -1;

        [Required(ErrorMessage = "Chưa nhập tên tài khoản")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        public string? Password { get; set; }
        public string? Name { get; set; }

        public string? Role { get; set; }

        public string? PhotoFileName { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Cấu trúc email không đúng !")]
        public string? Email { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
