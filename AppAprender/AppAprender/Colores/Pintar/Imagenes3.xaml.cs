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
using Microsoft.Devices;
using System.IO.IsolatedStorage;

namespace AppAprender
{
    public partial class Imagenes3 : PhoneApplicationPage
    {
        CompositeTransform dragTranslation;
        bool pintoBurbujas = false;
        bool pintoPes = false;
        bool pintoAgua = false;
        Recursos re;
        public Imagenes3()
        {
            InitializeComponent();
            this.Loaded += Imagenes3_Loaded;
        }

        void Imagenes3_Loaded(object sender, RoutedEventArgs e)
        {
            re = new Recursos();
            re.reporducirSonido("/Sonidos/Voces/PintarPez.mp3");
            dragTranslation = new CompositeTransform();
            
        }
        void Botones_Completed(object sender, EventArgs e)
        {
            re.reporducirSonido("/Sonidos/Voces/MuyBien.mp3");
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings["Colores"] = 4;
            if (Convert.ToInt16(settings["coloresPuntaje"].ToString()) == 2)
            {
                settings["coloresPuntaje"] = 3;
            }
            btnSiguiente.Click += btnHome_Click;
        }
        void btnHome_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri("/../../Colores/Mezclar/MainPage.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Ellipse_ManipulationStarted_1(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            dragTranslation.TranslateX = e.ManipulationOrigin.X - 45;
            dragTranslation.TranslateY = e.ManipulationOrigin.Y - 45;
        }

        private void Ellipse_ManipulationDelta_1(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            FrameworkElement elipse = sender as FrameworkElement;
            elipse.RenderTransform = dragTranslation;
            dragTranslation.TranslateX = dragTranslation.TranslateX + e.DeltaManipulation.Translation.X;
            dragTranslation.TranslateY = dragTranslation.TranslateY + e.DeltaManipulation.Translation.Y;
        }

        private void Ellipse_ManipulationCompleted_1(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            Recursos re = new Recursos();
            Ellipse elipse = sender as Ellipse;
            if (elipse.Name == "burbujas" && validarBurbujas(elipse,e)&&!pintoBurbujas)
            {                
                burbujasPescado.Source = new BitmapImage(new Uri("../../imagenes/coloresPintados/fish1_color.png", UriKind.Relative));
                pintoBurbujas = true;
                if (pintoPes && pintoAgua)
                {
                    Botones.Begin();
                    Botones.Completed += Botones_Completed;
                }
                else
                {
                    re.Correcto();
                }
            }
            else if (elipse.Name == "pez" && validarPez(elipse,e)&&!pintoPes)
            {
                pescado.Source = new BitmapImage(new Uri("../../imagenes/coloresPintados/fish2_color.png", UriKind.Relative));
                pintoPes = true;
                if ( pintoAgua && pintoBurbujas)
                {
                    Botones.Begin();
                    Botones.Completed += Botones_Completed;
                }
                else
                {
                    re.Correcto();
                }
            }
            else if (elipse.Name == "agua" && validarAgua(elipse, e)&&!pintoAgua)
            {
                fondoPescado.Source = new BitmapImage(new Uri("../../imagenes/coloresPintados/fish3_color.png", UriKind.Relative));
                pintoAgua = true;
                if (pintoPes && pintoBurbujas)
                {
                    Botones.Begin();
                    Botones.Completed += Botones_Completed;
                }
                else
                {
                    re.Correcto();
                }
            }
            else
            {
                //cuando es error
                re.Error();
            }
            elipse.RenderTransform = null;
        }

        private bool validarAgua(Ellipse elipse, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            double arriba = Canvas.GetTop(fondoPescado);
            double izquierda = Canvas.GetLeft(fondoPescado);
            double ancho = fondoPescado.Width;
            double alto = fondoPescado.Height;
            double xElipse = Canvas.GetLeft(elipse) + e.TotalManipulation.Translation.X;
            double yElipse = Canvas.GetTop(elipse) + e.TotalManipulation.Translation.Y;

             if (xElipse > izquierda && xElipse < (izquierda + ancho) && yElipse > arriba && yElipse < (arriba + alto)&&!validarPez(elipse,e) && !validarBurbujas(elipse, e))
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool validarPez(Ellipse elipse, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            double arriba = Canvas.GetTop(pescado);
            double izquierda = Canvas.GetLeft(pescado);
            double ancho = pescado.Width;
            double alto = pescado.Height;
            double xElipse = Canvas.GetLeft(elipse) + e.TotalManipulation.Translation.X;
            double yElipse = Canvas.GetTop(elipse) + e.TotalManipulation.Translation.Y;

            if (xElipse > izquierda && xElipse < (izquierda + ancho) && yElipse > arriba && yElipse < (arriba + alto))
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool validarBurbujas(Ellipse elipse, System.Windows.Input.ManipulationCompletedEventArgs e)
        {

            double arriba = Canvas.GetTop(burbujasPescado);
            double izquierda = Canvas.GetLeft(burbujasPescado);
            double ancho = burbujasPescado.Width;
            double alto = burbujasPescado.Height;
            double xElipse = Canvas.GetLeft(elipse)+e.TotalManipulation.Translation.X;
            double yElipse = Canvas.GetTop(elipse)+e.TotalManipulation.Translation.Y;

            if (xElipse > izquierda && xElipse < (izquierda + ancho) && yElipse > arriba && yElipse < (arriba + alto)&&!validarPez(elipse,e))
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Uri.ToString() != "/Colores/Pintar/Imagenes3.xaml")
            {
                NavigationService.RemoveBackEntry();
            }
            base.OnNavigatedTo(e);
        }

    }
}