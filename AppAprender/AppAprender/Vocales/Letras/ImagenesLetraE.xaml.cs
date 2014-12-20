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

namespace Vocales
{
    public partial class ImagenesLetraE : PhoneApplicationPage
    {
        DispatcherTimer timer = new DispatcherTimer();
        int contador = 0;
        bool elefante = false;
        bool estrella = false;
        Recursos recurso;
        public ImagenesLetraE()
        {
            InitializeComponent();
            this.Loaded += ImagenesLetraE_Loaded;
        }

        void ImagenesLetraE_Loaded(object sender, RoutedEventArgs e)
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
                    recurso.reporducirSonido("/Sonidos/Voces/TocaComienzaE.mp3");
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    recurso.reporducirSonido("/Sonidos/Voces/Arbol.mp3");
                    break;
                case 4:
                    animacionArbol.Begin();
                    break;
                case 5:
                    recurso.reporducirSonido("/Sonidos/Voces/Huevo.mp3");
                    break;
                case 6:
                    animacionHuevo.Begin();
                    break;
                case 7:
                    recurso.reporducirSonido("/Sonidos/Voces/Galleta.mp3");
                    break;
                case 8:
                    animacionGalleta.Begin();
                    break;
                case 9:
                    recurso.reporducirSonido("/Sonidos/Voces/Elefante.mp3");
                    break;
                case 10:
                    animacionElefante.Begin();
                    break;
                case 11:
                    recurso.reporducirSonido("/Sonidos/Voces/Estrella.mp3");
                    break;
                case 12:
                    ainimacionEstrella.Begin();
                   
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
            imgArbol.Tap += imgArbol_Tap;
            imgElefante.Tap += imgArbol_Tap;
            imgEstrella.Tap += imgArbol_Tap;
            imgGalleta.Tap += imgArbol_Tap;
            imgHuevo.Tap += imgArbol_Tap;
        }

        void imgArbol_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Image img = sender as Image;
            if (img.Name == "imgElefante")
            {
                if (estrella)
                {
                    gano();
                }
                else
                {
                    animacionElefante.Begin();
                    elefante = true;
                }
            }
            else if (img.Name == "imgEstrella")
            {
                if (elefante)
                {
                    gano();
                }
                else
                {
                    ainimacionEstrella.Begin();
                    estrella = true;
                }
            }
            else
            {
            }
        }

        private void gano()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings["Vocales"] = 3;
            if (Convert.ToInt16(settings.Contains("vocalesPuntaje")) == 1)
            {
                settings["vocalesPuntaje"] = 2;
            }
            NavigationService.Navigate(new Uri("/../../Vocales/LetraI.xaml", UriKind.Relative));
        }
    }
}