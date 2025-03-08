using System.ComponentModel.DataAnnotations;

namespace DepInfoCare.Model
{
    public sealed class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
