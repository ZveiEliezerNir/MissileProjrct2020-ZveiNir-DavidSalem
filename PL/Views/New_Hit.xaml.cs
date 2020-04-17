using PL.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.IO;
namespace PL.Views
{
    /// <summary>
    /// Interaction logic for New_Hit.xaml
    /// </summary>
    public partial class New_Hit : Window
    {

        public string path = "D:/forth year winter/dotNET/WPFMissleProject_5356_57804/PL/PL/Images";
        public BitmapImage bmi;
        public string destFile;

        public HitViewModel hitViewModel { set; get; }
        //public AutoCompleteBox

        /// <summary>
        /// Constructor, creates the ViewMoedl and set it as its DataContext
        /// </summary>
        public New_Hit()
        {
            hitViewModel = new HitViewModel();
            this.DataContext = hitViewModel;
            InitializeComponent();
        }

        /*
         * event handlers:
         */

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HitLocation_TextChanged(object sender, RoutedEventArgs e)
        {
            hitViewModel.TextChange(location.Text);
        }

        private void lstImage_Drop(object sender, DragEventArgs e)
        {
            //Only supports file drag and drop
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            //Drag the file access
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            //Note that, because the program supports both pulled also supports drag the past, then ListBox can receive its drag and drop files
            //In order to prevent conflict mouse clicking and dragging, need to be shielded from the program itself to drag the file
            //Here to determine whether a document from outside the program drag in, is to determine the image is in the working directory
            if (files.Length > 0 && !files[0].StartsWith(path) &&
                (e.AllowedEffects & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

            foreach (string file in files)
            {
                try
                {
                    //If the image is from the external drag in, make a backup copy of the file to the working directory
                    destFile = path + System.IO.Path.GetFileName(file);

                    switch (e.Effects)
                    {
                        case DragDropEffects.Copy:
                            File.Copy(file, destFile, true);
                            bmi = new BitmapImage(new Uri(destFile));
                            imgShow.Source = bmi;
                            lstImage.Items.Add(destFile);
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    MessageBox.Show("Already exists in this file or import the non image files！");
                }

            }
        }

        private void lstImage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstImage.SelectedIndex > -1)
            {
                //Use only the Listbox radio function
                string[] files = new string[1];
                files[0] = lstImage.SelectedItem.ToString();

                DragDrop.DoDragDrop(lstImage, new DataObject(DataFormats.FileDrop, files), DragDropEffects.Copy | DragDropEffects.Move /* | DragDropEffects.Link */);
            }
        }
    }
}
