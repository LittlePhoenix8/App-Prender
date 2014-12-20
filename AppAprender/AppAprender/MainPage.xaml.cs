using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Threading;
using Telerik.Windows.Controls;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace AppAprender
{
    public partial class MainPage : PhoneApplicationPage
    {
        IsolatedStorageSettings settings;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            //Thread.Sleep(2000);
            settings = IsolatedStorageSettings.ApplicationSettings;
            colocarAudiosSiNoExisten();
            if (!settings.Contains("mostrarMensajeMusica"))
            {
                //MessageBox.Show("hola");
                settings.Add("mostrarMensajeMusica", "false");
            }
           
        }
        private void colocarAudiosSiNoExisten()
        {
            if (!settings.Contains("colocarMusicaFondo"))
            {
                settings.Add("colocarMusicaFondo", "True");
            }
            if (!settings.Contains("volumenMusicaFondo"))
            {
                settings.Add("volumenMusicaFondo", 0.5f);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                if (settings["mostrarMensajeMusica"].ToString() == "false")
                {
                    RadMessageBox.Show("Antes que nada", MessageBoxButtons.YesNo, "Si presionas si," +
                        " se pondra en stop el reproductor de musica, " +
                        "debido a que lo necesitamos para el juego", closedHandler: (arg) =>
                        {
                            int indexBoton = arg.ButtonIndex;
                            switch (indexBoton)
                            {
                                case 0:
       
                                    FrameworkDispatcher.Update();
                                     MediaPlayer.Stop();
                                    settings["colocarMusicaFondo"] = "True";
                                    settings["volumenMusicaFondo"] = 0.5f;
                                    NavigationService.Navigate(new Uri("/Niveles.xaml", UriKind.Relative));
                                    break;
                                case 1:
                                    settings["colocarMusicaFondo"] = "False";
                                    settings["volumenMusicaFondo"] = 0.5f;
                                    NavigationService.Navigate(new Uri("/Niveles.xaml", UriKind.Relative));
                                    break;
                            }
                        });
                }
                else
                {
                    FrameworkDispatcher.Update();
                    MediaPlayer.Stop();
                    NavigationService.Navigate(new Uri("/Niveles.xaml", UriKind.Relative));
                }
            }
            else
            {
                NavigationService.Navigate(new Uri("/Niveles.xaml", UriKind.Relative));
            }
          
         //  
            //NavigationService.Navigate(new Uri("/Imagenes1.xaml", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Proximamente!!!","App-Prender",MessageBoxButton.OK);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Configuraciones.xaml", UriKind.Relative));
        }

    }
}