using OmniTrackTma_TDB_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OmniTrackTma_TDB_.Controllers
{
    public class HomeController : Controller
    {
        OmnitracktmaContext db = new OmnitracktmaContext();
        
        public ActionResult Index()
        {

           // List<mantis_bug_table> listbug = db.mantis_bug_table.Where(m => (m.category == "evolution" || m.category == "anomalie") && m.last_updated.Year == 2010 && m.last_updated.Month ==2).ToList();
            //var list = from bt in db.mantis_bug_table
            //           where ((bt.category == "evolution" || bt.category == "anomalie") && bt.last_updated.Year == 2010 && bt.last_updated.Month == 2)
            //           select bt;
            var list = from bt in db.mantis_bug_table
                       where ((bt.category == "evolution" || bt.category == "anomalie") && bt.last_updated.Year == 2010 && bt.last_updated.Month == 2)
                       select bt;
            ViewBag.test = list.Count();
              
            return View(list);

         }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public IView lst { get; set; }
    }
}