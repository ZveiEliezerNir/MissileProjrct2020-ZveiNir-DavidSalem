using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using BE;
namespace DAL
{
    public class Dal_imp : IDAL
    {
        /// <summary>
        /// add a Report to the local SQL DB
        /// </summary>
        /// <param name="_report"></param>
        public async void addReport(Report _report)
        {
            if (getReport(_report.EventID) != null)
            {
                return;
                //throw new Exception("you have entered this report already!");
            }
            using (var ctx = new DBContext())
            {
                ctx.Reports.Add(_report);
                await ctx.SaveChangesAsync();
            }
        }

        /// <summary>
        /// remove a Report from the local SQL DB
        /// </summary>
        /// <param name="_report"></param>
        public async void removeReport(Report _report)
        {
            if (getReport(_report.EventID) == null)
                return;
            using (var ctx = new DBContext())
            {
                ctx.Reports.Remove(_report);
                await ctx.SaveChangesAsync();
            }

        }

        /// <summary>
        /// update a Report in the local SQL DB
        /// </summary>
        /// <param name="_report"></param>
        public async void updateReport(Report _report)
        {
            if (getReport(_report.EventID) == null)
                return;
            using (var ctx = new DBContext())
            {
                ctx.Entry(_report);
                ctx.Reports.AddOrUpdate(_report);
                await ctx.SaveChangesAsync();
            }
        }

        /// <summary>
        /// add a Hit to the local SQL DB
        /// </summary>
        /// <param name="_hit"></param>
        public async void addHit(Hit _hit)
        {
            if (getHit(_hit.EventID) != null)
            {
                return;
                // throw new Exception("you have entered this hit already!");
            }
            using (var ctx = new DBContext())
            {
                ctx.Hits.Add(_hit);
                await ctx.SaveChangesAsync();
            }

        }

        /// <summary>
        /// remove a Hit to the local SQL DB
        /// </summary>
        /// <param name="_hit"></param>
        public async void removeHit(Hit _hit)
        {
            if (getHit(_hit.EventID) == null)
                return;
            using (var ctx = new DBContext())
            {
                ctx.Hits.Remove(_hit);
                await ctx.SaveChangesAsync();
            }
        }

        /// <summary>
        /// update a Hit to the local SQL DB
        /// </summary>
        /// <param name="_hit"></param>
        public async void updateHit(Hit _hit)
        {
            if (getHit(_hit.EventID) == null)
                return;
            using (var ctx = new DBContext())
            {
                ctx.Entry(_hit);
                ctx.Hits.AddOrUpdate(_hit);
                await ctx.SaveChangesAsync();
            }
        }

        /*public async Task<List<Report>> GetReportsAsync(Predicate<Report> predicate = null)
        {
            List<Report> reports;
            using (var db = new ProjectContext())
            {
                reports = await db.Reports.Include(report => report.Event).ToListAsync();
                if (predicate != null)
                    reports = reports.Where(r => predicate(r)).ToList();
            }
            return reports;
        }*/

        /// <summary>
        /// get all reoirt from the local DB
        /// </summary>
        /// <returns></returns>
        public async Task<List<Report>> getAllReports()
        {
            List<Report> reports;
            using (var ctx = new DBContext())
            {
                reports = await (from _report in ctx.Reports select _report).ToListAsync();                
            }
            return reports;
        }

        /*public Task<List<Report>> getAllReports()
        {
            Task<List<Report>> reports;
            var ctx = new DBContext();
            
                reports = (from _report in ctx.Reports select _report).ToListAsync();

            

            return reports;
        }*/

        /// <summary>
        /// get a Report object from the DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Report getReport(int id)
        {
            Report report;
            using (var ctx = new DBContext())
            {
                report = ctx.Reports.SingleOrDefault(r => r.EventID == id);
            }
            return report;
        }

        /// <summary>
        /// get a Hit object from the DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Hit getHit(int id)
        {
            Hit hit;
            using (var ctx = new DBContext())
            {
                hit = ctx.Hits.SingleOrDefault(r => r.EventID == id);
            }
            return hit;
        }

        /// <summary>
        /// get all Hit objects from the local DB
        /// </summary>
        /// <returns></returns>
        public async Task<List<Hit>> getAllHits()
        {
            List<Hit> hits;
            using (var ctx = new DBContext())
            {
                hits = await (from _hit in ctx.Hits
                              select _hit).ToListAsync();
            }
            return hits;
        }
    }
}
