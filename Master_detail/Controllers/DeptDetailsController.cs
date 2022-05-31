using Master_detail.Models;
using Master_detail.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Master_detail.Controllers
{
    public class DeptDetailsController : Controller
    {
        // GET: DeptDetails
        public ActionResult Index()
        {
            EMPMV deptView = new EMPMV();
            List<EMP> emp = deptView.GetDept();

            return View(emp);
        }
    }
}