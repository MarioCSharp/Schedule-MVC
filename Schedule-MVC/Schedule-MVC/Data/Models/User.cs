using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Schedule_MVC.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(40)]
        public string FullName { get; set; }
    }
}
