using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.VSCode.Models.Country
{
    public class CreateCountryDto : BaseCountryDto
    {

        
    }

    public class UpdateCountryDto : BaseCountryDto
    {
        public int Id { get; set; }
    }
}