using AutoMapper;
using System.Diagnostics;
using WorkingWomenApp.Database.DTOs.ViewModels;
using WorkingWomenApp.Database.Models.Climate;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.Mappings
{
    public class ArticlesMappingProfile:Profile

    {
        public ArticlesMappingProfile()
        {
            CreateMap<ArticleDto, Article>().ReverseMap(); ;
            //.ForMember(dest => dest.Attendees, opt => opt.MapFrom(src => src.Attendees.Select(attendee => new Attendee {PersonId  = attendee.PersonId }).ToList()))
            //.ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.Participants.Select(participant => new Participant { PersonId = participant.PersonId }).ToList()))



            CreateMap<Article, ArticleDto>().ReverseMap();

            CreateMap<UserProfileDtos, UserProfile>().ReverseMap();
            CreateMap<UserProfile, UserProfileDtos>().ReverseMap();
        }


    }
}
