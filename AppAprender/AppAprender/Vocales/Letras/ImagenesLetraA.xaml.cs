using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Threading;
using System.IO.IsolatedStorage;
using AppAprender;
using Microsoft.Xna.Framework.Media;

namespace Vocales.Letra_A
{
    public partial class ImagenesLetraA : PhoneApplicationPage
    {
        DispatcherTimer timer = new DispatcherTimer();
        int contador = 0;
        bool auto = false;
        Recursos recurso;
        bool avion = false;
        public ImagenesLetraA()
        {
            InitializeComponent();
            this.Loaded += ImagenesLetraA_Loaded;
        }

        void ImagenesLetraA_Loaded(object sender, RoutedEventArgs e)
        {
            recurso = new Recursos();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1500);
            timer.Tick += timer_Tick;
            timer.Start();   
        }

        

        void timer_Tick(object sender, EventArgs e)
        {
            switch (contador)
            {
                case 0:
                    recurso.reporducirSonido("/Sonidos/Voces/TocaComienzaA.mp3");
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    recurso.reporducirSonido("/Sonidos/Voces/Auto.mp3");
                    break;
                case 4:
                    animacionAuto.Begin();
                    break;
                case 5:
                    recurso.reporducirSonido("/Sonidos/Voces/Torta.mp3");
                    break;
                case 6:
                    animacionTorta.Begin();
                    break;
                case 7:
                    recurso.reporducirSonido("/Sonidos/Voces/Zanahoria.mp3");
                    break;
                case 8:
                    animacionZanahoria.Begin();
                    break;
                case 9:
                    recurso.reporducirSonido("/Sonidos/Voces/Avion.mp3");
                    break;
                case 10:
                    animacionAvion.Begin();
                    break;
                case 11:
                    recurso.reporducirSonido("/Sonidos/Voces/Mochila.mp3");
                    break;
                case 12:
                    animacionMochila.Begin();
                    
                    break;
                case 13:
                    timer.Stop();
                    asignarEventos();
                    break;
            }
            contador++;
        }

        private void asignarEventos()
        {
            imgAuto.Tap += imgAuto_Tap_1;
            imgAvion.Tap += imgAuto_Tap_1;
            imgMochila.Tap += imgAuto_Tap_1;
            imgTorta.Tap += imgAuto_Tap_1;
            imgZanahoria.Tap += imgAuto_Tap_1;
        }

        private void imgAuto_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Image img = sender as Image;
            if (img.Name == "imgAvion")
            {
                if (auto)
                {
                    gano();
                }
                else
                {
                    animacionAvion.Begin();
                    avion = true;
                }

            }
            else if (img.Name == "imgAuto")
            {
                if (avion)
                {
                    gano();
                }
                else
                {
                    animacionAuto.Begin();
                    auto = true;
                }
            }
            else
            {

            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            timer.Stop();
            base.OnNavigatedFrom(e);
        }
        private void gano()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings["Vocales"] = 2;
            if (Convert.ToInt16(settings.Contains("vocalesPuntaje")) == 0)
            {
                settings["vocalesPuntaje"] = 1;
            }
            NavigationService.Navigate(new Uri("/../../Vocales/LetraE.xaml", UriKind.Relative));
        }
    }
}