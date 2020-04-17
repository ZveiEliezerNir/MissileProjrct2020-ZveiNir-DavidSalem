using Microsoft.Maps.MapControl.WPF;
using System.Drawing;
using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;
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
using System.Windows.Shapes;
using PL.ViewModel;
using BE;
using PL.Model;

namespace PL.Views
{
    /// <summary>
    /// Interaction logic for Data_Analysis.xaml
    /// </summary>
    public partial class Data_Analysis : Window
    {
        DataAnalysisViewModel dataAnalysisViewModel { set; get; }
        public List<Pushpin> pushpins { get; set; }

        /// <summary>
        /// Constructor, initialize the grapical components of the View
        /// </summary>
        public Data_Analysis()
        {
            InitializeComponent();            
            dataAnalysisViewModel = new DataAnalysisViewModel();            
            this.DataContext = dataAnalysisViewModel;
            pushpins = new List<Pushpin>();
            eventStartTime.IsEnabled = false;
            eventNum.IsEnabled = false;
            for (int i = 1; i < 10; ++i)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = i;
                eventNum.Items.Add(newItem);
            }
            for (int i = 1; i < 6; ++i)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = i;
                KmeansNum.Items.Add(newItem);
            }
        }

        /*
         *  event handlers: 
         */

        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {            
            dataAnalysisViewModel.allCurrentReport();            
        }

        public MColor ToMediaColor(DColor color)
        {
            return MColor.FromArgb(color.A, color.R, color.G, color.B);
        }

        private void MapCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            myMap.Mode = new AerialMode();
        }

        private void MapCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            myMap.Mode = new RoadMode();
        }

        private void DisplayByEvents_Checked(object sender, RoutedEventArgs e)
        {
            startTime.Text = null;
            startTime.IsEnabled = false;
            endTime.Text = null;
            endTime.IsEnabled = false;
            eventStartTime.IsEnabled = true;
            eventNum.IsEnabled = true;
        }       

        private void DisplayByEvents_Unchecked(object sender, RoutedEventArgs e)
        {
            startTime.IsEnabled = true;
            endTime.IsEnabled = true;
            eventStartTime.Text = null;
            eventStartTime.IsEnabled = false;
            eventNum.Text = null;
            eventNum.IsEnabled = false;
        }

        private void K_Means_Click(object sender, RoutedEventArgs e)
        {
            dataAnalysisViewModel.HitByK_Means();            
        }
                       
        private void AddHits_Click(object sender, RoutedEventArgs e)
        {
            dataAnalysisViewModel.allCurrentHits();
        }
    }
}
