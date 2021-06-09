using CitySearch.Searches;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CitySearch.Test.Searches
{
    [TestClass]
    public class SimpleCityFinderTest : CityFinderTest
    {
        internal override SimpleCityFinder getFinder(IList<string> cities)
        {
            return new SimpleCityFinder(cities);
        }
    }
}