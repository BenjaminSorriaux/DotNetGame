using DotNetGame.Entities;
using DotNetGame.Server.Services;
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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace DotNetGame.Client.UWP
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        public GamePage()
        {
            this.InitializeComponent();
            App.ConnectedPlayer = App.Client.Game.ConnectedPlayers.Where(p => p.Name == App.ConnectedPlayer.Name).FirstOrDefault();
            lvDevAv.ItemsSource = App.Client.Game.DevelopersAvailable;
            lvDev.ItemsSource = App.ConnectedPlayer.Developers;
            budgetBlock.Text = App.ConnectedPlayer.Money.ToString();
            budget3Block.Text = App.ConnectedPlayer.Projects.Where(p => p.RemainingDays < 3).ToList().Sum(p => p.Reward).ToString();
            costBlock.Text = App.ConnectedPlayer.Developers.Sum(d => d.Salary).ToString();
            lvProjectAv.ItemsSource = App.Client.Game.ProjectsAvailable;
            lvProj.ItemsSource = App.ConnectedPlayer.Projects;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //App.Client.Game.ConnectedPlayers.Remove(App.Client.Game.ConnectedPlayers.Where(p => p.Id == App.ConnectedPlayer.Id).FirstOrDefault());
            //App.Client.Game.ConnectedPlayers.Add(App.ConnectedPlayer);
            App.Client.sendData(App.ConnectedPlayer);
        }

        private void tapped_dev_av(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                var test = lvDevAv.SelectedItem;
                Developer devTemp = (Developer)test;
                if (App.ConnectedPlayer.Money >= devTemp.PurchaseCost)
                {
                    App.ConnectedPlayer.Developers.Add(devTemp);
                    App.Client.Game.DevelopersAvailable.Remove(devTemp);
                    App.ConnectedPlayer.Money -= devTemp.PurchaseCost;
                }
                this.Frame.Navigate(typeof(GamePage));
            }
            catch (Exception)
            {


            }
            
        }

        private void tapped_dev(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                var test = lvDev.SelectedItem;
                Developer devTemp = (Developer)test;
                if (App.ConnectedPlayer.Money >= devTemp.PurchaseCost)
                {
                    App.ConnectedPlayer.Developers.Remove(devTemp);
                    App.Client.Game.DevelopersAvailable.Add(devTemp);
                    App.ConnectedPlayer.Money += devTemp.PurchaseCost;
                }
                this.Frame.Navigate(typeof(GamePage));
            }
            catch (Exception)
            {


            }
            
        }

        private void tapped_proj(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                var test = lvProjectAv.SelectedItem;
                Project proj = (Project)test;
                App.ConnectedPlayer.Projects.Add(proj);
                App.Client.Game.ProjectsAvailable.Remove(proj);
                this.Frame.Navigate(typeof(GamePage));
            }
            catch (Exception)
            {

            }
            
        }

        private void tapped_player_proj(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                var test = lvProjectAv.SelectedItem;
                Project proj = (Project)test;
                App.ConnectedPlayer.Projects.Remove(proj);
                App.Client.Game.ProjectsAvailable.Add(proj);
                this.Frame.Navigate(typeof(GamePage));
            }
            catch (Exception)
            {


            }
            
        }
    }
}
