using Microsoft.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Windows;

namespace AppAprender
{
    class Recursos
    {
        VibrateController vibrateCont;
        public void Error()
        {
            vibrateCont = VibrateController.Default;
            vibrateCont.Start(new TimeSpan(0, 0, 0, 0, 500));
        }
        public void Correcto()
        {
        }
        public void Casi()
        {
        }
        public void TodoBien()
        {

        }
        public void reporducirSonido(string url)
        {
            
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            float volumen=float.Parse(settings["volumenMusicaFondo"].ToString());
            bool reproducir = Convert.ToBoolean(settings["colocarMusicaFondo"].ToString());
            if (reproducir)
            {
                Song voz = Song.FromUri("name", new Uri(url, UriKind.Relative));
                FrameworkDispatcher.Update();
                if (MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Volume = volumen;
                    MediaPlayer.Play(voz);
                }
                else
                {
                    MediaPlayer.Volume = volumen;
                    MediaPlayer.Play(voz);
                }
            }
        }
        public void pararSonido()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            bool reproducir = Convert.ToBoolean(settings["colocarMusicaFondo"].ToString());
            
            if (reproducir)
            {
                FrameworkDispatcher.Update();
                MediaPlayer.Stop();

            }
        }

    }
}
