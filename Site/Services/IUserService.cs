namespace Authentication.Local.Services
{
    using System.Threading.Tasks;
    using Site.Models;

    public interface IUserService
    {
        Task<(bool, AccountViewModel)> ValidateUserCredentialsAsync(string Account, string Password);
    }
}