using System;
using System.ComponentModel.DataAnnotations;

namespace DeveloperNotebookAPI.Models.Entity
{
    public class EntityBase
    {
        [Required]
        public int Id {get; set;}
    }
}