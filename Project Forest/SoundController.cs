using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;

namespace Project_Forest
{
    class SoundController
    {
        SoundEffectInstance music;

        public SoundEffectInstance Music
        {
            get { return music; }
            set { music = value; }
        }
        public SoundController(SoundEffect e)
        {
            music = e.CreateInstance();
        }

        public void Play()
        {
            music.Stop();
            music.Play();
        }

        public void Pause()
        {
            music.Pause();
        }
    }
}
