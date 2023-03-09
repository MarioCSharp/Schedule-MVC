using System.ComponentModel.DataAnnotations;

namespace Schedule_MVC.Data.Models
{
    public class Work
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int ClassId { get; set; }
        [Required]
        [MaxLength(32)]
        public string Subject { get; set; }
    }
}
