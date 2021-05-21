using AutoMapper;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Services.MapConfigurations
{
    public class MapHelper : Profile
    {
        public MapHelper()
        {
            CreateMap<Contest, ContestDTO>();
            CreateMap<NewContestDTO, ContestDTO>();
            CreateMap<NewPhotoDTO, Photo>();
            CreateMap<NewUserDTO, User>()/*.ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.FirstName))
                                         .ForMember(d => d.LastName, opt => opt.MapFrom(src => src.LastName))
                                         .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.Username))
                                         .ForMember(d => d.PasswordHash, opt => opt.MapFrom(src => src.Password))*/;
        }
    }
}
