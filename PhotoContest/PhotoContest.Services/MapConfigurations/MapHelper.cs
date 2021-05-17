using AutoMapper;
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
        }
    }
}
