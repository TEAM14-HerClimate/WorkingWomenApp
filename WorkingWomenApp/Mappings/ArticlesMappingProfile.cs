using AutoMapper;
using System.Diagnostics;
using WorkingWomenApp.Database.DTOs.UserDtos;
using WorkingWomenApp.Database.DTOs.ViewModels;
using WorkingWomenApp.Database.Models.Climate;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.Mappings
{
    public class ArticlesMappingProfile:Profile

    {
        public ArticlesMappingProfile()
        {
            CreateMap<ArticleDto, Article>().ReverseMap(); 
            //.ForMember(dest => dest.Attendees, opt => opt.MapFrom(src => src.Attendees.Select(attendee => new Attendee {PersonId  = attendee.PersonId }).ToList()))
            //.ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.Participants.Select(participant => new Participant { PersonId = participant.PersonId }).ToList()))



            CreateMap<Article, ArticleDto>().ReverseMap();
            CreateMap<UserCreateDto, ApplicationUser>().ReverseMap().ForMember(dest => dest.Roles,
                opt => opt.MapFrom(src => src.UserRoleMappings.Select(participant => new UserRoleMapping() { Id = participant.SecurityRole.Id }).ToList())); ;
            CreateMap< ApplicationUser, UserCreateDto>().ReverseMap().ForMember(dest => dest.UserRoleMappings, 
                opt => opt.MapFrom(src => src.Roles.Select(participant => new UserRoleDto() { RoleId = participant.RoleId }).ToList()));
            CreateMap<UserProfileDtos, UserProfile>().ReverseMap();
            CreateMap<UserProfile, UserProfileDtos>().ReverseMap();
        }


    }
}
