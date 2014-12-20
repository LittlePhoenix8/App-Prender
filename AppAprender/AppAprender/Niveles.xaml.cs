using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework.Media;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework;

namespace AppAprender
{
    public partial class NivelesVer2 : PhoneApplicationPage
    {
        int pantalla = 1;
        IsolatedStorageSettings settings;
        public NivelesVer2()
        {
            InitializeComponent();
            this.Loaded += NivelesVer2_Loaded;      
        }

        void NivelesVer2_Loaded(object sender, RoutedEventArgs e)
        {
            settings = IsolatedStorageSettings.ApplicationSettings;
            obtenerPuntajes();
            
        }

       

        private void obtenerPuntajes()
        {
            if (settings.Contains("coloresPuntaje"))
            {
                imgBotonesColores.Source = new BitmapImage(new Uri("Icons/psicologia-infantil-colores.jpg", UriKind.Relative));
                if (Convert.ToInt32(settings["coloresPuntaje"]) == 6)
                {
                    imgBotonesAnimales.Source = new BitmapImage(new Uri("Icons/animales.jpg", UriKind.Relative));
                    fakeAnimal.Click += fakeAnimal_Click;
                }
                else
                {
                    imgBotonesAnimales.Source = new BitmapImage(new Uri("Icons/animalesBloqueado.png", UriKind.Relative));
                    
                }
            }
            else
            {
                settings.Add("coloresPuntaje", 0);
                imgBotonesColores.Source = new BitmapImage(new Uri("Icons/psicologia-infantil-colores.jpg", UriKind.Relative));
                imgBotonesAnimales.Source = new BitmapImage(new Uri("Icons/animalesBloqueado.png", UriKind.Relative));
            }
            if (settings.Contains("animalesPuntaje"))
            {
                if (Convert.ToInt32(settings["animalesPuntaje"]) == 3)
                {
                    imgBotonesVocales.Source = new BitmapImage(new Uri("Icons/vocales.png", UriKind.Relative));
                    btnVocales.Click += btnVocales_Click;
                }
                else
                {
                    imgBotonesVocales.Source = new BitmapImage(new Uri("Icons/vocalesBloqueado.png", UriKind.Relative));
                }
            }
            else
            {
                settings.Add("animalesPuntaje", 0);
                imgBotonesVocales.Source = new BitmapImage(new Uri("Icons/vocalesBloqueado.png", UriKind.Relative));
            }
            if (settings.Contains("vocalesPuntaje"))
            {

            }
            else
            {
                settings.Add("vocalesPuntaje", 0);
            }

        }

       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int nivel=1;
            if (settings.Contains("Colores"))
            {
                nivel = Convert.ToInt32(settings["Colores"].ToString());
            }
            else
            {
                settings.Add("Colores", 1);
            }
            navegarNivelesColores(nivel);
          
        }

        private void navegarNivelesColores(int nivel)
        {
            switch (nivel)
            {
                case 1:
                    NavigationService.Navigate(new Uri("/Colores/Pintar/Imagenes1.xaml", UriKind.Relative));
                    break;
                case 2:
                    NavigationService.Navigate(new Uri("/Colores/Pintar/Imagenes2.xaml", UriKind.Relative));
                    break;
                case 3:
                    NavigationService.Navigate(new Uri("/Colores/Pintar/Imagenes3.xaml", UriKind.Relative));
                    break;
                case 4:
                    NavigationService.Navigate(new Uri("/Colores/Mezclar/MainPage.xaml", UriKind.Relative));
                    break;
                case 5:
                    NavigationService.Navigate(new Uri("/Colores/Mezclar/NivelAgua.xaml", UriKind.Relative));
                    break;
                case 6:
                    NavigationService.Navigate(new Uri("/Colores/Mezclar/NivelAuto.xaml", UriKind.Relative));
                    break;

            }
            
        }

       

        private void fakeAnimal_Click(object sender, RoutedEventArgs e)
        {
            int nivel = 1;
            if (settings.Contains("Animales"))
            {
                nivel = Convert.ToInt32(settings["Animales"].ToString());
            }
            else
            {
                settings.Add("Animales", nivel);
            }
            navegarNivelesAnimales(nivel);
        }
        private void navegarNivelesAnimales(int nivel)
        {
            switch (nivel)
            {
                case 1:
                    NavigationService.Navigate(new Uri("/Animales/animalNivel1.xaml", UriKind.Relative));
                    break;
                case 2:
                    NavigationService.Navigate(new Uri("/Animales/animalNivel2.xaml", UriKind.Relative));
                    break;
                case 3:
                    NavigationService.Navigate(new Uri("/Animales/animalNivel3.xaml", UriKind.Relative));
                    break;

            }

        }
        private void GestureListener_Flick(object sender, FlickGestureEventArgs e)
        {
            if (e.Direction.ToString() == "Horizontal")
            {
                if (e.HorizontalVelocity > 0)
                {
                    switch (pantalla)
                    {
                        case 2:
                            AnimacionDosDerecha.Begin();
                            pantalla--;
                            break;
                        case 3:
                            AnimacionTresDerecha.Begin();
                            pantalla--;
                            break;
                        case 4:
                            AnimacionCuatroDerecha.Begin();
                            pantalla--;
                            break;
                    }
                }
                else
                {
                    switch (pantalla)
                    {
                        case 1:
                            AnimacionUno.Begin();
                            pantalla++;
                            break;
                        case 2:
                            AnimacionDosIzquierda.Begin();
                            pantalla++;
                            break;
                        case 3:
                            AnimacionTresIzquierda.Begin();
                            pantalla++;
                            break;
                    }

                }
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
           
            if (e.Uri.ToString() == "/../../Niveles.xaml")
            {
                NavigationService.RemoveBackEntry();
                NavigationService.RemoveBackEntry();
            }
            else if (e.Uri.ToString() == "/../Niveles.xaml")
            {
                NavigationService.RemoveBackEntry();
                NavigationService.RemoveBackEntry();
            }
            Recursos recurso = new Recursos();
            recurso.pararSonido();

            base.OnNavigatedTo(e);
        }

        private void btnVocales_Click(object sender, RoutedEventArgs e)
        {
            int nivel = 1;
            if (settings.Contains("Vocales"))
            {
                nivel = Convert.ToInt32(settings["Vocales"].ToString());
            }
            else
            {
                settings.Add("Vocales", 1);
            }
            
            navegarNivelesVocales(nivel);
        }

        private void navegarNivelesVocales(int nivel)
        {
            switch (nivel)
            {
                case 1:
                    NavigationService.Navigate(new Uri("/Vocales/LetraA.xaml", UriKind.Relative));
                    break;
                case 2:
                    NavigationService.Navigate(new Uri("/Vocales/LetraE.xaml", UriKind.Relative));
                    break;
                case 3:
                    NavigationService.Navigate(new Uri("/Vocales/LetraI.xaml", UriKind.Relative));
                    break;
                case 4:
                    NavigationService.Navigate(new Uri("/Vocales/LetraO.xaml", UriKind.Relative));
                    break;
                case 5:
                    NavigationService.Navigate(new Uri("/Vocales/LetraU.xaml", UriKind.Relative));
                    break;

            }
        }
    }
}