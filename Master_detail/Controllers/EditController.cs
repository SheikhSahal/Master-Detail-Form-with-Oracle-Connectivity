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
    public class EditController : Controller
    {
        private EMPMV db = new EMPMV();
        string oradb = "Data Source=dbtest;User ID=psl;Password=psl;";
        // GET: Update
        public ActionResult Index(int id)
        {
            EMP deptno = db.AutoGenerate();

            ///data fetch
            //EMPMV db = new EMPMV();
            EMP dept = db.DetailMaster(id);
            List<EMP> emp = db.Details(id);

            ViewBag.emp = emp;
            ///data fetch

            MergeModelDetails mm = new MergeModelDetails();
            mm.EMP = dept;
            //mm.EMPlist = emp;
            mm.EMPNO = deptno;

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

            //ViewBag.drop = new SelectList(list, "deptid", "dname");
            //////////////////Dept Number Drop Down//////////////////

            return View(mm);
        }

        [HttpPost]
        public ActionResult Index(Dept d, int deptid)
        {
            bool status = false;

            EMP dept = db.DetailMaster(deptid);

            List<EMP> emp = db.Details(deptid);
            ViewBag.emp = emp;

            if (deptid == dept.Deptid)
            {
                db.updatedept(d);
                status = true;
            }


            foreach (var item in d.EMP)
            {
                if (item.STATE == "A")
                {
                    db.AddNewEmployee(item);
                    status = true;
                }
                else if (item.STATE == "U")
                {
                    db.updateemp(item);
                    status = true;
                }
                else if (item.STATE == "D")
                {
                    db.DeleteEMP(Convert.ToInt16(item.EMPNO));
                    status = true;
                }
            }

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
            //ViewBag.drop = new SelectList(list, "deptid", "dname");
            //////////////////Dept Number Drop Down//////////////////

            return new JsonResult { Data = new { status = status, newurl = Url.Action("Index", "DeptDetails") } };
        }
    }
}
