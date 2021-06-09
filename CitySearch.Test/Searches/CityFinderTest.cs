using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using CitySearch.Searches;
using CitySearch;
using System.IO;

namespace CitySearch.Test.Searches
{
    [TestClass]
    public abstract class CityFinderTest
    {
        internal abstract SimpleCityFinder getFinder(IList<string> cities);
        IList<string> cities;

        [TestInitialize]
        public void Test_Initialise()
        {
            if(cities == null)
            {
                cities = new List<string>();

                if (File.Exists("citynames.csv"))
                {
                    using(StreamReader sr = File.OpenText("citynames.csv"))
                    {
                        string s;
                        while((s = sr.ReadLine()) != null)
                        {
                            cities.Add(s);
                        }
                    }
                }
                else
                {
                    Assert.Fail("Could not load city names");
                }
            }
        }

        [TestMethod]
        public void search_random()
        {
            ICityFinder finder = getFinder(cities);

            Random random = new Random();
            for(int x = 0; x < 100; x++)
            {
                string fullCityName = cities[random.Next(cities.Count)];
                string partialCityName = fullCityName.Substring(0, random.Next(fullCityName.Length));

                ICityResult result = finder.Search(partialCityName);
                foreach(String s in result.NextCities)
                {
                    Assert.AreEqual(partialCityName, s.Substring(0, partialCityName.Length));
                    //we only want to check for next letter if there is one
                    if(s.Length > partialCityName.Length)
                    {
                        string nextLetter = s.Substring(partialCityName.Length, 1);
                        Assert.IsTrue(result.NextLetters.Contains(nextLetter), "Character (" + nextLetter + ") not available in the list of next characters");
                    }
                }
            }
        }
    }
}