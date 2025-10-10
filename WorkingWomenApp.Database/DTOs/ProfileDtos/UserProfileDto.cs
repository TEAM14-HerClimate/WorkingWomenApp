using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using WorkingWomenApp.Database.enums;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.Database.DTOs.ProfileDtos
{
    public class UserProfileDto
    {
        public Guid UserId { get; set; }
       
        public bool IsPregnant { get; set; }
        public bool IsMother { get; set; }
        public Profession Profession { get; set; }
        public string ProfessionDescription { get; set; }
        public bool IsBreastfeeding { get; set; }
        public int NumberOfChildren { get; set; }
        public int PregnancyWeeks { get; set; }
        public byte[] ProfilePicture { get; set; }

        public ApplicationUser User { get; set; }
    }
}
