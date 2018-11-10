using MySql.Data.MySqlClient;
using OmnitrackTma.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;


namespace OmnitrackTma.Controllers
{
    public class EngagementServiceController : Controller
    {
       
        OmniTrackTmaContext db = new OmniTrackTmaContext();
        private MySqlConnection connection = new MySqlConnection("server=localhost;user id=root;database=omnitracktma");
        public ActionResult Index()
        {
            var projet = (from bt in db.mantis_project_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                          bt.id == mth.parent_id
                          select new
                          {
                              bt.name,
                              bt.id
                          }).Distinct();
           

            
          



            ViewBag.projet = projet;
            return View();
        }
        public JsonResult GetYearbyProjectE(int p)
        {
            var year = (from mt in db.mantis_bug_table
                        from mht in db.mantis_project_hierarchy_table
                        where
                          mht.parent_id == p
                          && mt.project_id == mht.child_id

                        select new
                        {
                            mt.last_updated.Year
                        }).Distinct();

            return new JsonResult { Data = year, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //_____________________________________Les Mois par anné ___________________________________________________________


        public JsonResult GetDataDelai(int y, int idp, int priorite)
        {
           
            //string year = Convert.ToString(y);
            //string id = Convert.ToString(idp);
            //string prio = Convert.ToString(priorite);
            connection.Open();
            String Query = "SELECT month(last_updated), SEC_TO_TIME(avg(unix_timestamp(`last_updated`)-unix_timestamp(`date_submitted`))) as time FROM `mantis_bug_table`, `mantis_project_hierarchy_table` WHERE year(`last_updated`) = @year and `status` = 90 and `project_id`=`child_id` and priority = @prio and `parent_id` = @id group by month(last_updated)";
            MySqlCommand cmd = new MySqlCommand(Query, connection);
            cmd.Parameters.Add(new MySqlParameter("@year",y));
            cmd.Parameters.Add(new MySqlParameter("@id",idp));
            cmd.Parameters.Add(new MySqlParameter("@prio", priorite));
            MySqlDataReader DR = cmd.ExecuteReader();
            string[][] res = new string[12][];
            while (DR.Read())
            {
                res[Convert.ToInt32(DR[0].ToString()) - 1] = new string[] { DR[0].ToString(), DR[1].ToString() };


            }
            for (int i = 0; i < res.Length; i++)
            {
                if (res[i] == null)
                { res[i] = new string[] { (i+1).ToString(), "0"}; }
            }
            connection.Close();
            return new JsonResult { Data = res, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult GetDataPourcentage(int y, int idp, int priorite)
        {

           
            connection.Open();
            String Query = "SELECT month(last_updated), TIMESTAMPDIFF(MINUTE,`date_submitted`,`last_updated`) as time FROM `mantis_bug_table`, `mantis_project_hierarchy_table` WHERE year(`last_updated`) = @year and `status` = 90  and `project_id`=`child_id` and priority = @prio and `parent_id` = @id group by month(last_updated)";
            MySqlCommand cmd = new MySqlCommand(Query, connection);
            cmd.Parameters.Add(new MySqlParameter("@year", y));
            cmd.Parameters.Add(new MySqlParameter("@id", idp));
            cmd.Parameters.Add(new MySqlParameter("@prio", priorite));
            MySqlDataReader DR = cmd.ExecuteReader();
            string[][] res = new string[12][];
            while (DR.Read())
            {
                res[Convert.ToInt32(DR[0].ToString()) - 1] = new string[] { DR[0].ToString(), DR[1].ToString() };


            }
            for (int i = 0; i < res.Length; i++)
            {
                if (res[i] == null)
                { res[i] = new string[] { (i + 1).ToString(), "0" }; }
            }
            connection.Close();
            return new JsonResult { Data = res, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
  
    }
}