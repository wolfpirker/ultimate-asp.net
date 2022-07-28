using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListingAPI.VSCode.Data;
using HotelListingAPI.VSCode.Models.Country;

namespace HotelListingAPI.VSCode.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap();
        }
    }
}