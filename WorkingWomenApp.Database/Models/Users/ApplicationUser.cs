using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingWomenApp.Database.Core;

namespace WorkingWomenApp.Database.Models.Users
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid Id {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        
        public DateTime MemberSince { get; set; }
        public DateTime PasswordLastChange { get; set; }
    }
}
