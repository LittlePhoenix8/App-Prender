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
using ControlTiltEffect;
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using System.IO.IsolatedStorage;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using System.Threading;
using Microsoft.Devices;
using System.ComponentModel;
using System.Windows.Navigation;
using AppAprender;


namespace Animales
{

    public partial class MainPage : PhoneApplicationPage
    {
        DispatcherTimer timer = new DispatcherTimer();
        int contador = 0;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Storyboard1.Begin();
            Recursos recurso = new Recursos();
            recurso.reporducirSonido("/Sonidos/DescubreCaballo.mp3");
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            correcto.Opacity = 0;
            incorrecto.Opacity = 0;
            
            
        }

        void timer_Tick(object sender, EventArgs e)
        {
            contador = contador + 1;

            if (contador > 1 && incorrecto.Opacity == 1.0)
            {
                
                timer.Stop();
                contador = 0;
                StoryboardIncorrectoBack.Begin();
            }

            if (contador > 2 && correcto.Opacity == 1.0)
            {
                timer.Stop();
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                settings["Animales"] = 2;
                if (Convert.ToInt32(settings["animalesPuntaje"]) == 0)
                {
                    settings["animalesPuntaje"] = 1;
                }
                NavigationService.Navigate(new Uri("/../Animales/animalNivel2.xaml", UriKind.Relative));
            }
        }

        private void alternativa1_Click_1(object sender, RoutedEventArgs e)
        {
            Recursos recurso = new Recursos();
            recurso.reporducirSonido("Sonidos/obeja.mp3");
            
            if(incorrecto.Opacity != 100)
            //incorrecto.Opacity = 100;
            StoryboardIncorrecto.Begin();
            timer.Start();
        }

        private void alternativa2_Click_1(object sender, RoutedEventArgs e)
        {
            Recursos recurso = new Recursos();
            recurso.reporducirSonido("Sonidos/caballo.mp3");
           
            StoryboardCorrecto.Begin();
            timer.Start();
            
            if (incorrecto.Opacity == 1.0)
                incorrecto.Opacity = 0;

            if (contador == 3)
            {
                correcto.Opacity = 100;
                
            }
        }

        private void alternativa3_Click_1(object sender, RoutedEventArgs e)
        {
            Recursos recurso = new Recursos();
            recurso.reporducirSonido("Sonidos/pato.mp3");
            
            StoryboardIncorrecto.Begin();

            if (incorrecto.Opacity != 100)
                incorrecto.Opacity = 100;
            timer.Start();

            if (contador == 3 && incorrecto.Opacity == 100)
            {
                //timer.Stop();
                //contador = 0; 
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Uri.ToString() != "/Animales/animalNivel1.xaml")
            {
                NavigationService.RemoveBackEntry();
            }
            
            base.OnNavigatedTo(e);
        }

    }
}
