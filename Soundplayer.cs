using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Media;

namespace _20200325_Mastermind_LucaKeller
{
    class Soundplayer
    {
        string path;
        private SoundPlayer p;

        public bool playing { get; private set; }

        public Soundplayer(String path)
        {
            this.path = path;
            if (File.Exists(path) == false)
            {
                return;
            }
            p = new SoundPlayer(path);
            playing = false;
        }

        public void play()
        {
            p.PlayLooping();
            playing = true;
        }

        public void stop()
        {
            p.Stop();
            playing = false;
        }
        
    }
}
