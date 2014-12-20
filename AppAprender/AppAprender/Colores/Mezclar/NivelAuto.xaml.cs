using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using AppAprender;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;

namespace MezclaColores
{
    public partial class NivelAuto : PhoneApplicationPage
    {
        string nombreAnterior = "asd";
        CompositeTransform dragTranslation;
        Recursos recurso = new Recursos();
        int ver = 0;

        public NivelAuto()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            recurso.reporducirSonido("/Sonidos/Voces/MezclarColorVerde.mp3");
            dragTranslation = new CompositeTransform();
        }

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

        private void azul_ManipulationCompleted_1(object sender, ManipulationCompletedEventArgs e)
        {
            Ellipse elipse = sender as Ellipse;
            string nombreObjeto = (sender as Ellipse).Name;
            double elipsex = Canvas.GetLeft(rect) + e.TotalManipulation.Translation.X;
            double elipsey = Canvas.GetTop(rect) + e.TotalManipulation.Translation.Y;
            //Canvas.Left="79" Stroke="Black" Canvas.Top="122" Width="318"
            if (elipse.Name == "rosado" || elipse.Name == "azul" || elipse.Name == "amarillo")
            {
                if ((elipsex * -1) > 79 && (elipsex * -1) < 394)
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
                case "rosado":
                    switch (nombreAnterior)
                    {
                        case "azul":
                            recurso.Error();
                            ver = 1;
                            break;
                        case "amarillo":
                            recurso.Error();
                            ver = 1;
                            break;
                        case "rosado":
                            recurso.Error();
                            ver = 1;
                            break;
                        default:
                            recurso.Error();
                            ver = 1;
                            break;
                    }
                    break;
                case "azul":
                    switch (nombreAnterior)
                    {
                        case "rosado":
                            recurso.Error();
                            ver = 0;
                            break;
                        case "amarillo":
                            this.auto.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/car_green.png", UriKind.Relative));
                            recurso.Correcto();
                            ver = 0;
                            Botones.Begin();
                            Botones.Completed += Botones_Completed;
                            
                            break;
                        case "azul":
                            this.auto.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/car_blue.png", UriKind.Relative));
                            ver = 0;
                            break;
                        default:
                            this.auto.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/car_blue.png", UriKind.Relative));
                            ver = 0;
                            break;
                    }
                    break;
                case "amarillo":
                    switch (nombreAnterior)
                    {
                        case "rosado":
                            recurso.Error();
                            break;
                        case "azul":
                            this.auto.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/car_green.png", UriKind.Relative));
                            ver = 0;
                            recurso.Correcto();
                            Botones.Begin();
                            Botones.Completed += Botones_Completed;
                            break;
                        case "amarillo":
                            this.auto.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/car_yellow.png", UriKind.Relative));
                            ver = 0;
                            break;
                        default:
                            this.auto.Source = new BitmapImage(new Uri("../../imagenes/fotosMesclar/car_yellow.png", UriKind.Relative));
                            ver = 0;
                            break;
                    }
                    break;
            }
        }

        void Botones_Completed(object sender, EventArgs e)
        {
            recurso.reporducirSonido("/Sonidos/Voces/MuyBien.mp3");
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings["Colores"] = 1;
            if (Convert.ToInt16(settings["coloresPuntaje"].ToString()) == 5)
            {
                settings["coloresPuntaje"] = 6;
            }
            btnSiguiente.Click += btnSiguiente_Click;
            
        }

        void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../../Niveles.xaml", UriKind.Relative));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Uri.ToString() != "/Colores/Mezclar/NivelAuto.xaml")
            {
                NavigationService.RemoveBackEntry();
            }
            base.OnNavigatedTo(e);
        }
    }
}