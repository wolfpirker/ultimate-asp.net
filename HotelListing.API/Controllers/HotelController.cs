using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListingAPI.VSCode.Contract;
using HotelListingAPI.VSCode.Data;
using HotelListingAPI.VSCode.Models.Hotel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using HotelListingAPI.VSCode.Models;

namespace HotelListingAPI.VSCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HotelController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHotelsRepository _hotelsRepository;

        public HotelController(IMapper mapper, IHotelsRepository hotelsRepository)
        {
            this._hotelsRepository = hotelsRepository;     
            this._mapper = mapper;       
        }

        // GET: api/Hotels
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            var hotels = await _hotelsRepository.GetAllAsync<List<HotelDto>>();
            return Ok(hotels);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync<HotelDto>(id);
            return Ok(hotel);
        }

       

        // PUT: api/Hotels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelDto hotelDto)
        {
            try
            {
                await _hotelsRepository.UpdateAsync(id, hotelDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
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

         // POST: api/Hotels
        [HttpPost]
        public async Task<ActionResult<HotelDto>> PostHotel(CreateHotelDto hotelDto)
        {
            var hotel = await _hotelsRepository.AddAsync<CreateHotelDto, HotelDto>(hotelDto);
            return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            await _hotelsRepository.DeleteAsync(id); 
            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelsRepository.Exists(id);
        }
    }
}