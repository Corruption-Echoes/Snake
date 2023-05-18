using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AricsSoundEngine
{
    class SoundEngine
    {
        System.Media.SoundPlayer move;
        System.Media.SoundPlayer kaboom;
        System.Media.SoundPlayer dig;
        System.Media.SoundPlayer flag;
        System.Media.SoundPlayer victory;
        System.Media.SoundPlayer start;
        string executableFilePath;
        string executableDirectoryPath;
        Thread bgm;
        MediaPlayer c;
        public void init()
        {
            executableFilePath = Assembly.GetExecutingAssembly().Location;
            executableDirectoryPath = Path.GetDirectoryName(executableFilePath);

            move = new System.Media.SoundPlayer(Path.Combine(executableDirectoryPath, "SoundFiles\\Move.wav"));
            kaboom = new System.Media.SoundPlayer(Path.Combine(executableDirectoryPath, "SoundFiles\\Explosion.wav"));
            dig = new System.Media.SoundPlayer(Path.Combine(executableDirectoryPath, "SoundFiles\\Dig.wav"));
            flag = new System.Media.SoundPlayer(Path.Combine(executableDirectoryPath, "SoundFiles\\Flag.wav"));
            victory = new System.Media.SoundPlayer(Path.Combine(executableDirectoryPath, "SoundFiles\\Victory.wav"));
            start = new System.Media.SoundPlayer(Path.Combine(executableDirectoryPath, "SoundFiles\\Start.wav"));



        }
        public void playSound(string toPlay)
        {
            if (toPlay == "move")
            {
                move.Play();
            }
            if (toPlay == "kaboom")
            {
                kaboom.Play();
            }
            if (toPlay == "dig")
            {
                dig.Play();
            }
            if (toPlay == "flag")
            {
                flag.Play();
            }
            if (toPlay == "victory")
            {
                victory.Play();
            }
            if (toPlay == "start")
            {
                start.Play();
                bgm = new Thread(playMusic);
                bgm.Start();
            }
        }
        public void stopPlayback()
        {
            bgm.Abort();
        }
        public void playMusic()
        {
            c = new MediaPlayer();
            c.Open(new Uri(Path.Combine(executableDirectoryPath, "SoundFiles\\Erhu.wav")));
            try
            {
                while (true)
                {
                    c.Play();
                    Thread.Sleep(48000);
                }
            }
            finally//Gotta make sure we kill our music when we kill the thread!
            {
                c.Stop();
            }
        }
    }
}
