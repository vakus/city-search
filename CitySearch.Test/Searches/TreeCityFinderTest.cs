using CitySearch.Searches;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CitySearch.Test.Searches
{
    [TestClass]
    public class TreeCityFinderTest : CityFinderTest
    {
        internal override ICityFinder getFinder(IList<string> cities)
        {
            return new TreeCityFinder(cities);
        }
    }
}
