using System.ComponentModel.DataAnnotations;

namespace Schedule_MVC.Models.Admin
{
    public class UserDisplayModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
