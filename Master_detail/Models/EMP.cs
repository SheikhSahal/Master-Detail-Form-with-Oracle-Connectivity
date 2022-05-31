using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Master_detail.Models
{
    public class EMP
    {
        public Nullable<int> EMPNO { get; set; }
        public string ENAME { get; set; }
        public string JOB { get; set; }
        public Nullable<int> MGR { get; set; }
        public Nullable<DateTime> HIREDATE { get; set; }
        public Nullable<int> SAL { get; set; }
        public Nullable<int> COMM { get; set; }
        public Nullable<int> DEPTNO { get; set; }
        public string STATE { get; set; }


        public Nullable<int> Deptid { get; set; }
        public  string Dname { get; set; }
        public string Location { get; set; }
    }
}