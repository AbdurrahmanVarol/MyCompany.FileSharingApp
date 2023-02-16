using System.ComponentModel.DataAnnotations;

namespace MyCompany.FileSharingApp.MVC.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage = "Password and Confirm Password are not equal.")]
        public string ConfirmPassword { get; set; }
    }
}
