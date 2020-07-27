using DotNetGame.Client.Entities;
using DotNetGame.Client.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DotNetGame.Client.UWP
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            btPlay.IsEnabled = true;
            //App.MainPageData = new MainPageData();
        }
    
        private void BtPlay_Click(object sender, RoutedEventArgs e)
        {
            App.ConnectedPlayer.Name = pseudoBox.Text;
            this.Frame.Navigate(typeof(MultiplayerPage));
        }

        private void PseudoBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (pseudoBox.Text != null)
            {
                btPlay.IsEnabled = true;
            }
            else
            {
                btPlay.IsEnabled = false;
            }
        }
    }
}
