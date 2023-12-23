using System.ComponentModel.DataAnnotations;
using Entities.Enums;

namespace Entities
{
    public class User
    {
        public int UserId { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? UserName { get; set; }
        public UserType UserType { get; set; }
    }
}
