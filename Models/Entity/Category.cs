using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperNotebookAPI.Models
{
    public class Category : EntityBase
    { 
        [StringLength(40)]
        [Required]
        [Remote("IsCategoryNameExist", "Categories", AdditionalFields = "ActiveRecord", 
                ErrorMessage = "Category name already exists")] 
        public string Name {get; set;}
    }
}