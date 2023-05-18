using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AricsStandardDataTypeLibrary
{
    public struct Point
    {
        public int x; public int y;

        public Point(int ix, int iy)
        {
            x = ix;
            y = iy;
        }
        public static Point Zero()
        {
            return new Point(0, 0);
        }
        public void modifyX(int ix)
        {
            x = ix;
        }
        public void modifyY(int iy)
        {
            y = iy;
        }
    }
    public struct Vector2
    {
        public float x; public float y;

        public Vector2(float ix, float iy)
        {
            x = ix;
            y = iy;
        }
        public static Vector2 Zero()
        {
            return new Vector2(0, 0);
        }
        public void modifyX(float ix)
        {
            x = ix;
        }
        public void modifyY(float iy)
        {
            y = iy;
        }
    }
    public struct Vector3
    {
        public float x; public float y; public float z;

        public Vector3(float ix, float iy, float iz)
        {
            x = ix;
            y = iy;
            z = iz;
        }
        public static Vector3 Zero()
        {
            return new Vector3(0, 0,0);
        }
        public void modifyX(float ix)
        {
            x = ix;
        }
        public void modifyY(float iy)
        {
            y = iy;
        }
        public void modifyZ(float iz)
        {
            z = iz;
        }
    }
    public struct Tile
    {
        public Point position;
        public List<Tile> neighbours;
        public string symbol;
        public string defaultSymbol;
        public Tile(Point ipos)
        {
            position = ipos;
            neighbours = new List<Tile>();
            symbol = "~";
            defaultSymbol = symbol;
        }
        public void setNeighbours(List<Tile> ineighbours)
        {
            neighbours=ineighbours;
        }
        public void setSymbol(string s)
        {
            symbol = s;
        }
    }
    public struct Map 
    {
        public Tile[][] tiles;
        public Map(Point mapDimensions)
        {
            tiles = new Tile[mapDimensions.y][];
            for (int y = 0; y < mapDimensions.x; y++)
            {
                tiles[y] = new Tile[mapDimensions.x];
                for (int x = 0; x < mapDimensions.x; x++)
                {
                    tiles[y][x] = new Tile(new Point(x, y));
                }
            }
        }
        public Point randomPoint()
        {
            Random r = new Random();
            while (true)
            {
                int y = r.Next(0, tiles.Count());
                int x = r.Next(0, tiles[y].Count());
                if (tiles[y][x].symbol == tiles[y][x].defaultSymbol)
                {
                    return new Point(x, y);
                }
            }
        }
        public void setSymbol(Point toChange,string symbol)
        {
            tiles[toChange.y][toChange.x].setSymbol(symbol);
        }
        public void setNeighbours()
        {
            for (int y = 0; y < tiles.Length; y++)
            {
                for (int x = 0; x < tiles[y].Length; x++)
                {
                    tiles[y][x].setNeighbours(determineNeighbours(new Point(y, x)));
                }
            }
        }
        public List<Tile> determineNeighbours(Point position)
        {
            List<Tile> neighbours = new List<Tile>();
            if (position.x != 0 && position.x != tiles[position.y].Length - 1)
            {//It's not an X edged tile
                if (position.y != 0 && position.y != tiles.Length - 1)
                {
                    //It's also not a Y edged tile, so it has all 8 potential neighbours
                    neighbours.Add( tiles[position.y - 1][ position.x - 1]);
                    neighbours.Add( tiles[position.y - 1][ position.x]);
                    neighbours.Add( tiles[position.y - 1][ position.x + 1]);
                    neighbours.Add( tiles[position.y][ position.x - 1]);
                    neighbours.Add( tiles[position.y][position.x + 1]);
                    neighbours.Add( tiles[position.y + 1][ position.x - 1]);
                    neighbours.Add( tiles[position.y + 1][ position.x]);
                    neighbours.Add( tiles[position.y + 1][ position.x + 1]);
                    return neighbours;
                }
                else
                {//It's a Y edged tile -3 to neighbours


                    neighbours.Add(tiles[position.y][position.x - 1]);
                    neighbours.Add(tiles[position.y][position.x + 1]);
                    //Check which way we're Y edged
                    if (position.y == 0)//Are we on the bottom?
                    {//Then we get the 3 above us
                        neighbours.Add(tiles[position.y + 1][position.x - 1]);
                        neighbours.Add(tiles[position.y + 1][position.x]);
                        neighbours.Add(tiles[position.y + 1][position.x + 1]);
                        return neighbours;
                    }
                    else
                    {//Otherwise it's the 3 below us
                        neighbours.Add(tiles[position.y - 1][position.x - 1]);
                        neighbours.Add(tiles[position.y - 1][position.x]);
                        neighbours.Add(tiles[position.y - 1][position.x + 1]);
                        return neighbours;
                    }
                }
            }//It's an X edged tile, so -3, but is it a Y edged? 
            else if (position.y != 0 && position.y != tiles.Length - 1)
            { //No, so just -3
                neighbours.Add(tiles[position.y - 1][position.x]);
                neighbours.Add(tiles[position.y + 1][position.x]);
                if (position.x == 0)//Are we crammed against the left edge?
                {//Then we get the 3 to our right
                    neighbours.Add(tiles[position.y - 1][position.x + 1]);
                    neighbours.Add(tiles[position.y][position.x + 1]);
                    neighbours.Add(tiles[position.y + 1][position.x + 1]);
                    return neighbours;
                }
                else
                {//Otherwise it's the 3 to our left!
                    neighbours.Add(tiles[position.y - 1][position.x - 1]);
                    neighbours.Add(tiles[position.y][position.x - 1]);
                    neighbours.Add(tiles[position.y + 1][position.x - 1]);
                    return neighbours;
                }
            }
            else
            {//Yes, so a further -2, this is a corner tile!
                if (position.x == 0)
                {//We're against the left wall
                    if (position.y == 0)
                    {//And we're against the bottom wall
                        neighbours.Add(tiles[position.y + 1][position.x]); //So we get these 3
                        neighbours.Add(tiles[position.y + 1][position.x + 1]);
                        neighbours.Add(tiles[position.y][position.x + 1]);
                        return neighbours;
                    }
                    else
                    {//Otherwise we're against the left and top walls
                        neighbours.Add(tiles[position.y][position.x + 1]);//So we get these 3
                        neighbours.Add(tiles[position.y - 1][position.x + 1]);
                        neighbours.Add(tiles[position.y - 1][position.x]);
                        return neighbours;
                    }
                }
                else
                {//We're against the right wall, so no x+1 tiles
                    if (position.y == 0)
                    {//And we're at the bottom
                        neighbours.Add(tiles[position.y + 1][position.x]);//So we get these 3
                        neighbours.Add(tiles[position.y + 1][position.x - 1]);
                        neighbours.Add(tiles[position.y][position.x - 1]);
                        return neighbours;
                    }
                    else
                    {//We're in the top right corner
                        neighbours.Add(tiles[position.y - 1][position.x]);//So we get these 3
                        neighbours.Add(tiles[position.y - 1][position.x - 1]);
                        neighbours.Add(tiles[position.y][position.x - 1]);
                        return neighbours;
                    }
                }
            }
        }
    }

}
