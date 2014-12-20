using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading;
using System.Windows.Threading;
using System.IO.IsolatedStorage;
using AppAprender;

namespace Animales
{
    public partial class animalNivel2 : PhoneApplicationPage
    {
        int contador = 0;
        DispatcherTimer timer = new DispatcherTimer();
        public animalNivel2()
        {
            InitializeComponent();
            correcto.Opacity = 0;
            imgIncorrecto.Opacity = 0;
            SBalternativa1.Begin();
            SBalternativa2.Begin();
            SBalternativa3.Begin();
            playSound();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            
        }

        void timer_Tick(object sender, EventArgs e)
        {
            contador = contador + 1;

            if (contador > 1 && imgIncorrecto.Opacity == 0.99500000476837158)
            {
                timer.Stop();
                contador = 0;
                SBIncorrectoBack.Begin();
            }
            if (contador > 1 && imgIncorrecto.Opacity == 0.0)
            {
                timer.Stop();
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                settings["Animales"] = 3;
                if (Convert.ToInt32(settings["animalesPuntaje"]) == 1)
                {
                    settings["animalesPuntaje"] = 2;
                }
                NavigationService.Navigate(new Uri("/../Animales/animalNivel3.xaml", UriKind.Relative));
                
            }
        }


        private void playSound()
        {
            Recursos recurso = new Recursos();
            recurso.reporducirSonido("Sonidos/perro.mp3");           //

        }
        private void alternativa1_Click_1(object sender, RoutedEventArgs e)
        {
            Recursos recurso = new Recursos();
            recurso.reporducirSonido("Sonidos/pollo.mp3");  
            if (correcto.Opacity == 1.0)
                correcto.Opacity = 0;
            SBIncorrecto.Begin();
            timer.Start();
        }

        private void alternativa2_Click_1(object sender, RoutedEventArgs e)
        {
            Recursos recurso = new Recursos();
            recurso.reporducirSonido("Sonidos/luciernaga.mp3");  
            if (correcto.Opacity == 1.0)
                correcto.Opacity = 0;
            SBIncorrecto.Begin();
            timer.Start();
        }

        private void alternativa3_Click_1(object sender, RoutedEventArgs e)
        {
            playSound();
            if (imgIncorrecto.Opacity == 0.99500000476837158)
                imgIncorrecto.Opacity = 0;
            SBCorrecto.Begin();
            timer.Start();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Uri.ToString() != "/Animales/animalNivel2.xaml")
            {
                NavigationService.RemoveBackEntry();
            }
            base.OnNavigatedTo(e);
        }
    }
}