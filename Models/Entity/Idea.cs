using System.ComponentModel.DataAnnotations;

namespace DeveloperNotebookAPI.Models.Entity
{
    public class Idea : AuditableEntity
    {
        [StringLength(30)]
        [Required]
        public string Name {get; set;}

        public string Description {get; set;}
    }
}