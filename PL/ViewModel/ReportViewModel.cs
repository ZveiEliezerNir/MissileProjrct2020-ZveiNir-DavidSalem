using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BE;
using PL.Commands;
using PL.Model;

namespace PL.ViewModel
{
    /// <summary>
    /// This class is a ViewModel for the Report Entity and the New_Report View
    /// </summary>
    public class ReportViewModel : IReportViewModel, INotifyPropertyChanged
    {
        public string SelectedLocation { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public MissleModel currentModel { set; get; }
        public ObservableCollection<string> locations
        {
            get
            {
                return new ObservableCollection<string>(currentModel.locations);
            }
        }
        public AddReportCommand addReportCommand { set; get; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Report incomingReport
        {
            get
            {
                return currentModel.incomingReport;
            }
            set
            {
                currentModel.incomingReport = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("incomingReport"));
            }
        }

        public void TextChange(string leters)
        {
            currentModel.checkLocations(leters);            
        }

        /// <summary>
        /// Constructor, creates the Model and the ICommand for the "sumit" button of the View
        /// </summary>
        public ReportViewModel()
        {
            currentModel = new MissleModel();
            addReportCommand = new AddReportCommand(this);
            currentModel.PropertyChanged += (Property, EventArgs) =>
            {
                if (EventArgs.PropertyName == "locations")
                    OnPropertyChanged("locations");
            };
        }
    }
}

