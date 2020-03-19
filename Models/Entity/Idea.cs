using System.ComponentModel.DataAnnotations;

namespace DeveloperNotebookAPI.Models
{
    public class Idea : EntityBase
    {
        [StringLength(30)]
        [Required]
        public string Name {get; set;}

        public string Description {get; set;}
    }
}