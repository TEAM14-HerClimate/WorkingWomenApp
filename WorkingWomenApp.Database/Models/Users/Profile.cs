using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingWomenApp.Database.Core;

namespace WorkingWomenApp.Database.Models.Users
{
    public class UserProfile:Entity
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public bool IsPregnant { get; set; }
        public bool IsMother { get; set; }
        public Profession Profession { get; set; }
        public string ProfessionDescription { get; set; }
        public bool IsBreastfeeding { get; set; }
        public int NumberOfChidren { get; set; }
        public int PregnancyWeeks { get; set; }




        public virtual ApplicationUser User { get; set; }
    }

    public class Children : Entity
    {
        [ForeignKey("Profile")]
        public Guid ProfileId { get; set; }
        public DateTime? ChildDob { get; set; }
        public bool IsMother { get; set; }

        public virtual UserProfile Profile { get; set; }
    }
}
