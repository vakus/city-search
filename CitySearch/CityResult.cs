using System;
using System.Collections.Generic;
using System.Text;

namespace CitySearch
{
    public class CityResult : ICityResult
    {
        public ICollection<string> NextLetters { get; set; }
        public ICollection<string> NextCities { get; set; }
    }
}
