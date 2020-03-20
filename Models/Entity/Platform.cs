using System.ComponentModel.DataAnnotations;

namespace DeveloperNotebookAPI.Models.Entity
{
    public class Platform : AuditableEntity
    {
        [StringLength(20)]
        [Required]
        public string Name {get; set;}
    }
}