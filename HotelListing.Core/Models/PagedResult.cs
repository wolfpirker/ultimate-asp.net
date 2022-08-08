using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListingAPI.VSCode.Models
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; } // now many pages
        public int PageNumber { get; set; } // which page
        public int RecordNumber { get; set; } // record num per page
        public List<T> Items { get; set; } // the actual items
    }
}