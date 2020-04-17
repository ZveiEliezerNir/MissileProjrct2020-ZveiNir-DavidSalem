using PL.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using PL.ViewModel;
using BLL;



namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }
        /*
                private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
                {
                    ButtonCloseMenu.Visibility = Visibility.Visible;
                    ButtonOpenMenu.Visibility = Visibility.Collapsed;
                }

                private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
                {
                    ButtonCloseMenu.Visibility = Visibility.Collapsed;
                    ButtonOpenMenu.Visibility = Visibility.Visible;
                }

        */
        private void Report_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (NewReportGrid.Visibility == Visibility.Hidden)
            {
                NewReportGrid.Visibility = Visibility.Visible;
                ReportViewModel reportViewModel = new ReportViewModel();
            }
            else
                NewReportGrid.Visibility = Visibility.Hidden;
            */     
             New_Report report = new New_Report();
             report.Show();

        }

        private void VerifiedHits_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (verifyGrid.Visibility == Visibility.Hidden)
                verifyGrid.Visibility = Visibility.Visible;
            else
                verifyGrid.Visibility = Visibility.Hidden;
            */
            
            New_Hit hit = new New_Hit();
            hit.Show();
            
        }

        private void DataAnalysis_Click(object sender, RoutedEventArgs e)
        {
            Data_Analysis data_Analysis = new Data_Analysis();
            data_Analysis.Show();
        }

        private void BrowseReports_Click(object sender, RoutedEventArgs e)
        {
            Browse_Report browse_Report = new Browse_Report();
            browse_Report.Show();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;            

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    usc = new UserControlHome();
                    //GridMap.Children.Add(usc);
                    break;
                case "ItemCreate":
                    usc = new UserControlCreate();
                    //GridMap.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
           WindowState = WindowState.Minimized;
        }

/*        private void ItemMap_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(GridMap.Visibility == Visibility.Hidden)
                GridMap.Visibility = Visibility.Visible;
            else
                GridMap.Visibility = Visibility.Hidden;
        }
*/
        /*
        /// <summary>
        /// Openf file dialog for a user to select an image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = false;
            Nullable<bool> dialogOK = fd.ShowDialog();

            if(dialogOK == true)
            {
                FileName.Text = fd.FileName;
            }
        }
        */
    }
}