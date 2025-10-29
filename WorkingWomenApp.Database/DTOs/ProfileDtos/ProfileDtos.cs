using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingWomenApp.Database.enums;

namespace WorkingWomenApp.Database.DTOs.ProfileDtos
{
    public class UserProfileDtos
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Display(Name = "Are you pregnant?")]
        public bool IsPregnant { get; set; }
        [Display(Name = "Are you a mother?")]
        public bool IsMother { get; set; }
        [Display(Name = "What do you do professionally/ do for a living?")]
        public Profession Profession { get; set; }
        [Display(Name = "Describe what you professionally/ do for a living")]
        public string ProfessionDescription { get; set; }
        [Display(Name = "Are you breastfeeding?")]
        public bool IsBreastfeeding { get; set; }
        public int NumberOfChidren { get; set; }
        public int PregnancyWeeks { get; set; }
    }
}
