using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListingAPI.VSCode.Data;

namespace HotelListingAPI.VSCode.Models.Country
{
    public class GetCountryDto
    {
        public int Id {get; set;}
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}