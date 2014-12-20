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
    public partial class LetraO : PhoneApplicationPage
    {
        DispatcherTimer timer = new DispatcherTimer();
        int contador = 0;
        bool ojos = false;
        bool oso = false;
        Recursos recurso;

        public LetraO()
        {
            InitializeComponent();
            this.Loaded+=LetraO_Loaded;
        }

        void LetraO_Loaded(object sender, RoutedEventArgs e)
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
                    recurso.reporducirSonido("/Sonidos/Voces/TocaComienzaO.mp3");
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    recurso.reporducirSonido("/Sonidos/Voces/Extintor.mp3");
                    break;
                case 4:
                    Extintor.Begin();
                    break;
                case 5:
                    recurso.reporducirSonido("/Sonidos/Voces/Oso.mp3");
                    break;
                case 6:
                    Oso.Begin();
                    break;
                case 7:
                    recurso.reporducirSonido("/Sonidos/Voces/Pollo.mp3");
                    break;
                case 8:
                    Pollito.Begin();
                    break;
                case 9:
                    recurso.reporducirSonido("/Sonidos/Voces/Ampolleta.mp3");
                    break;
                case 10:
                    Ampolleta.Begin();
                    break;
                case 11:
                    recurso.reporducirSonido("/Sonidos/Voces/Ojos.mp3");
                    break;
                case 12:
                    Ojos.Begin();
                    break;
                case 13:
                    timer.Stop();
                    desbloquearBotones();
                    break;
            }
            contador++;
        }

        private void desbloquearBotones()
        {
            imgOjos.Tap += imgAmpolleta_Tap_1;
            imgOso.Tap += imgAmpolleta_Tap_1;
            imgPollito.Tap += imgAmpolleta_Tap_1;
            imgExtintor.Tap += imgAmpolleta_Tap_1;
            imgAmpolleta.Tap += imgAmpolleta_Tap_1;
        }

       
        private void imgAmpolleta_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Image img = sender as Image;
            if (img.Name == "imgOjos")
            {
                if (oso)
                {
                    gano();
                    
                }
                else
                {
                    Ojos.Begin();
                    ojos = true;
                }
            }
            else if (img.Name == "imgOso")
            {
                if (ojos)
                {
                    gano();
                }
                else
                {
                    Oso.Begin();
                    oso = true;
                }
            }
            else
            {
            }
        }

        private void gano()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings["Vocales"] = 5;
            if (Convert.ToInt16(settings.Contains("vocalesPuntaje")) == 3)
            {
                settings["vocalesPuntaje"] = 4;
            }
            NavigationService.Navigate(new Uri("/../../Vocales/LetraU.xaml", UriKind.Relative));
        }
    }
}