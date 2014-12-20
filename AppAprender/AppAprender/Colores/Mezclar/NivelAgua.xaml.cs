using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using AppAprender;
using System.IO.IsolatedStorage;

namespace MezclaColores
{
    public partial class NivelAgua : PhoneApplicationPage
    {
        string nombreAnterior = "asd";
        CompositeTransform dragTranslation;
        Recursos recurso = new Recursos();
        int ver = 0;

        public NivelAgua()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            recurso.reporducirSonido("/Sonidos/Voces/MezclarColorCeleste.mp3");
            dragTranslation = new CompositeTransform();
        }

        private void negro_ManipulationStarted_1(object sender, ManipulationStartedEventArgs e)
        {
            dragTranslation.TranslateX = e.ManipulationOrigin.X - 50;
            dragTranslation.TranslateY = e.ManipulationOrigin.Y - 50;
        }

        private void negro_ManipulationDelta_1(object sender, ManipulationDeltaEventArgs e)
        {
            FrameworkElement elipse = sender as FrameworkElement;
            elipse.RenderTransform = dragTranslation;
            dragTranslation.TranslateX = dragTranslation.TranslateX + e.DeltaManipulation.Translation.X;
            dragTranslation.TranslateY = dragTranslation.TranslateY + e.DeltaManipulation.Translation.Y;
        }

        private void azul_ManipulationCompleted_1(object sender, ManipulationCompletedEventArgs e)
        {
            Ellipse elipse = sender as Ellipse;
            string nombreObjeto = (sender as Ellipse).Name;
            double elipsex = Canvas.GetLeft(rect) + e.TotalManipulation.Translation.X;
            double elipsey = Canvas.GetTop(rect) + e.TotalManipulation.Translation.Y;
            //Canvas.Left="102" Stroke="Black" Canvas.Top="122" Width="231"
            if (elipse.Name == "blanco" || elipse.Name == "azul" || elipse.Name == "verde")
            {
                if ((elipsex * -1) > 102 && (elipsex * -1) < 333)
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
                case "blanco":
                    switch (nombreAnterior)
                    {
                        case "azul":
                            this.vaso.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/water_cyan.png", UriKind.Relative));
                            recurso.Correcto();
                            ver = 0;
                            Botones.Begin();
                            Botones.Completed += Botones_Completed;
                            break;
                        case "verde":
                            recurso.Error();
                            ver = 0;
                            break;
                        case "blanco":
                            this.vaso.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/water_white.png", UriKind.Relative));
                            ver = 0;
                            break;
                        default:
                            this.vaso.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/water_white.png", UriKind.Relative));
                            ver = 0;
                            break;
                    }
                    break;
                case "azul":
                    switch (nombreAnterior)
                    {
                        case "blanco":
                            this.vaso.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/water_cyan.png", UriKind.Relative));
                            recurso.Correcto();
                            ver = 0;
                            Botones.Begin();
                            Botones.Completed += Botones_Completed;
                            break;
                        case "verde":
                            recurso.Error();
                            ver = 0;
                            break;
                        case "azul":
                            this.vaso.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/water_blue.png", UriKind.Relative));
                            ver = 0;
                            break;
                        default:
                            this.vaso.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/water_blue.png", UriKind.Relative));
                            ver = 0;
                            break;
                    }
                    break;
                case "verde":
                    switch (nombreAnterior)
                    {
                        case "blanco":
                            recurso.Error();
                            ver = 1;
                            break;
                        case "azul":
                            recurso.Error();
                            ver = 1;
                            break;
                        case "verde":
                            recurso.Error();
                            ver = 1;
                            break;
                        default:
                            recurso.Error();
                            ver = 1;
                            break;
                    }
                    break;
            }
        }

        void Botones_Completed(object sender, EventArgs e)
        {
            recurso.reporducirSonido("/Sonidos/Voces/MuyBien.mp3");
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings["Colores"] = 6;
            if (Convert.ToInt16(settings["coloresPuntaje"].ToString()) == 4)
            {
                settings["coloresPuntaje"] = 5;
            }
            btnSiguiente.Click += btnSiguiente_Click;                   
        }

        void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../../Colores/Mezclar/NivelAuto.xaml", UriKind.Relative));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Uri.ToString() != "/Colores/Mezclar/NivelAgua.xaml")
            {
                NavigationService.RemoveBackEntry();
            }
            base.OnNavigatedTo(e);
        }
    }
}