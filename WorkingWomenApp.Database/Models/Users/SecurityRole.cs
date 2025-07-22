using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWomenApp.Database.Models.Users
{
    public class SecurityRole : IdentityRole<Guid>
    {
        public Guid Id { get; set; }
        public SecurityRole() { }
    }
}
