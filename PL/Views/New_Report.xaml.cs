using System;
using System.Windows;
using System.Windows.Controls;
using PL.ViewModel;

namespace PL.Views
{
    /// <summary>
    /// Interaction logic for New_Report.xaml
    /// New_Report is thw "Model" class for the Report Entity
    /// </summary>
    public partial class New_Report : Window
    {
        public ReportViewModel reportViewModel { set; get; }

        /// <summary>
        /// Constructor, Creates the ViewModel and set it as its DataContext
        /// </summary>
        public New_Report()
        {            
            reportViewModel = new ReportViewModel();
            this.DataContext = reportViewModel;
            InitializeComponent();
            
            for (int i = 1; i < 9; ++i)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content =  i;
                NumOfFalls.Items.Add(newItem);
            }
            
        }

        /// <summary>
        /// The real submit happens with the "ICommand" logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ReportLocation_TextChanged(object sender, RoutedEventArgs e)
        {
                reportViewModel.TextChange(Location.Text);
        }
    }
}
