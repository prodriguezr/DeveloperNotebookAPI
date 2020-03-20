using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperNotebookAPI.Models.Entity
{
    public class Category : AuditableEntity
    { 
        [StringLength(40)]
        [Required]
        [Remote("IsCategoryNameExist", "Categories", AdditionalFields = "ActiveRecord", 
                ErrorMessage = "Category name already exists")] 
        public string Name {get; set;}
    }
}