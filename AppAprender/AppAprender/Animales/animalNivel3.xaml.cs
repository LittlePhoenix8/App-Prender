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

namespace AppAprender.Animales
{
    public partial class animalNivel3 : PhoneApplicationPage
    {
        DispatcherTimer timer2 = new DispatcherTimer();
        int contador = 0;

        public animalNivel3()
        {
            InitializeComponent();
            Storyboard1.Begin();
            Recursos recurso = new Recursos();
            recurso.reporducirSonido("/Sonidos/DescubreChanchito.mp3");
            timer2 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromSeconds(1);
            timer2.Tick += new EventHandler(timer2_Tick);
            correcto.Opacity = 0;
            incorrecto.Opacity = 0;
        }

        void timer2_Tick(object sender, EventArgs e)
        {
            contador = contador + 1;

            if (contador > 1 && incorrecto.Opacity == 1.0)
            {
                timer2.Stop();
                contador = 0;
                StoryboardIncorrectoBack.Begin();
            }

            if (contador > 2 && correcto.Opacity == 1.0)
            {
                timer2.Stop();
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                settings["Animales"] = 1;
                if (Convert.ToInt32(settings["animalesPuntaje"]) == 2)
                {
                    settings["animalesPuntaje"] = 3;
                }
            
                NavigationService.Navigate(new Uri("/../Niveles.xaml", UriKind.Relative));
            }
        }

         private void alternativa1_Click_1(object sender, RoutedEventArgs e)
        {
            Recursos recurso = new Recursos();
            recurso.reporducirSonido("Sonidos/obeja.mp3");
           
            if(incorrecto.Opacity != 100)
            //incorrecto.Opacity = 100;
            StoryboardIncorrecto.Begin();
            timer2.Start();

            if (contador == 3 && incorrecto.Opacity == 100)
            {

            }
        }

        private void alternativa2_Click_1(object sender, RoutedEventArgs e)
        {
            Recursos recurso = new Recursos();
            recurso.reporducirSonido("Sonidos/cerdaco.mp3");
            StoryboardCorrecto.Begin();
            timer2.Start();
            
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
            timer2.Start();

            if (contador == 3 && incorrecto.Opacity == 100)
            {
                //timer.Stop();
                //contador = 0; 
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Uri.ToString() != "/Animales/animalNivel3.xaml")
            {
                NavigationService.RemoveBackEntry();
            }

            base.OnNavigatedTo(e);
        }

    }
}
