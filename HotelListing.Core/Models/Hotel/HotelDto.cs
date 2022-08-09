using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListingAPI.VSCode.Models.Hotel
{

    public class HotelDto : BaseHotelDto, IBaseDto
    {
        public int Id { get; set; }
    }
}