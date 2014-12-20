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

namespace Vocales.Letras
{
    public partial class ImagenesLetraI : PhoneApplicationPage
    {
        DispatcherTimer timer = new DispatcherTimer();
        int contador = 0;
        bool iglu = false;
        bool isla = false;
        Recursos recurso;
        public ImagenesLetraI()
        {
            InitializeComponent();
            this.Loaded += ImagenesLetraI_Loaded;
        }

        void ImagenesLetraI_Loaded(object sender, RoutedEventArgs e)
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
                    recurso.reporducirSonido("/Sonidos/Voces/TocaComienzaI.mp3");
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    recurso.reporducirSonido("/Sonidos/Voces/iglu.mp3");
                    break;
                case 4:
                    animacionIglu.Begin();
                    break;
                case 5:
                    recurso.reporducirSonido("/Sonidos/Voces/celular.mp3");
                    break;
                case 6:
                    animacionTelefono.Begin();
                    break;
                case 7:
                    recurso.reporducirSonido("/Sonidos/Voces/Mascara.mp3");
                    break;
                case 8:
                    animacionMascara.Begin();
                    break;
                case 9:
                    recurso.reporducirSonido("/Sonidos/Voces/isla.mp3");
                    break;
                case 10:
                    animacionIsla.Begin();
                    break;
                case 11:
                    recurso.reporducirSonido("/Sonidos/Voces/Zorro.mp3");
                    break;
                case 12:
                    animacionZorro.Begin();
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
            imgIglu.Tap += imgIglu_Tap;
            imgIsla.Tap += imgIglu_Tap;
            imgMascara.Tap += imgIglu_Tap;
            imgTelefono.Tap += imgIglu_Tap;
            imgZorro.Tap += imgIglu_Tap;
        }

        void imgIglu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Image img = sender as Image;
            if (img.Name == "imgIglu")
            {
                if (isla)
                {
                    gano();
                }
                else
                {
                    animacionIglu.Begin();
                    iglu = true;
                }
            }
            else if (img.Name == "imgIsla")
            {
                if (iglu)
                {
                    gano();
                }
                else
                {
                    animacionIsla.Begin();
                    isla = true;
                }
            }
            else
            {
            }
        }

        private void gano()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings["Vocales"] = 4;
            if (Convert.ToInt16(settings.Contains("vocalesPuntaje")) == 2)
            {
                settings["vocalesPuntaje"] = 3;
            }
            NavigationService.Navigate(new Uri("/../../Vocales/LetraO.xaml", UriKind.Relative));
        }
    }
}