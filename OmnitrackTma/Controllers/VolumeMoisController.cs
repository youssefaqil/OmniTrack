using OmnitrackTma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace OmnitrackTma.Controllers
{

    public class VolumeMoisController : Controller
    {
        OmniTrackTmaContext db = new OmniTrackTmaContext();
        // GET: VolumeMois

        public ActionResult Index(mantis_commentaire_table cmt)
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

            var year = (from mt in db.mantis_bug_table
                        from mht in db.mantis_project_hierarchy_table
                        where
                          mt.project_id == mht.child_id
                        select new
                        {
                            a = mt.last_updated.Year
                        }).Distinct();
            ViewBag.year = year;
            return View();
          
        }
        public JsonResult GetYearbyProject(int p)
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
  
        public JsonResult GetMonthbyYear(int Year,int idp)
        {
            var bugTable = (
                from b in db.mantis_bug_table
                from mth in db.mantis_project_hierarchy_table
                where b.last_updated.Year == Year &&
                mth.parent_id == idp &&
                b.project_id == mth.child_id 
                                
                select (new { b.last_updated.Month })).Distinct();
           
            return new JsonResult { Data = bugTable, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetCountPriorityEvo(int m, int y, int idp)
        {
            
            int mois = m;
           int annee=y;
            var prioritydata = from mt in db.mantis_bug_table
                               from mht in db.mantis_project_hierarchy_table
                               where
                                 mt.category == "evolution" &&
                                 mt.project_id == mht.child_id &&
                                 mht.parent_id == idp &&
                                 mt.last_updated.Month == m &&
                                 mt.last_updated.Year == annee &&
                                 mt.status == 90 &&
                                 mt.project_id == mht.child_id 
                               group mt by new
                               {
                                   mt.category,
                                   
                               } into g
                               select new
                               {
                                   g.Key.category,
                                   //g.Key.parent_id,
                                   //g.Key.child_id,
                                   High = g.Count(p => (
                                   p.priority== 40 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                   Moyenne = g.Count(p => (
                                   p.priority == 30 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                   Basse = g.Count(p => (
                                   p.priority == 20 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                   Undifined = g.Count(p => (
                                   p.priority == 10 ? (System.Int64?)1 : (System.Int64?)null) != null)
                               };
         
            
            
            return new JsonResult { Data = prioritydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetCountPriorityAnom(int m, int y,int idp)
        {

            int mois = m;
            int annee = y;
            var prioritydata = from mt in db.mantis_bug_table
                               from mht in db.mantis_project_hierarchy_table
                               where
                                 mt.category == "anomalie" &&
                                 mt.project_id == mht.child_id &&
                                  
                                 mt.last_updated.Month == m &&
                                  mht.parent_id == idp &&
                                 mt.last_updated.Year == annee &&
                                 mt.status == 90 
                                
                               group mt by new
                               {
                                   mt.category,

                               } into g
                               select new
                               {
                                   g.Key.category,
                                   //g.Key.parent_id,
                                   //g.Key.child_id,
                                   High = g.Count(p => (
                                   p.priority == 40 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                   Moyenne = g.Count(p => (
                                   p.priority == 30 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                   Basse = g.Count(p => (
                                   p.priority == 20 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                   Undifined = g.Count(p => (
                                   p.priority == 10 ? (System.Int64?)1 : (System.Int64?)null) != null)
                               };



            return new JsonResult { Data = prioritydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
         public JsonResult GetCountPriorityAssist(int m,int y,int idp)
        {
            
            int mois = m;
           int annee=y;
            var prioritydata = from mt in db.mantis_bug_table
                               from mht in db.mantis_project_hierarchy_table
                               where
                               
                                  mt.category == "assistance util" &&
                                 mt.project_id == mht.child_id &&
                                
                                 mt.last_updated.Month == m &&
                                  mht.parent_id == idp &&
                                 mt.last_updated.Year == annee &&
                                 mt.status == 90 &&
                                  mt.project_id == mht.child_id 
                               group mt by new
                               {
                                   mt.category,
                                   
                               } into g
                               select new
                               {
                                   g.Key.category,
                                   //g.Key.parent_id,
                                   //g.Key.child_id,
                                   High = g.Count(p => (
                                   p.priority== 40 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                   Moyenne = g.Count(p => (
                                   p.priority == 30 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                   Basse = g.Count(p => (
                                   p.priority == 20 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                   Undifined = g.Count(p => (
                                   p.priority == 10 ? (System.Int64?)1 : (System.Int64?)null) != null)
                               };
         
            
            
            return new JsonResult { Data = prioritydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        
          public JsonResult GetCountPriorityEnStockEvo(int m, int y,int idp)
         {

             int mois = m;
             int annee = y;
             var prioritydata = from mt in db.mantis_bug_table
                                from mht in db.mantis_project_hierarchy_table
                                where
                                  (mt.category == "evolution") &&
                                  mt.project_id == mht.child_id &&
                                    
                                  mt.last_updated.Month == m &&
                                  mt.last_updated.Year == annee &&
                                   mht.parent_id == idp &&
                                  mt.status != 90 && mt.status != 10 
                                  
                                group mt by new
                                {
                                    mt.category,

                                } into g
                                select new
                                {
                                    g.Key.category,

                                    High = g.Count(p => (p.priority == 40 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                    Moyenne = g.Count(p => (p.priority == 30 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                    Basse = g.Count(p => (p.priority == 20 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                    Undifined = g.Count(p => (p.priority == 10 ? (System.Int64?)1 : (System.Int64?)null) != null)
                                };
       



             return new JsonResult { Data = prioritydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
         }
          public JsonResult GetCountPriorityEnStockAnom(int m, int y,int idp)
          {

              int mois = m;
              int annee = y;
              var prioritydata = from mt in db.mantis_bug_table
                                 from mht in db.mantis_project_hierarchy_table
                                 where
                                   (mt.category == "anomalie") &&
                                   mt.project_id == mht.child_id &&
                                     mht.parent_id == idp &&
                                   mt.last_updated.Month == m &&
                                   mt.last_updated.Year == annee &&
                                   mt.status != 90 && mt.status != 10
                                 group mt by new
                                 {
                                     mt.category,

                                 } into g
                                 select new
                                 {
                                     g.Key.category,

                                     High = g.Count(p => (p.priority == 40 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                     Moyenne = g.Count(p => (p.priority == 30 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                     Basse = g.Count(p => (p.priority == 20 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                     Undifined = g.Count(p => (p.priority == 10 ? (System.Int64?)1 : (System.Int64?)null) != null)
                                 };
             



              return new JsonResult { Data = prioritydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
          }
          public JsonResult GetCountPriorityEnStockAssist(int m, int y,int idp)
          {

              int mois = m;
              int annee = y;
              var prioritydata = from mt in db.mantis_bug_table
                                 from mht in db.mantis_project_hierarchy_table
                                 where
                                   (mt.category == "assistance util") &&
                                   mt.project_id == mht.child_id &&
                                      mht.parent_id == idp &&
                                   mt.last_updated.Month == m &&
                                   mt.last_updated.Year == annee &&
                                   mt.status != 90 && mt.status != 10
                                 group mt by new
                                 {
                                     mt.category,

                                 } into g
                                 select new
                                 {
                                     g.Key.category,

                                     High = g.Count(p => (p.priority == 40 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                     Moyenne = g.Count(p => (p.priority == 30 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                     Basse = g.Count(p => (p.priority == 20 ? (System.Int64?)1 : (System.Int64?)null) != null),
                                     Undifined = g.Count(p => (p.priority == 10 ? (System.Int64?)1 : (System.Int64?)null) != null)
                                 };
             

              return new JsonResult { Data = prioritydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
          }
          public JsonResult GetCommentaire()
          {
              var Commentaire = from cmt in db.mantis_commentaire_table
                                where cmt.id == 1
                                select new
                                {
                                    cmt.commentaire
                                };


              return new JsonResult { Data = Commentaire, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
          }





          [HttpPost]
          public ActionResult UpdateComment(string comment)
          {
              using(OmniTrackTmaContext dba  = new OmniTrackTmaContext())
              {

                  mantis_commentaire_table nComment = dba.mantis_commentaire_table.Single(u => u.id == 1);
              nComment.commentaire = comment;
              dba.SaveChanges();
              return Json(true);
              }
              
          }




            
       
    }
}
