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
using System.Windows.Media.Imaging;
using AppAprender;
using System.IO.IsolatedStorage;
using System.Windows.Navigation;

namespace MezclaColores
{
    public partial class MainPage : PhoneApplicationPage
    {
        string nombreAnterior = "asd";
        CompositeTransform dragTranslation;
        Recursos recurso = new Recursos();
        int ver = 0;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            recurso.reporducirSonido("/Sonidos/Voces/MezclarColorMorado.mp3");
            dragTranslation = new CompositeTransform();
        }

        // Al hacer click en el circulo
        private void negro_ManipulationStarted_1(object sender, ManipulationStartedEventArgs e)
        {
            dragTranslation.TranslateX = e.ManipulationOrigin.X - 50;
            dragTranslation.TranslateY = e.ManipulationOrigin.Y - 50;
        }

        // Al arrastrar el circulo.
        private void negro_ManipulationDelta_1(object sender, ManipulationDeltaEventArgs e)
        {
            FrameworkElement elipse = sender as FrameworkElement;
            elipse.RenderTransform = dragTranslation;
            dragTranslation.TranslateX = dragTranslation.TranslateX + e.DeltaManipulation.Translation.X;
            dragTranslation.TranslateY = dragTranslation.TranslateY + e.DeltaManipulation.Translation.Y;
        }

        // Al soltar el circulo. Comprueba coordenadas.
        private void azul_ManipulationCompleted_1(object sender, ManipulationCompletedEventArgs e)
        {
            Ellipse elipse = sender as Ellipse;
            string nombreObjeto = (sender as Ellipse).Name;
            double elipsex = Canvas.GetLeft(rect) + e.TotalManipulation.Translation.X;
            double elipsey = Canvas.GetTop(rect) + e.TotalManipulation.Translation.Y;
            //Canvas.Left="130" Canvas.Top="138"  Width="187"
            if (elipse.Name == "rojo" || elipse.Name == "azul" || elipse.Name == "amarillo")
            {
                if ((elipsex * -1) > 130 && (elipsex * -1) < 317)
                {
                    CambiarColores(nombreObjeto, nombreAnterior);
                }
            }
                //??
            else
            {
                if ((e.TotalManipulation.Translation.Y * -1) > 312 && (e.TotalManipulation.Translation.Y * -1) < 560)
                {
                    CambiarColores(nombreObjeto, nombreAnterior);
                }
            }

            if (ver == 0)
            {
                nombreAnterior = (sender as Ellipse).Name;
            }
            elipse.RenderTransform = null;
            dragTranslation = new CompositeTransform();
        }

        private void CambiarColores(string nombreObjeto, string nombreAnterior)
        {
            switch (nombreObjeto)
            {
                case "rojo":
                    switch (nombreAnterior)
                    {
                        case "azul":
                            this.flor.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/flor_purple.png", UriKind.Relative));
                            recurso.Correcto();
                            ver = 0;
                            Botones.Completed += Botones_Completed;
                            Botones.Begin();
                            break;
                        case "amarillo":
                            recurso.Error();
                            break;
                        case "rojo":
                            this.flor.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/flor_red.png", UriKind.Relative));
                            ver = 0;
                            break;
                        default:
                            this.flor.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/flor_red.png", UriKind.Relative));
                            ver = 0;
                            break;
                    }
                    break;
                case "azul":
                    switch (nombreAnterior)
                    {
                        case "rojo":
                            this.flor.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/flor_purple.png", UriKind.Relative));
                            recurso.Correcto();
                            ver = 0;
                            Botones.Completed += Botones_Completed;
                            Botones.Begin();
                            break;
                        case "amarillo":
                            recurso.Error();
                            break;
                        case "azul":
                            this.flor.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/flor_blue.png", UriKind.Relative));
                            ver = 0;
                            break;
                        default:
                            this.flor.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/flor_blue.png", UriKind.Relative));
                            ver = 0;
                            break;
                    }
                    break;
                case "amarillo":
                    switch (nombreAnterior)
                    {
                        case "rojo":
                            recurso.Error();
                            ver = 1;
                            break;
                        case "azul":
                            recurso.Error();
                            ver = 1;
                            break;
                        case "amarillo":
                            recurso.Error();
                            ver = 1;
                            break;
                        default:
                            recurso.Error();
                            break;
                    }
                    break;
            }
        }

        void Botones_Completed(object sender, EventArgs e)
        {
            recurso.reporducirSonido("/Sonidos/Voces/MuyBien.mp3");
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings["Colores"] = 5;
            if (Convert.ToInt16(settings["coloresPuntaje"].ToString()) == 3)
            {
                settings["coloresPuntaje"] = 4;
            }
            btnSiguiente.Click += btnSiguiente_Click;
        }

        void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../../Colores/Mezclar/NivelAgua.xaml", UriKind.Relative));
        }

        private void Combinaciones_Tap_1(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            //Combinaciones.Fill = new SolidColorBrush(Colors.White);
            nombreAnterior = "asd";
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Uri.ToString() != "/Colores/Mezclar/MainPage.xaml")
            {
                NavigationService.RemoveBackEntry();
            }
            base.OnNavigatedTo(e);
        }
    }
}