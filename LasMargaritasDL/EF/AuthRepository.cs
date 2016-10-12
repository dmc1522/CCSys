using LasMargaritas.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace LasMargaritas.DL.EF
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;

        private UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(User user)
        {
            IdentityUser identityUser = new IdentityUser
            {
                UserName = user.UserName
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
  
}
