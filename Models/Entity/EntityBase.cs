using System;
using System.ComponentModel.DataAnnotations;

namespace DeveloperNotebookAPI.Models
{
    public class EntityBase
    {
        public int Id {get; set;}
        
        [Required]
        public DateTime CreationDate {get; set;}
        
        public DateTime? ModificationDate {get; set;}
        
        [Required]
        public bool ActiveRecord {get; set;}
        
        [Required]
        public int UserId {get; set;}
        
    }
}