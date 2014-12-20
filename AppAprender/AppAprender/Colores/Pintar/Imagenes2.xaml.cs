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
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace AppAprender
{
    public partial class Imagenes2 : PhoneApplicationPage
    {
        CompositeTransform dragTranslation;
        bool pintoRana = false;
        bool pintoLago = false;
        bool pintoPlantas = false;
        Recursos re;
        public Imagenes2()
        {
            InitializeComponent();
            this.Loaded += Imagenes2_Loaded;
        }

        void Imagenes2_Loaded(object sender, RoutedEventArgs e)
        {
            re = new Recursos();
            re.reporducirSonido("/Sonidos/Voces/PintarRana.mp3");
            dragTranslation = new CompositeTransform();
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
        void Botones_Completed(object sender, EventArgs e)
        {
            re.reporducirSonido("/Sonidos/Voces/MuyBien.mp3");
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings["Colores"] = 3;
            if (Convert.ToInt16(settings["coloresPuntaje"].ToString()) == 1)
            {
                settings["coloresPuntaje"] = 2;
            }
            btnSiguiente.Click += btnHome_Click;
        }
        void btnHome_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri("/../../Colores/Pintar/Imagenes3.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Ellipse_ManipulationCompleted_1(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            Recursos re = new Recursos();
            //fondoPescado.Source = new BitmapImage(new Uri("imagenes/coloresPintados/fish3_color.png", UriKind.Relative));
            Ellipse elipse = sender as Ellipse;
            if (elipse.Name == "rana" && validarRana(elipse,e)&&!pintoRana)
            {
                ranaDibujo.Source = new BitmapImage(new Uri("../../imagenes/coloresPintados/frog1_color.png", UriKind.Relative));
                pintoRana=true;
                if (pintoLago && pintoPlantas)
                {
                    Botones.Begin();
                    Botones.Completed += Botones_Completed;
                }
                else
                {
                    re.Correcto();
                }
            }
            else if (elipse.Name == "aguaLago" && validarAgua(elipse, e)&&!pintoLago)
            {
                lago.Source = new BitmapImage(new Uri("../../imagenes/coloresPintados/frog3_color.png", UriKind.Relative));
                pintoLago=true;
                if (pintoPlantas && pintoRana)
                {
                    Botones.Begin();
                    Botones.Completed += Botones_Completed;
                }
                else
                {
                    re.Correcto();
                }
            }
            else if (elipse.Name == "hojasLago" && validarHojas(elipse, e)&&!pintoPlantas)
            {
                hojas.Source = new BitmapImage(new Uri("../../imagenes/coloresPintados/frog2_color.png", UriKind.Relative));
                pintoPlantas = true;
                if (pintoRana && pintoLago)
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
                //Cuando existe error
                re.Error();
            }
            elipse.RenderTransform = null;
            
        }

        private bool validarHojas(Ellipse elipse, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            double arriba = Canvas.GetTop(hojas);
            double izquierda = Canvas.GetLeft(hojas);
            double ancho = hojas.Width;
            double alto = hojas.Height;
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

        private bool validarAgua(Ellipse elipse, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            double arriba = Canvas.GetTop(lago);
            double izquierda = Canvas.GetLeft(lago);
            double ancho = lago.Width;
            double alto = lago.Height;
            double xElipse = Canvas.GetLeft(elipse) + e.TotalManipulation.Translation.X;
            double yElipse = Canvas.GetTop(elipse) + e.TotalManipulation.Translation.Y;
            if (xElipse > izquierda && xElipse < (izquierda + ancho) && yElipse > arriba && yElipse < (arriba + alto)&&!validarHojas(elipse,e)&&!validarRana(elipse,e))
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool validarRana(Ellipse elipse, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            double arriba = Canvas.GetTop(ranaDibujo);
            double izquierda = Canvas.GetLeft(ranaDibujo);
            double ancho = ranaDibujo.Width;
            double alto = ranaDibujo.Height;
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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Uri.ToString() != "/Colores/Pintar/Imagenes2.xaml")
            {
                NavigationService.RemoveBackEntry();
            }
            base.OnNavigatedTo(e);
        }

    }
}