using AricsStandardDataTypeLibrary;
using AricsStandardInputLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AricsSoundEngine;
using System.Windows;
using Point = AricsStandardDataTypeLibrary.Point;

namespace Snake
{
    internal class Loop
    {
        Thread renderThread;
        Thread gameThread;
        int renderSpeed;
        public int gameSpeed = 500;
        int mapSize=25;
        int FPSTarget = 60;
        MapRenderer renderer;
        InputHandler input;
        public SnakeController snake;
        public SoundEngine soundEngine;

        public Loop() 
        {
            Console.CursorVisible = false;
            soundEngine= new SoundEngine();
            soundEngine.init();
            soundEngine.playSound("start");
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
            try
            {
                while (true)
                {
                    renderer.renderMap();
                    Thread.Sleep(renderSpeed);
                }
            }
            finally
            {
                //Intentionally left blank
            }
        }
        public void killThreads()
        {
            renderThread.Abort();
            soundEngine.stopPlayback();
            renderGameOver();
        }
        void gameplayThread()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(gameSpeed);
                    if (snake.move())
                    {
                        gameSpeed = (int)(gameSpeed * 0.95f);
                        soundEngine.playSound("victory");
                    }
                    else if (snake.lost)
                    {
                        soundEngine.playSound("kaboom");
                        killThreads();
                        break;
                    }
                    else
                    {
                        soundEngine.playSound("move");
                    }
                }
            }
            finally
            {
                //Intentionally left break
            }
        }
        public void renderGameOver()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n  You lose!  \n Final Score:\n " + snake.points+" \n  Try again!  ");
        }
    }
}
