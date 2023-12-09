using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace NewsAggregatingProject.Models
{
    public class CreateNewModel
    {
        [Required(ErrorMessage = "Incorrect input")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Incorrect input")]
        [StringLength(500, MinimumLength=10)]
        public string Description { get; set; }
        [Required]
        public Guid SourceId { get; set; }
        public List<SelectListItem>? AvailableSources { get; set; }
    
    }
}
