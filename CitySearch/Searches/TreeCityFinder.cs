using CitySearch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace CitySearch
{
    public class TreeCityFinder : ICityFinder
    {
        Node rootNode;
        IList<string> cities;

        public TreeCityFinder(IList<string> cities)
        {
            rootNode = new Node { subnodes = new Dictionary<char, Node>(), startIndex = 0, endIndex = cities.Count };

            cities = cities.OrderBy(r => r.ToUpper(), StringComparer.Ordinal).ToList();
            this.cities = cities;
            for (int x = 0; x < cities.Count; x++)
            {
                Node current = rootNode;
                string name = cities[x];
                foreach (char letter in name)
                {
                    if (!current.subnodes.ContainsKey(letter))
                    {

                        current.subnodes.Add(letter, new Node { subnodes = new Dictionary<char, Node>(), startIndex = x, endIndex = x });
                    }
                    else
                    {
                        current.endIndex = x;
                    }
                    current = current.subnodes[letter];
                }
            }
        }

        public ICityResult Search(string searchString)
        {
            searchString = searchString.ToUpper();
            Node node = rootNode;
            foreach (char letter in searchString)
            {
                if (node.subnodes.ContainsKey(letter))
                {
                    node = node.subnodes[letter];
                }
                else
                {
                    //there isnt any deeper nodes so we dont know of any cities with this name
                    return new CityResult { NextCities = new List<string> { }, NextLetters = new List<string> { } };
                }
            }

            return new CityResult
            {
                NextLetters = node.subnodes.Keys.Select(r => r.ToString()).ToList(),
                NextCities = cities.Skip(node.startIndex).Take(node.endIndex - node.startIndex).ToList()
            };
        }
    }

    class Node
    {
        public Dictionary<char, Node> subnodes;
        public int startIndex;
        public int endIndex;
    }

}