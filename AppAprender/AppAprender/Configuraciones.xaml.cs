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

namespace AppAprender
{
    public partial class Configuraciones : PhoneApplicationPage
    {
        IsolatedStorageSettings settings;
        public Configuraciones()
        {
            InitializeComponent();
            this.Loaded += Configuraciones_Loaded;
        }

        void Configuraciones_Loaded(object sender, RoutedEventArgs e)
        {
            settings = IsolatedStorageSettings.ApplicationSettings;
        }

      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            settings["colocarMusicaFondo"] = instrucciones.IsChecked;
            settings["volumenMusicaFondo"] = Math.Round(sliderVolumen.Value, 0)/10;
            NavigationService.GoBack();
        }
    }
}