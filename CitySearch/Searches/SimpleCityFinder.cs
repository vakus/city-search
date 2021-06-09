using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CitySearch.Searches
{
    public class SimpleCityFinder : ICityFinder
    {
        private ICollection<string> cities;

        public SimpleCityFinder(ICollection<string> cities)
        {
            //sorted lists are usually better for speculative execution
            cities = cities.OrderBy(r => r.ToUpper(), StringComparer.OrdinalIgnoreCase).ToList();
            this.cities = cities;
            
        }

        /**
         * The simplest implementation of search, used as a base case for testing
         * not optimised
         */
        public ICityResult Search(string searchString)
        {
            CityResult result = new CityResult();
            result.NextCities = cities.Where(r => r.StartsWith(searchString)).ToList();
            result.NextLetters = result.NextCities.Select(r => (r.Length > searchString.Length) ? r.Substring(searchString.Length, 1) : "").ToList();
            return result;
        }
    }
}