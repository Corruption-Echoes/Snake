using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AricsStandardDataTypeLibrary
{
    public struct Victor2
    {
        public int x; public int y;

        public Victor2(int ix, int iy)
        {
            x = ix;
            y = iy;
        }
        public static Victor2 Zero()
        {
            return new Victor2(0, 0);
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
        public Victor2 position;
        public List<Tile> neighbours;
        public string symbol;
        public Tile(Victor2 ipos)
        {
            position = ipos;
            neighbours = new List<Tile>();
            symbol = "~";
        }
        public void setNeighbours(List<Tile> ineighbours)
        {
            neighbours=ineighbours;
        }
    }
    public struct Map 
    {
        public Tile[][] tiles;
        public Map(Victor2 mapDimensions)
        {
            tiles = new Tile[mapDimensions.y][];
            for (int y = 0; y < mapDimensions.x; y++)
            {
                tiles[y] = new Tile[mapDimensions.x];
                for (int x = 0; x < mapDimensions.x; x++)
                {
                    tiles[y][x] = new Tile(new Victor2(x, y));
                }
            }
        }
        public void setNeighbours()
        {
            for (int y = 0; y < tiles.Length; y++)
            {
                for (int x = 0; x < tiles[y].Length; x++)
                {
                    tiles[y][x].setNeighbours(determineNeighbours(new Victor2(y, x)));
                }
            }
        }
        public List<Tile> determineNeighbours(Victor2 position)
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
