using System.ComponentModel.DataAnnotations;

namespace lr10.Models
{
    public class ConsultationModel
    {
        [Required (ErrorMessage = "Name not specifed")]
        public string Name { get; set; }
        [Required (ErrorMessage = "Email not specifed")]
        public string Email { get; set; }
        [Required (ErrorMessage = "Date not specifed")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Language not specifed")]
        public string SelectedLanguage { get; set; } = "Basis";
       
    }
}
