using System;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Site.Models
{
    public class UserViewModel
    {
        [Key]
        public int iId { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Necesita colocar al menos 4 caracteres")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Necesita una cuenta con menos de 4 caracteres.")]
        [Display(Name = "Account")]
        public string Account { get; set; }
        [MinLength(5, ErrorMessage = "Necesita colocar al menos 5 caracteres")]
        [StringLength(100, ErrorMessage = "Necesita una contraseña con menos de 5 caracteres.")]
        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Required]
        [Compare("Password", ErrorMessage = "Las contraseñas introducidas no son iguales")]
        public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
        [Required]
        [MinLength(7, ErrorMessage = "Necesita colocar al menos 7 caracteres")]
        [Display(Name = "RemoveCode")]
        [StringLength(7, ErrorMessage = "Necesita un código de al menos 7 dígitos")]
        public string RemoveCode { get; set; }
        [Display(Name = "Coins")]
        public int Coins { get; set; }
        [Display(Name = "DateTime")]
        public DateTime GetDateTime { get; set; }

        public UserViewModel()
        {
        }
        public UserViewModel(string sAccount, string sPassword, string sEmail, string sRemoveCode)
        {
            this.Account = sAccount;
            this.Password = sPassword;
            this.Email = sEmail;
            this.RemoveCode = sRemoveCode;
        }
    }
}
