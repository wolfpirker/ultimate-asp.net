using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelListingAPI.VSCode.Data;

namespace HotelListingAPI.VSCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly HotelListingDbContext _context;
        public CountriesController(HotelListingDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Country>>> GetTModels()
        {
            // TODO: Your code here
            await Task.Yield();

            return new List<Country> { };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetTModelById(int id)
        {
            // TODO: Your code here
            await Task.Yield();

            return null;
        }

        [HttpPost("")]
        public async Task<ActionResult<Country>> PostTModel(Country model)
        {
            // TODO: Your code here
            await Task.Yield();

            return null;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTModel(int id, Country model)
        {
            // TODO: Your code here
            await Task.Yield();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Country>> DeleteTModelById(int id)
        {
            // TODO: Your code here
            await Task.Yield();

            return null;
        }
    }
}