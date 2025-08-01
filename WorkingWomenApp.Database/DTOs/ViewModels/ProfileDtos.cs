using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingWomenApp.Database.enums;

namespace WorkingWomenApp.Database.DTOs.ViewModels
{
    public class UserProfileDtos
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsPregnant { get; set; }
        public bool IsMother { get; set; }
        public Profession Profession { get; set; }
        public string ProfessionDescription { get; set; }
        public bool IsBreastfeeding { get; set; }
        public int NumberOfChidren { get; set; }
        public int PregnancyWeeks { get; set; }
    }
}
