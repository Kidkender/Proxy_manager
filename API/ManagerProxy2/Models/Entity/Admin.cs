using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManagerProxy2.Models.Entity
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int Id { get; set; } = -1;
        public int UserId { get; set; }

        [Required(ErrorMessage = "Chưa nhập tên tài khoản")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        public string? Password { get; set; }
    }
}
