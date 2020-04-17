using BE;
using PL.Model;
using GoogleMapsApi.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.Commands;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace PL.ViewModel
{
    /// <summary>
    /// ViewModel class for the Hit Model and the New_Hit View
    /// </summary>
    public class HitViewModel : INotifyPropertyChanged, IHitViewModel
    {
        public string SelectedLocation { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> locations
        {
            get
            {
                return new ObservableCollection<string>(currentModel.locations);
            }
        }
        public MissleModel currentModel { set; get; }

        public Hit incomingHit
        {
            get
            {
                return currentModel.incomingHit;
            }
            set
            {
                currentModel.incomingHit = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("incomingHit"));
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void TextChange(string leters)
        {
            currentModel.checkLocations(leters);
        }

        private void locations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ///////do the observer
            return;

        }

        public AddHitCommand addHitCommand { set; get; }

        /// <summary>
        /// Constructor. create the Model and the ICommand for the View "submit" button
        /// </summary>
        public HitViewModel()
        {
            currentModel = new MissleModel();
            addHitCommand = new AddHitCommand(this);
            currentModel.PropertyChanged += (Property, EventArgs) =>
            {
                if (EventArgs.PropertyName == "locations")
                    OnPropertyChanged("locations");
            };
        }

    }
}
