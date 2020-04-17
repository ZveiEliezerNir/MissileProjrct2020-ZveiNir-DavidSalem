using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using PL.Model;
using BE;

namespace PL.ViewModel
{
    /// <summary>
    /// specify the behavior of ViewModel class for the Report Model and the New_Report View
    /// </summary>
    public interface IReportViewModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        MissleModel currentModel { set; get; }
        Report incomingReport { set; get; }
    }
}
