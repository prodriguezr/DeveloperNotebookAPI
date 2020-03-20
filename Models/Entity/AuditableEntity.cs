using System;
using System.ComponentModel.DataAnnotations;

namespace DeveloperNotebookAPI.Models.Entity
{
    public class AuditableEntity : EntityBase 
    {
        [Required]
        public DateTime CreationDate {get; set;}
        
        public DateTime? ModificationDate {get; set;}
        
        [Required]
        public bool ActiveRecord {get; set;}
        
        [Required]
        public int UserId {get; set;}
    }
}