using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BE;
using PL.Model;

namespace PL.ViewModel
{
    /// <summary>
    /// The ViewModel class for the extra analytics
    /// </summary>
    public class BrowseReportsViewModel : INotifyPropertyChanged
    {
        public MissleModel currentModel { set; get; }
        public ObservableCollection<Report> North { get; set; }
        public ObservableCollection<Report> South { get; set; }
        public ObservableCollection<Report> Center { get; set; }
        public ObservableCollection<Report> Other { get; set; }

        public BrowseReportsViewModel()
        {
            currentModel = new MissleModel();
            North = new ObservableCollection<Report>();
            South = new ObservableCollection<Report>();
            Center = new ObservableCollection<Report>();
            Other = new ObservableCollection<Report>();
            North.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged("North");
            };
            South.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged("Center");
            };
            Center.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged("South");
            };
            Other.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged("Other");
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// get all Report in the system and divide them to four categories
        /// </summary>
        public async void getAllReports()
        {
            var allreport = await currentModel.allReports();
            foreach (var r in allreport)
            {
                if (r.Latitude > 32.144343 && r.Latitude <= 33.333795 && r.Longitude > 34.217333 && r.Longitude < 35.672361)
                {
                    North.Add(r);
                }
                else if (r.Latitude > 31.571548 && r.Latitude <= 32.144343 && r.Longitude > 34.217333 && r.Longitude < 35.672361)
                {
                    Center.Add(r);
                }
                else if (r.Latitude > 29.497512 && r.Latitude <= 31.571548 && r.Longitude > 34.217333 && r.Longitude < 35.672361)
                {
                     South.Add(r);
                }
                else
                {
                    Other.Add(r);
                }
            }

        }
    }
}
