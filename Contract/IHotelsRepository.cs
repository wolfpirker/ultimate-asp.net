using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListingAPI.VSCode.Data;

namespace HotelListingAPI.VSCode.Contract
{
    public interface IHotelsRepository : IGenericRepository<Hotel>
    {
        
    }
}