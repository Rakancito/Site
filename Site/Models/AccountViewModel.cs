using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class AccountViewModel
    {
        [Key]
        public int iId { get; set; }
        [Required]
        [Display(Name = "Account")]
        public string Account { get; set; }
        [MinLength(5, ErrorMessage = "Necesita colocar al menos 5 caracteres")]
        [StringLength(100, ErrorMessage = "Necesita una contraseña con menos de 5 caracteres.")]
        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Password Actual")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [Required]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
        [Display(Name = "Coins")]
        public int Coins { get; set; }
        [Display(Name = "DateTime")]
        public DateTime GetDateTime { get; set; }
        [Display(Name = "RemoveCode")]
        public string RemoveCode { get; set; }
        public AccountViewModel()
        {
        }
        public AccountViewModel(string sAccount, string sPassword, string sEmail, string sRemoveCode, int iCoins)
        {
            this.Account = sAccount;
            this.Password = sPassword;
            this.Email = sEmail;
            this.RemoveCode = sRemoveCode;
            this.Coins = iCoins;
        }

    }
}
