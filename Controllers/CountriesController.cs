using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListingAPI.VSCode.Data;
using HotelListingAPI.VSCode.Models.Country;
using HotelListingAPI.VSCode.Models.Hotel;
using AutoMapper;
using HotelListingAPI.VSCode.Contract;
using Microsoft.AspNetCore.Authorization;

namespace HotelListingAPI.VSCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;

        public CountriesController(IMapper mapper, ICountriesRepository countriesRepository)
        {
            this._mapper = mapper;
            this._countriesRepository = countriesRepository;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries()
        {
            // Select * from Countries,
            var countries = await _countriesRepository.GetAllAsync();
            // Note: we need a list, see return type!
            // AutoMapper do not alert about that!
            var records = _mapper.Map<List<CountryDto>>(countries);
            return Ok(records);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            // var country = await _context.Countries.FindAsync(id);   
            // now we need to also include the list of hotels!:
            var country = await _countriesRepository.GetAsync(id);

            if (country == null)
            {
                return NotFound();
            }
            var record = _mapper.Map<CountryDto>(country);
            return Ok(record);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest();
            }

            // _context.Entry(country).State = EntityState.Modified;
            var country = await _countriesRepository.GetAsync(id);

            if (country == null){
                return NotFound();
            }

            _mapper.Map(updateCountryDto, country); // ->  we still need that mapper!
            

            try
            {
                // Note: even after our refactoring: we should still catch
                // the exception DbUpdateConcurrencyException, like below!
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
          
            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountryDto)
        {
            var country = _mapper.Map<Country>(createCountryDto);

            await _countriesRepository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles ="Administrator,User")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            await _countriesRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRepository.Exists(id);
        }
    }
}
