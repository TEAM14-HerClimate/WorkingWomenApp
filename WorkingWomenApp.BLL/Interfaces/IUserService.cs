using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWomenApp.BLL.Interfaces
{
       public interface IUserService
    {
        Task<bool> Register(string userName, string password);

        Task<bool> Login(string userName, string password);

        Task Logout();
    }
}
