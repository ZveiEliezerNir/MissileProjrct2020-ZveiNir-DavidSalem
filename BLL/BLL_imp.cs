using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using BE;
using DAL;

namespace BLL
{
    public class BLL_imp : I_Bll
    {
        Dal_imp currentDal;

        /// <summary>
        /// Constructor. creates an instance of the DAL, keeping the Dependency Inversion principle
        /// </summary>
        public BLL_imp()
        {
            currentDal = new Dal_imp();
        }

        /// <summary>
        /// controlls access to the K-Mean logic
        /// </summary>
        /// <param name="report_List"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public List<GeoCoordinate> k_MeansControler(List<Report> report_List, int k)
        {
            List<GeoCoordinate> result = new List<GeoCoordinate>();
            List<Report> temp = new List<Report>();
            Report tempreport = new Report();
            report_List = report_List.OrderBy(c => c.cityName).ToList();
            for (int i = 0; i < report_List.Count; i++)
            {
                temp.Add(report_List[i]);
                if (i == report_List.Count-1 || report_List[i].cityName!= report_List[i+1].cityName)
                {
                    foreach (var item in k_Means(temp, k))
                    {
                        result.Add(item);
                    }
                }
            }
            return result;
        }



        /// <summary>
        ///  the algorithm gets a list of reports and a number K. 
        ///  the algorithm finds K clusters and assigns for each report a cluster.
        ///  each cluster contains the nearest reports.
        /// </summary>
        /// <param name="report_List"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public List<GeoCoordinate> k_Means(List<Report> report_List, int k)
        {
            if (k > report_List.Count)
            {
                k = report_List.Count;
            }
            // Initiates list of central points       
            List<GeoCoordinate> ci_List = new List<GeoCoordinate>();
            double latitude_Min;
            double latitude_Max;
            double longitude_Min;
            double longitude_Max;

            if (report_List.Count == 0)
            {
                return null;
            }
            latitude_Min = report_List.Min(r => r.Latitude);
            latitude_Max = report_List.Max(r => r.Latitude);
            longitude_Min = report_List.Min(r => r.Longitude);
            longitude_Max = report_List.Max(r => r.Longitude);

            // We will generate k central point by finding random points in the surrounded area
            for (int i = 0; i < k; i++)
            {
                Random r = new Random(DateTime.Now.Ticks.GetHashCode());
                double latitude = latitude_Min + r.NextDouble() * (latitude_Max - latitude_Min);
                double longitude = longitude_Min + r.NextDouble() * (longitude_Max - longitude_Min);
                GeoCoordinate c = new GeoCoordinate(latitude, longitude);
                ci_List.Add(c);
            }

            bool is_Changed;
            do
            {
                is_Changed = false;
                // For each report we will find its closest center point
                for (int i = 0; i < report_List.Count; i++)
                {
                    double min = report_List[i].GetCoordinate().GetDistanceTo(ci_List[0]);
                    report_List[i].clusterId = 0;

                    for (int j = 1; j < ci_List.Count; j++)
                    {
                        double temp = report_List[i].GetCoordinate().GetDistanceTo(ci_List[j]);
                        if (temp < min)
                        {
                            min = temp;
                            report_List[i].clusterId = j;
                        }
                    }
                }

                report_List = report_List.OrderBy(c => c.clusterId).ToList();
                bool[] chooseCi = new bool[k];
                for (int i = 0; i < k; i++)
                {
                    chooseCi[i] = false;
                }

                for (int i = 0; i < report_List.Count; i++)
                {
                    chooseCi[report_List[i].clusterId] = true;
                }
                for (int i = 0; i < chooseCi.Length; i++)
                {
                    if (chooseCi[i] == false)
                    {
                        ci_List[i].Latitude = report_List[i].Latitude;
                        ci_List[i].Longitude = report_List[i].Longitude;
                    }
                }

                // finds new center points of each cluster
                int id = 0;
                double c_LongitudeSum = 0;
                double c_LatitudeSum = 0;
                int counter = 0;
                for (int i = 0; i < report_List.Count; i++)
                {
                    if (report_List[i].clusterId == id)
                    {
                        c_LatitudeSum += report_List[i].Latitude;
                        c_LongitudeSum += report_List[i].Longitude;
                        counter++;
                    }
                    else //(report_List[i].clusterId != id)
                    {
                        if (ci_List[id].Latitude != c_LatitudeSum / counter|| ci_List[id].Longitude!= c_LongitudeSum / counter)
                        {
                            ci_List[id].Latitude = c_LatitudeSum / counter;
                            ci_List[id].Longitude = c_LongitudeSum / counter;
                            is_Changed = true;
                        }                        
                        counter = 0;
                        c_LongitudeSum = 0;
                        c_LatitudeSum = 0;
                        i--;
                        id++;
                    }

                }
                if (ci_List[id].Latitude != c_LatitudeSum / counter || ci_List[id].Longitude != c_LongitudeSum / counter)
                {
                    ci_List[id].Latitude = c_LatitudeSum / counter;
                    ci_List[id].Longitude = c_LongitudeSum / counter;
                    is_Changed = true;
                }

            } while (is_Changed);

            return ci_List;
        }

        /// <summary>
        /// send a Report to the DAL for storage
        /// </summary>
        /// <param name="report"></param>
        public void addReport(Report report)
        {
            currentDal.addReport(report);
        }

        /// <summary>
        /// send a Hit to the DAL for storage
        /// </summary>
        /// <param name="hit"></param>
        public void addHit(Hit hit)
        {
            currentDal.addHit(hit);
        }

        /// <summary>
        /// send a Hit to the DAL for droping
        /// </summary>
        /// <param name="_hit"></param>
        public void removeHit(Hit _hit)
        {
            currentDal.removeHit(_hit);
        }

        /// <summary>
        /// send a Report to the DAL for droping
        /// </summary>
        /// <param name="_report"></param>
        public void removeReport(Report _report)
        {
            currentDal.removeReport(_report);
        }

        /// <summary>
        /// send a Report to the DAL for update
        /// </summary>
        /// <param name="_report"></param>
        public void updateReport(Report _report)
        {
            currentDal.updateReport(_report);
        }

        /// <summary>
        /// send a Hit to the DAL for update
        /// </summary>
        /// <param name="_hit"></param>
        public void updateHit(Hit _hit)
        {
            currentDal.updateHit(_hit);
        }

        /// <summary>
        /// retrieve all Report object form the DAL
        /// </summary>
        /// <returns></returns>
        public Task<List<Report>> getAllReports()
        {
            return currentDal.getAllReports();
        }

        /// <summary>
        /// retrieve all Hit object form the DAL
        /// </summary>
        /// <returns></returns>
        public Task<List<Hit>> getAllHits()
        {
            return currentDal.getAllHits();
        }
    }

}
