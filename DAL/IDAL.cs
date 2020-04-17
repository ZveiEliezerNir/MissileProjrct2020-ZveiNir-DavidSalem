using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    /// <summary>
    /// specify the behavior of the data access layer. additional documentation in the implementation class.
    /// </summary>
    interface IDAL
    {
        void addReport(Report _report);    

        void addHit(Hit _hit);

        void removeHit(Hit _hit);

        void removeReport(Report _report);

        void updateReport(Report _report);

        void updateHit(Hit _hit);

        // reading methods
        Task<List<Report>> getAllReports();

        Task<List<Hit>> getAllHits();

        Report getReport(int id);

        Hit getHit(int id);

    }
}
