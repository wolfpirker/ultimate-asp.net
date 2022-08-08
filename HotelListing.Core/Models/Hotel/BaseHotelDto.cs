using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.VSCode.Models.Hotel
{
    public class BaseHotelDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public double? Rating { get; set; }

        //[ForeignKey(nameof(CountryId))]
        [Required]
        [Range(1, int.MaxValue)]
        public int CountryId { get; set; }
    }
}