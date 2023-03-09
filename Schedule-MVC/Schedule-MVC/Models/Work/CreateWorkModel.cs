using System.ComponentModel.DataAnnotations;

namespace Schedule_MVC.Models.Work
{
    public class CreateWorkModel
    {
        [Required]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Клас")]
        public int ClassId { get; set; }
        [Required]
        [Display(Name = "Предмет")]
        public string Subject { get; set; }

        public List<ClassDisplayModel> Classes { get; set; }

        public string Error { get; set; }
    }
}
