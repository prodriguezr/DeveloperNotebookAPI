using System.ComponentModel.DataAnnotations;

namespace DeveloperNotebookAPI.Models.Entity
{
    public class Command : AuditableEntity
    {
        [Required]
        [StringLength(50)]
        public string Name {get; set;}

        [Required]
        public int PlatformId {get; set;}

        [Required]
        public int CategoryId {get; set;}

        [Required]
        public string CommandLine {get; set;}
    }
}