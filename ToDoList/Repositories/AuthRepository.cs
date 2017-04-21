using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using ToDoList.ViewModels;

namespace ToDoList.Repositories
{
    public class AuthRepository : IDisposable
    {
        private ApplicationDbContext _ctx;

        private UserManager<ApplicationUser> _userManager;

        public AuthRepository()
        {
            _ctx = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));

        }

        public async Task<IdentityResult> RegisterUser(UserViewModel userModel)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.UserName

            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public ApplicationUser FindUser(string userName, string password)
        {
            ApplicationUser user = _userManager.Find<ApplicationUser, string>(userName, password);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}