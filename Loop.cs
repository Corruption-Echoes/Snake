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
        int renderSpeed;
        int mapSize=25;
        int FPSTarget = 60;
        MapRenderer renderer;

        public Loop() 
        {
            Console.CursorVisible = false;
            renderThread = new Thread(animationThread);
            renderThread.Start();
            renderSpeed = 1000/FPSTarget;
            while(true)
            {
                char intake=char.Parse(char.ConvertFromUtf32(Console.Read()));
                if (intake == 'q')
                {
                    break;
                }
            }
            renderThread.Abort();
        }
        void animationThread()
        {
            renderer= new MapRenderer( mapSize);
            while (true)
            {
                renderer.renderMap();
                Thread.Sleep(renderSpeed);
            }
        }
    }
}
