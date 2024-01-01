using Microsoft.Extensions.Configuration.UserSecrets;
using System.ComponentModel.DataAnnotations;

namespace RapiddIdenity.Models.Authenication.SignUp
{
    public class RegisterUser
    {

        [Required(ErrorMessage = "User name is required")]
        [MinLength(6)]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Role is Missing")]
        public Roles Role { get; set; }

    }
}
