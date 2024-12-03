using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LinksApp.Models
{

    public class LinkModel
    {   
        [Key]
        public int id {get; set;} 

        [Range(1, 4, ErrorMessage = "Category ID must be a number between 1 and 4.")]
        public int category_id {get; set;}

        [Required(ErrorMessage = "Label is required.")]
        [MaxLength(100, ErrorMessage = "Label cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Label must be alphanumeric.")]
        public string label {get; set;}

        [Required(ErrorMessage = "Link is required.")]
        [MaxLength(200, ErrorMessage = "Link cannot exceed 200 characters.")]
        [Url(ErrorMessage = "Link must be in a valid HTTP/HTTPS format.")]
        public string link {get; set;}

        public Boolean pinned {get; set;}

    
    }
}
