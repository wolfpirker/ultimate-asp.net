using HotelListingAPI.VSCode.Models.Hotel;

namespace HotelListingAPI.VSCode.Models.Country
{
    public class CountryDto
    {
        public int Id {get; set;}
        public string Name { get; set; }
        public string ShortName { get; set; }
        // public virtual IList<Hotel> Hotels { get; set; }
        public List<HotelDto> Hotels{get; set;}
    }
}