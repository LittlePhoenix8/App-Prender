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
    public partial class LetraU : PhoneApplicationPage
    {
        DispatcherTimer timer = new DispatcherTimer();
        int contador = 0;
        bool boolUva = false;
        bool boolUno = false;
        Recursos recurso;
        public LetraU()
        {
            InitializeComponent();
            this.Loaded += LetraU_Loaded;
           

        }

        void LetraU_Loaded(object sender, RoutedEventArgs e)
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
                    recurso.reporducirSonido("/Sonidos/Voces/TocaComienzaU.mp3");
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    recurso.reporducirSonido("/Sonidos/Voces/Helado.mp3");
                    break;
                case 4:
                    Helado.Begin();
                    break;
                case 5:
                    recurso.reporducirSonido("/Sonidos/Voces/Globo.mp3");
                    break;
                case 6:
                    globo.Begin();
                    break;
                case 7:
                    recurso.reporducirSonido("/Sonidos/Voces/Television.mp3");
                    break;
                case 8:
                    televisor.Begin();
                    break;
                case 9:
                    recurso.reporducirSonido("/Sonidos/Voces/uva.mp3");
                    break;
                case 10:
                    Uva.Begin();
                    break;
                case 11:
                    recurso.reporducirSonido("/Sonidos/Voces/Uno.mp3");
                    break;
                case 12:
                    Uno.Begin();
                    
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
             imgGlobo.Tap += imgGlobo_Tap;
             imgHelado.Tap += imgGlobo_Tap;
             imgTele.Tap += imgGlobo_Tap;
             imgUno.Tap += imgGlobo_Tap;
             imgUva.Tap += imgGlobo_Tap;
         }

         void imgGlobo_Tap(object sender, System.Windows.Input.GestureEventArgs e)
         {
             Image img = sender as Image;
             if (img.Name == "imgUno")
             {
                 if (boolUva)
                 {
                     gano();
                 }
                 else
                 {
                     Uno.Begin();
                     boolUno = true;
                 }
             }
             else if (img.Name == "imgUva")
             {
                 if (boolUno)
                 {
                     gano();
                 }
                 else
                 {
                     Uva.Begin();
                     boolUva = true;
                 }
             }
             else
             {
             }
         }

         private void gano()
         {
             IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
             settings["Vocales"] = 1;
             if (Convert.ToInt16(settings.Contains("vocalesPuntaje")) == 4)
             {
                 settings["vocalesPuntaje"] = 5;
             }
             NavigationService.RemoveBackEntry();
             NavigationService.Navigate(new Uri("/../../Niveles.xaml", UriKind.Relative));
         }

       
    }
}