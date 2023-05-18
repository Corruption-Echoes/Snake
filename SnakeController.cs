using AricsStandardDataTypeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class SnakeController
    {
        public static List<Victor2> snakeTiles = new List<Victor2>();
        List<Victor2> newTiles;
        string moveDirection = "up";
        public Dictionary<string, string[]> keyMaps;
        public void initializeMaps()
        {
            keyMaps = new Dictionary<string, string[]>();
            keyMaps["Left"] = new string[] { "LeftArrow", "A" };
            keyMaps["Right"] = new string[] { "RightArrow", "D" };
            keyMaps["Up"] = new string[] { "UpArrow", "W" };
            keyMaps["Down"] = new string[] { "DownArrow", "S" };
            keyMaps["Confirm"] = new string[] { "Enter", "Spacebar", "X" };
            keyMaps["Cancel"] = new string[] { "Backspace", "Z" };
        }

    }
}
