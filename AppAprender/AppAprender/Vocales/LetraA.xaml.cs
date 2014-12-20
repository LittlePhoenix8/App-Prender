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
using System.Windows.Threading;
using AppAprender;

namespace Vocales
{
    public partial class MainPage : PhoneApplicationPage
    {
        DispatcherTimer timer = new DispatcherTimer();
        Recursos re = new Recursos();
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            re.reporducirSonido("/Sonidos/Voces/LetraA.mp3");
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(4000);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            siguiente.Completed += siguiente_Completed;
            siguiente.Begin();
            timer.Stop();
            
        }

        void siguiente_Completed(object sender, EventArgs e)
        {
            btnSiguienteA.Click += button_Click_1;
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/../Vocales/Letras/ImagenesLetraA.xaml", UriKind.Relative));
        }
        
    }
}