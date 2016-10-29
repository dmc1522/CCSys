using LasMargaritas.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LasMargaritas.DL.EF
{
    public class AuthRepository : IDisposable
    {
        private AuthContext context;

        private UserManager<IdentityUser> userManager;

        public AuthRepository()
        {
            context = new AuthContext();
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
        }

        public async Task<IdentityResult> RegisterUser(User user)
        {
            IdentityUser identityUser = new IdentityUser
            {
                UserName = user.UserName
            };

            var result = await userManager.CreateAsync(identityUser, user.Password);
            identityUser = await userManager.FindAsync(user.UserName, user.Password);
            userManager.AddToRole(identityUser.Id, "Admin");
            //TODO: change this once we have role assignment
            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await userManager.FindAsync(userName, password);

            return user;
        }
        public async Task<IList<string>> UserRoles(string userId)
        {
            IList<string> roles = await userManager.GetRolesAsync(userId);

            return roles;
        }
        public void Dispose()
        {
            context.Dispose();
            userManager.Dispose();

        }
    }
  
}
