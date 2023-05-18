using AricsStandardDataTypeLibrary;
using AricsStandardInputLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    internal class Loop
    {
        Thread renderThread;
        Thread gameThread;
        int renderSpeed;
        int gameSpeed = 500;
        int mapSize=25;
        int FPSTarget = 60;
        MapRenderer renderer;
        InputHandler input;
        public SnakeController snake;

        public Loop() 
        {
            Console.CursorVisible = false;
            renderSpeed = 1000 / FPSTarget;
            renderer = new MapRenderer(mapSize);
            input = new InputHandler();
            snake = new SnakeController(new Point(mapSize / 2, mapSize / 3));
            snake.passMap(renderer.map);
            renderThread = new Thread(animationThread);
            renderThread.Start();
            gameThread = new Thread(gameplayThread);
            gameThread.Start();
            while(true)
            {
                snake.setMoveDirection(input.getPlayerInput());
            }
        }
        void animationThread()
        {
            while (true)
            {
                renderer.renderMap();
                Thread.Sleep(renderSpeed);
            }
        }
        void gameplayThread()
        {
            while (true)
            {
                Thread.Sleep(gameSpeed);
                snake.move();
            }
        }
    }
}
