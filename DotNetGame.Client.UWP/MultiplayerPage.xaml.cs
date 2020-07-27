
using DotNetGame.Client.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace DotNetGame.Client.UWP
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MultiplayerPage : Page
    {
        public MultiplayerPage()
        {
            this.InitializeComponent();
            PseudoBlock.Text = App.ConnectedPlayer.Name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Set ip and port of the App
            App.Ip = IPAddressInput.Text;
            App.Port = Convert.ToInt32(PortInput.Text);
            //Call
            App.Client.SocketClient(App.Ip,App.Port, App.ConnectedPlayer);
            //call the with an obj and ip / port
            //App.Client.sendData(App.ConnectedPlayer);
            //App.Client.SocketClient(IPAddressInput.Text, Int32.Parse(PortInput.Text));
            while (App.Client.Game == null)
            {
                Thread.Sleep(1);
            }
            this.Frame.Navigate(typeof(GamePage));
        }

        private void IPAddressInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PortInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
