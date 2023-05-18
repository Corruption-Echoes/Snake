using AricsStandardDataTypeLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class MapRenderer
    {
        Map map;
        public MapRenderer(int mapSize) 
        {
            map = new Map(new Victor2(mapSize,mapSize));
        }
        public void renderMap()
        {
            string toWrite = "";
            foreach (Tile[] t in map.tiles)
            {
                for (int x=0; x < t.Length; x++)
                {
                    toWrite+=t[x].symbol+" ";
                }
                toWrite+="\n";
            }
            Console.SetCursorPosition(0, 0);
            Console.Write(toWrite);
        }
    }
}
