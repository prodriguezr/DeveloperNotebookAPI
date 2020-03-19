using System.ComponentModel.DataAnnotations;

namespace DeveloperNotebookAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(8)]
        public string Username { get; set; }

        [StringLength(30)]
        [Required]
        public string Password { get; set; }
        public string Token { get; set; }
    }
}