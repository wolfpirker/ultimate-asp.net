using HotelListingAPI.VSCode.Data;
using HotelListingAPI.VSCode.Models.Country;

namespace HotelListingAPI.VSCode.Contract
{
    public interface ICountriesRepository : IGenericRepository<Country>{  
        Task<CountryDto> GetDetails(int id);      
    }
}