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
        internal abstract ICityFinder getFinder(IList<string> cities);
        IList<string> cities;

        [TestInitialize]
        public void Test_Initialise()
        {
            if(cities == null)
            {
                cities = new List<string>();

                string path = "Data/citynames.csv";

                if (File.Exists(path))
                {
                    using(StreamReader sr = File.OpenText(path))
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
            for(int x = 0; x < 500; x++)
            {
                string fullCityName = cities[random.Next(cities.Count)];
                string partialCityName = fullCityName.Substring(0, random.Next(fullCityName.Length)).ToUpper();

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

        /**
         * If not sorted correctly "L'A" can be mixed within "La" as `'` would be skipped
         */
        [TestMethod]
        public void search_La()
        {
            ICityFinder finder = getFinder(cities);
            string partialCityName = "La".ToUpper();

            ICityResult result = finder.Search(partialCityName);
            foreach (String s in result.NextCities)
            {
                Assert.AreEqual(partialCityName, s.Substring(0, partialCityName.Length));
                //we only want to check for next letter if there is one
                if (s.Length > partialCityName.Length)
                {
                    string nextLetter = s.Substring(partialCityName.Length, 1);
                    Assert.IsTrue(result.NextLetters.Contains(nextLetter), "Character (" + nextLetter + ") not available in the list of next characters");
                }
            }
        }

        /**
         * Check if space is correctly treated in "Los " search
         */
        [TestMethod]
        public void search_Los_()
        {
            ICityFinder finder = getFinder(cities);
            string partialCityName = "Los ".ToUpper();

            ICityResult result = finder.Search(partialCityName);
            foreach (String s in result.NextCities)
            {
                Assert.AreEqual(partialCityName, s.Substring(0, partialCityName.Length));
                //we only want to check for next letter if there is one
                if (s.Length > partialCityName.Length)
                {
                    string nextLetter = s.Substring(partialCityName.Length, 1);
                    Assert.IsTrue(result.NextLetters.Contains(nextLetter), "Character (" + nextLetter + ") not available in the list of next characters");
                }
            }
        }


        /**
         * Check if it works if we give empty string
         */
        [TestMethod]
        public void search_empty_string()
        {
            ICityFinder finder = getFinder(cities);
            string partialCityName = "".ToUpper();

            ICityResult result = finder.Search(partialCityName);
            foreach (String s in result.NextCities)
            {
                Assert.AreEqual(partialCityName, s.Substring(0, partialCityName.Length));
                //we only want to check for next letter if there is one
                if (s.Length > partialCityName.Length)
                {
                    string nextLetter = s.Substring(partialCityName.Length, 1);
                    Assert.IsTrue(result.NextLetters.Contains(nextLetter), "Character (" + nextLetter + ") not available in the list of next characters");
                }
            }
        }

        /**
         * Check if it works if we give not existing cityname
         */
        [TestMethod]
        public void search_non_existing()
        {
            ICityFinder finder = getFinder(cities);
            string partialCityName = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";

            ICityResult result = finder.Search(partialCityName);
            foreach (String s in result.NextCities)
            {
                Assert.AreEqual(partialCityName, s.Substring(0, partialCityName.Length));
                //we only want to check for next letter if there is one
                if (s.Length > partialCityName.Length)
                {
                    string nextLetter = s.Substring(partialCityName.Length, 1);
                    Assert.IsTrue(result.NextLetters.Contains(nextLetter), "Character (" + nextLetter + ") not available in the list of next characters");
                }
            }
        }
    }
}