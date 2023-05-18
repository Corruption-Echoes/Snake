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
        public static List<Point> snakeTiles = new List<Point>();
        string moveDirection = "Up";
        Map map;
        Point food;
        public SnakeController(Point startPosition)
        {
            snakeTiles.Add(startPosition);
        }
        public void passMap(Map m)
        {
            map = m;
            map.setSymbol(snakeTiles[0], "O");
        }
        public void setMoveDirection(string direction)
        {
            moveDirection = direction;
        }

        public void move()
        {
            if (food.Equals(new Point(0,0))||food.Equals(snakeTiles[0]))
            {
                food = map.randomPoint();
            }
            List<Point> newTiles = new List<Point>();
            Point headPoint=new Point(0,0);
            map.setSymbol(food, "+");
            switch (moveDirection)
            {
                case "Up":
                    headPoint = new Point(snakeTiles[0].x, snakeTiles[0].y + 1);
                    break;
                case "Down":
                    headPoint = new Point(snakeTiles[0].x, snakeTiles[0].y - 1);
                    break;
                case "Left":
                    headPoint = new Point(snakeTiles[0].x - 1, snakeTiles[0].y);
                    break;
                case "Right":
                    headPoint = new Point(snakeTiles[0].x + 1, snakeTiles[0].y);
                    break;
            }


            if (headPoint.x == food.x && headPoint.y == food.y)
            {
                snakeTiles.Insert(0,headPoint);
            }
            else
            {
                map.setSymbol(snakeTiles[snakeTiles.Count()-1], "~");
                for (int i = snakeTiles.Count()-1; i >0; i--)
                {
                    snakeTiles[i] = snakeTiles[i-1];
                    map.setSymbol(snakeTiles[i], "O");
                }
            }
            
            map.setSymbol(headPoint, "O");
            snakeTiles[0] = headPoint;
        }
    }
}
