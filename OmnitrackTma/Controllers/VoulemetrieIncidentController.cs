using OmnitrackTma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.Data.Entity.Core.Objects.DataClasses;


namespace OmnitrackTma.Controllers
{
    public class VoulemetrieIncidentController : Controller
    {
        // GET: VoulemetrieIncident
        OmniTrackTmaContext db = new OmniTrackTmaContext();


        string[] mois = { "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Septembre", "Octobre", "novembre", "décembre" };




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
        public JsonResult GetYearbyProject(int p)
        {


            var year = (from mt in db.mantis_bug_table
                        from mht in db.mantis_project_hierarchy_table
                        where
                          mht.parent_id == p  && mt.project_id == mht.child_id
                        select new
                        {
                            mt.last_updated.Year
                        }).Distinct();

            return new JsonResult { Data = year, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetCommentaire()
        {
            var Commentaire = from cmt in db.mantis_commentaire_table
                              where cmt.id == 2
                              select new
                              {
                                  cmt.commentaire
                              };


            return new JsonResult { Data = Commentaire, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult UpdateComment(string comment)
        {
            using (OmniTrackTmaContext dba = new OmniTrackTmaContext())
            {

                mantis_commentaire_table nComment = dba.mantis_commentaire_table.Single(u => u.id == 2);
                nComment.commentaire = comment;
                dba.SaveChanges();
                return Json(true);
            }

        }

   //_____________________________________Les Mois par anné ___________________________________________________________
        public JsonResult GetCountPriorityfev(int y,int idp)
        {
            var entre = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                         
                             mt.last_updated.Year == y &&
                              mt.project_id == mth.child_id &&
                             mth.parent_id==idp 
                             && mt.last_updated.Month == 2
                          select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&
                             mth.parent_id==idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 2
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority =( from mt in db.mantis_bug_table
                                from mth in db.mantis_project_hierarchy_table
                                where

                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 2 &&
                               mt.project_id == mth.child_id &&
                             mth.parent_id==idp
                                select new
                                {
                                   m = mt.last_updated.Month,
                                    entre,
                                    sortie,
                                    stock
                                }).Distinct();           

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetCountPriorityJanv(int y,int idp)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&
                             mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp 
                            && mt.last_updated.Month == 1
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&
                             mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 1
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                 mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 1
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetCountPriorityMars(int y,int idp)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&
                         
                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp 
                            && mt.last_updated.Month == 3
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&
                       
                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 3
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 3
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult GetCountPriorityAvril(int y,int idp)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&
                         
                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp 
                            && mt.last_updated.Month == 4
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&
                          
                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 4
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 4
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetCountPriorityMai(int y,int idp)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&
                          
                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp 
                            && mt.last_updated.Month == 5
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                             mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                        
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 5
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 5
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult GetCountPriorityJuin(int y, int idp)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&
                      
                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp 
                            && mt.last_updated.Month == 6
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&
         
                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 6
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 6
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult GetCountPriorityJuillet(int y,int idp)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&
                      
                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp 
                            && mt.last_updated.Month == 7
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&
       
                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 7
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 7
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetCountPriorityAout(int y,int idp)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&
                           
                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp 
                            && mt.last_updated.Month == 8
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&
                          
                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 8
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 8
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetCountPrioritySeptebmre(int y,int idp)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&
                          
                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp 
                            && mt.last_updated.Month == 9
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&
                         
                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 9
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 9
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult GetCountPriorityOctobre(int y, int idp)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&
                         
                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp 
                            && mt.last_updated.Month == 10
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&
                          
                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 10
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                              mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 10
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult GetCountPriorityNovember(int y,int idp)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&
                        
                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp 
                            && mt.last_updated.Month == 11
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&
                         
                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 11
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 11
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetCountPriorityDecembre(int y,int idp)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&
                          
                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y
                            && mt.last_updated.Month == 12
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&
                          
                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y &&
                             mth.parent_id == idp 
                             && mt.last_updated.Month == 12
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 12
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        //________________________________________________ les Mois par Priorité________________________________________________

        public JsonResult GetCountPriorityfevP(int y, int idp, int priorite)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                        
                            mt.last_updated.Year == y &&
                            mt.project_id == mth.child_id &&
                            mth.parent_id == idp
                            && mt.last_updated.Month == 2
                            && mt.priority==priorite
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&
                             mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 2
                             && mt.priority == priorite
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where

                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 2 &&
                                mt.project_id == mth.child_id &&
                              mth.parent_id == idp
                              && mt.priority == priorite
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetCountPriorityJanvP(int y, int idp,int priorite)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&
                             mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp
                            && mt.last_updated.Month == 1
                            && mt.priority == priorite
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&
                             mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 1
                             && mt.priority == priorite
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                 mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 1
                               && mt.priority==priorite
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetCountPriorityMarsP(int y, int idp,int priorite)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp
                            && mt.last_updated.Month == 3
                            && mt.priority == priorite
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 3
                             && mt.priority == priorite
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 3
                              && mt.priority == priorite
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult GetCountPriorityAvrilP(int y, int idp,int priorite)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp
                            && mt.last_updated.Month == 4
                            && mt.priority == priorite
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 4
                             && mt.priority == priorite
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 4
                              && mt.priority == priorite
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetCountPriorityMaiP(int y, int idp,int priorite)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp
                            && mt.last_updated.Month == 5
                            && mt.priority == priorite
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                             mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&

                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 5
                             && mt.priority == priorite
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 5
                              && mt.priority == priorite
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult GetCountPriorityJuinP(int y, int idp,int priorite)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp
                            && mt.last_updated.Month == 6
                            && mt.priority == priorite
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 6
                             && mt.priority == priorite
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 6
                              && mt.priority == priorite
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult GetCountPriorityJuilletP(int y, int idp,int priorite)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp
                            && mt.last_updated.Month == 7
                            && mt.priority == priorite
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 7
                             && mt.priority == priorite
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 7
                              && mt.priority == priorite
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetCountPriorityAoutP(int y, int idp, int priorite)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp
                            && mt.last_updated.Month == 8
                            && mt.priority == priorite
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 8
                             && mt.priority == priorite
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 8
                              && mt.priority == priorite
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetCountPrioritySeptebmreP(int y, int idp,int priorite)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp
                            && mt.last_updated.Month == 9
                            && mt.priority == priorite
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 9
                             && mt.priority == priorite
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 9
                              && mt.priority == priorite
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult GetCountPriorityOctobreP(int y, int idp,int priorite)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp
                            && mt.last_updated.Month == 10
                            && mt.priority == priorite
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 10
                             && mt.priority == priorite
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                              mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 10
                              && mt.priority == priorite
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult GetCountPriorityNovemberP(int y, int idp,int priorite)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y &&
                            mth.parent_id == idp
                            && mt.last_updated.Month == 11
                            && mt.priority == priorite
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y
                             && mt.last_updated.Month == 11
                             && mt.priority == priorite
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 11
                              && mt.priority == priorite
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetCountPriorityDecembreP(int y, int idp,int priorite)
        {
            var entre = (from mt in db.mantis_bug_table
                         from mth in db.mantis_project_hierarchy_table
                         where
                           (mt.category == "evolution" ||
                           mt.category == "anomalie" ||
                           mt.category == "assistance util") &&
                          mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                            mt.last_updated.Year == y
                            && mt.last_updated.Month == 12
                            && mt.priority == priorite
                         select mt.id).Count();
            var sortie = (from mt in db.mantis_bug_table
                          from mth in db.mantis_project_hierarchy_table
                          where
                            (mt.category == "evolution" ||
                            mt.category == "anomalie" ||
                            mt.category == "assistance util") &&
                           mt.project_id == mth.child_id &&

                                  mth.parent_id == idp &&
                               mt.status == 90 &&
                             mt.last_updated.Year == y &&
                             mth.parent_id == idp
                             && mt.last_updated.Month == 12
                             && mt.priority == priorite
                          select mt.id).Count();
            var stock = (entre - sortie);

            var countpriority = (from mt in db.mantis_bug_table
                                 from mth in db.mantis_project_hierarchy_table
                                 where
                                  mt.project_id == mth.child_id &&
                                  mth.parent_id == idp &&
                              mt.last_updated.Year == y
                              && mt.last_updated.Month == 12
                              && mt.priority == priorite
                                 select new
                                 {
                                     m = mt.last_updated.Month,
                                     entre,
                                     sortie,
                                     stock
                                 }).Distinct();

            return new JsonResult { Data = countpriority, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
       
    }
}
