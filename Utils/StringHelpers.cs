using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelListingAPI.VSCode.Utils
{
    public static class StringHelpers
    {
        public static string SafeReplace(this string input, string find, string replace, bool matchWholeWord) {
            string searchString = find.StartsWith("@") ? $@"@\b{find.Substring(1)}\b" : $@"\b{find}\b"; 
            string textToFind = matchWholeWord ? searchString : find;
            return Regex.Replace(input, textToFind, replace);
        }
    }
}