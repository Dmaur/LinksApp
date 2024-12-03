using System.ComponentModel.DataAnnotations;

namespace LinksApp.Models
{
    public class CatModel
    {
        [Key]
        public int id { get; set; }

        [Required] // Ensures the name field is mandatory
        [MaxLength(20, ErrorMessage = "Name cannot exceed 20 characters.")] // Maximum length constraint
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters.")] // Letter-only validation
        public string name { get; set; }
    }
}