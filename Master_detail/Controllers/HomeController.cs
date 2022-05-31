using Master_detail.Models;
using Master_detail.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace Master_detail.Controllers
{

    public class HomeController : Controller

    {
        private EMPMV db = new EMPMV();
        string oradb = "Data Source=dbtest;User ID=psl;Password=psl;";

        // GET: Home
        public ActionResult Index()
        {
            EMP deptno = db.AutoGenerate();

            //////////////////Dept Number Drop Down//////////////////
            var list = new List<DeptDropDown>();

            OracleConnection con = new OracleConnection(oradb);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "Select d.deptno,d.dname From dept d";
            cmd.Connection = con;
            con.Open();

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new DeptDropDown
                {
                    Deptid = reader.GetInt16(0),
                    Dname = reader.GetString(1)
                });
            }
            var model = new DeptModelList();
            ViewBag.drop = new SelectList(list, "deptid", "dname");
            //////////////////Dept Number Drop Down//////////////////

            return View(deptno);
        }

        

        [HttpPost]
        public JsonResult Save(Dept d)
        {
            bool status = false;

            db.AddNewDEPT(d);

            foreach (var item in d.EMP)
            {
                db.AddNewEmployee(item);
                status = true;
            }


            return new JsonResult { Data = new { status = status,newurl= Url.Action("Index", "DeptDetails") } };
        }
    }
}