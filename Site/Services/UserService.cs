namespace Authentication.Local.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Site.Models;

    public class UserService : IUserService
    {
        private readonly IDictionary<string, AccountViewModel> _users;

        public UserService(IDictionary<string, AccountViewModel> users) => _users = users;

        public Task<(bool, AccountViewModel)> ValidateUserCredentialsAsync(string username, string password)
        {
            var isValid = _users.ContainsKey(username) && 
                          string.Equals(_users[username].Password, password, StringComparison.Ordinal);
            var result = (isValid, isValid ? _users[username] : null);
            return Task.FromResult(result);
        }
    }
}