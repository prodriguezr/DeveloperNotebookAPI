using System.ComponentModel.DataAnnotations;

namespace DeveloperNotebookAPI.Models
{
    public class Platform : EntityBase
    {
        [StringLength(20)]
        [Required]
        public string Name {get; set;}
    }
}