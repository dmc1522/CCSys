using LasMargaritas.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LasMargaritas.DL.EF
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("LasMargaritasEFDb")
        {

        }
    }
}
