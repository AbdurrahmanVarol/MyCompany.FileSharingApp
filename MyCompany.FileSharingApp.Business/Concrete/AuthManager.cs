using MyCompany.FileSharingApp.Business.Abstract;
using MyCompany.FileSharingApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        public User Login(LoginModel loginModel)
        {
            if (loginModel is null)
                return null;

            var user = _userService.GetByUserName(loginModel.UserName);
            if (user is null)
                return null;

            if (!VerifyPasswordHash(loginModel.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using var hmach = new System.Security.Cryptography.HMACSHA512(userPasswordSalt);
            var compuredHash = hmach.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < compuredHash.Length; i++)
            {
                if (compuredHash[i] != userPasswordHash[i])
                    return false;
            }
            return true;
        }

        public User Register(UserModel userModel)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userModel.Password, out passwordHash, out passwordSalt);

            var user = new User
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserName = userModel.UserName,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,

            };

            _userService.Add(user);
            return user;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmach = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmach.Key;
            passwordHash = hmach.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

    }
}
