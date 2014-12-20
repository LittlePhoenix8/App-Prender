using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Devices;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace AppAprender
{
    public partial class Imagenes1 : PhoneApplicationPage
    {
        CompositeTransform dragTranslation;
        
        
        bool pintoPelota = false;
        bool pintoGato = false;
        Recursos re;
        public Imagenes1()
        {
            InitializeComponent();
            this.Loaded += Imagenes1_Loaded;
        }

        void Imagenes1_Loaded(object sender, RoutedEventArgs e)
        {
            re = new Recursos();
            re.reporducirSonido("/Sonidos/Voces/PintarGatoPelota.mp3");
            
            dragTranslation = new CompositeTransform();        
        }

        void Botones_Completed(object sender, EventArgs e)
        {
            re.reporducirSonido("/Sonidos/Voces/MuyBien.mp3");
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings["Colores"] = 2;
            if (Convert.ToInt16(settings["coloresPuntaje"].ToString()) == 0)
            {
                settings["coloresPuntaje"] = 1;
            }
            btnSiguiente.Click += btnHome_Click;
        }

        void btnHome_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri("/../../Colores/Pintar/Imagenes2.xaml", UriKind.Relative));
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
            //fondoPescado.Source = new BitmapImage(new Uri("imagenes/coloresPintados/fish3_color.png", UriKind.Relative));
            Ellipse elipse = sender as Ellipse;
            if (elipse.Name == "colorPelota" && validacionPelota(elipse, e)&&!pintoPelota)
            {
                pelotaGato.Source = new BitmapImage(new Uri("../../imagenes/coloresPintados/cat2_color.png", UriKind.Relative));
                pintoPelota = true;
                if (pintoGato == true)
                {
                    Botones.Begin();
                    Botones.Completed += Botones_Completed;
                    //re.TodoBien();
                }
                else
                {
                    re.Correcto();
                }
            }
            else if (elipse.Name == "colorGato" && validacionGato(elipse, e)&&!pintoGato)
            {
                gato.Source = new BitmapImage(new Uri("../../imagenes/coloresPintados/cat1_color.png", UriKind.Relative));
                pintoGato = true;
                if (pintoPelota == true)
                {
                    
                    //re.TodoBien();
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
                //Cuando es error
                re.Error();
            }
            elipse.RenderTransform = null;
        }

        private bool validacionGato(Ellipse elipse, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            double arriba = Canvas.GetTop(gato);
            double izquierda = Canvas.GetLeft(gato);
            double ancho = gato.Width;
            double alto = gato.Height;
            double xElipse = Canvas.GetLeft(elipse) + e.TotalManipulation.Translation.X;
            double yElipse = Canvas.GetTop(elipse) + e.TotalManipulation.Translation.Y;

            if (xElipse > izquierda && xElipse < (izquierda + ancho) && yElipse > arriba && yElipse < (arriba + alto) && !validacionPelota(elipse, e))
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool validacionPelota(Ellipse elipse, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            double arriba = Canvas.GetTop(pelotaGato);
            double izquierda = Canvas.GetLeft(pelotaGato);
            double ancho = pelotaGato.Width;
            double alto = pelotaGato.Height;
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
    }
}