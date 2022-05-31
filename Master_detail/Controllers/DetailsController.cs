using Master_detail.Models;
using Master_detail.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Master_detail.Controllers
{
    public class DetailsController : Controller
    {
        

        // GET: Details
        public ActionResult Index(int id)
        {
            EMPMV db = new EMPMV();
            EMP dept = db.DetailMaster(id);
            List<EMP> emp = db.Details(id);

            MergeModelDetails mm = new MergeModelDetails();
            mm.EMP = dept;
            mm.EMPlist = emp;

            return View(mm);
        }
    }
}