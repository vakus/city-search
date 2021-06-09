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
            rootNode = new Node { subnodes = new Dictionary<string, Node>(), startIndex = 0, endIndex = cities.Count};

            cities = cities.OrderBy(r => r, StringComparer.Ordinal).ToList();
            this.cities = cities;
            for(int x = 0; x < cities.Count; x++) 
            {
                Node current = rootNode;
                string name = cities[x];
                foreach(char letter in name)
                {
                    if (!current.subnodes.ContainsKey(letter.ToString())) { 

                        current.subnodes.Add(letter.ToString(), new Node { subnodes = new Dictionary<string, Node>(), startIndex =x, endIndex=x});
                    }
                    else
                    {
                        current.endIndex = x;
                    }
                    current = current.subnodes[letter.ToString()];
                }
            }
        }

        public ICityResult Search(string searchString)
        {
            
            Node node = rootNode;
            foreach(char letter in searchString)
            {
                if (node.subnodes.ContainsKey(letter.ToString()))
                {
                    node = node.subnodes[letter.ToString()];
                }
                else
                {
                    //there isnt any deeper nodes so we dont know of any cities with this name
                    return new CityResult { NextCities = new List<string> { }, NextLetters = new List<string> { } };
                }
            }

            return new CityResult{
                NextLetters= node.subnodes.Keys.ToList(),
                NextCities = cities.Where((v, i) => i >= node.startIndex && i <= node.endIndex).ToList()
            };
        }
    }

    class Node
    {
        public Dictionary<string, Node> subnodes;
        public int startIndex;
        public int endIndex;
    }

}