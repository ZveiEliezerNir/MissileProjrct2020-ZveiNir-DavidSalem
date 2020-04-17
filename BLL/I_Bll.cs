using BE;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// Specify the behavior of the business logic layer. additional documentation in the implementation class.
    /// </summary>
    interface I_Bll
    {
        List<GeoCoordinate> k_Means(List<Report> report_List, int k);
        void addReport(Report report);
        void addHit(Hit hit);
        void removeHit(Hit _hit);

        void removeReport(Report _report);

        void updateReport(Report _report);

        void updateHit(Hit _hit);

        // reading methods
        Task<List<Report>> getAllReports();

         Task<List<Hit>> getAllHits();
    }
}
