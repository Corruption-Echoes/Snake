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
        public Map map;
        Dictionary<string, List<ConsoleColor>> symbolColorCodex;
        public MapRenderer(int mapSize) 
        {
            map = new Map(new Point(mapSize,mapSize));
            symbolColorCodex = new Dictionary<string, List<ConsoleColor>> ();
            symbolColorCodex.Add("~", new List<ConsoleColor> { ConsoleColor.Green ,ConsoleColor.DarkGreen});
            symbolColorCodex.Add("O", new List<ConsoleColor> { ConsoleColor.Blue, ConsoleColor.Magenta });
            symbolColorCodex.Add("+", new List<ConsoleColor> { ConsoleColor.Red, ConsoleColor.DarkRed });

        }
        public void renderMap()
        {
            string toWrite = "";
            string lastSymbol = "~";
            Tile[][] inverter = map.tiles;
            Console.SetCursorPosition(0, 0);
            foreach (Tile[] t in inverter.Reverse())
            {
                foreach(Tile tile in t)
                {
                    if (tile.symbol != lastSymbol)
                    {
                        Console.ForegroundColor = symbolColorCodex[lastSymbol][0];
                        Console.BackgroundColor = symbolColorCodex[lastSymbol][1];
                        Console.Write(toWrite);
                        lastSymbol = tile.symbol;
                        toWrite = "";
                    }
                    toWrite +=tile.symbol;
                }
                toWrite+="\n";
            }
            Console.ForegroundColor = symbolColorCodex[lastSymbol][0];
            Console.BackgroundColor = symbolColorCodex[lastSymbol][1];
            Console.Write(toWrite);
        }
    }
}
