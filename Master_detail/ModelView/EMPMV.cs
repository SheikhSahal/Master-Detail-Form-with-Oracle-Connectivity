using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Master_detail.Models;

namespace Master_detail.ModelView
{
    public class EMPMV
    {

        string oradb = "Data Source=dbtest;User ID=psl;Password=psl;";

        public void AddNewDEPT(Dept dept)
        {
            OracleConnection con = new OracleConnection(oradb);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "insert into dept values(:deptno,:dname,:loc)";
            cmd.Connection = con;
            con.Open();

            cmd.Parameters.Add(new OracleParameter("deptno", dept.Deptid));
            cmd.Parameters.Add(new OracleParameter("dname", dept.Dname));
            cmd.Parameters.Add(new OracleParameter("loc", dept.Location));

            cmd.ExecuteNonQuery();
        }

        public void AddNewEmployee(EMP emp)
        {
            OracleConnection con = new OracleConnection(oradb);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "insert into emp values(:empno,:ename,:job,:mgr,:hiredate,:sal,:comm,:deptno)";
            cmd.Connection = con;
            con.Open();

            cmd.Parameters.Add(new OracleParameter("empno", emp.EMPNO));
            cmd.Parameters.Add(new OracleParameter("ename", emp.ENAME));
            cmd.Parameters.Add(new OracleParameter("job", emp.JOB));
            cmd.Parameters.Add(new OracleParameter("mgr", emp.MGR));
            cmd.Parameters.Add(new OracleParameter("hiredate", emp.HIREDATE));
            cmd.Parameters.Add(new OracleParameter("sal", emp.SAL));
            cmd.Parameters.Add(new OracleParameter("comm", emp.COMM));
            cmd.Parameters.Add(new OracleParameter("deptno", emp.DEPTNO));


            cmd.ExecuteNonQuery();
        }

        public EMP AutoGenerate()
        {
            EMP employee = new EMP();
            OracleConnection con = new OracleConnection(oradb);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "Select nvl(max(e.empno),0)+1 As empno , nvl(max(d.deptno),0)+10 As deptno From emp e , dept d";
            cmd.Connection = con;
            con.Open();


            OracleDataReader reader = cmd.ExecuteReader();

            reader.Read();

            employee.EMPNO = Convert.ToInt16(reader["empno"]);
            employee.Deptid = Convert.ToInt16(reader["deptno"]);



            reader.Close();
            return employee;
        }

        public List<EMP> GetDept()
        {
            List<EMP> DBase = new List<EMP>();
            OracleConnection con = new OracleConnection(oradb);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "Select * From dept";
            cmd.Connection = con;
            con.Open();

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                EMP emp = new EMP();

                emp.Deptid = Convert.ToInt16(reader["DEPTNO"]);
                emp.Dname = reader["DNAME"].ToString();
                emp.Location = reader["LOC"].ToString();

                DBase.Add(emp);
            }

            return DBase;
        }

        public List<EMP> Details(int id)
        {
            List<EMP> emp = new List<EMP>();

            EMP dept = new EMP();

            OracleConnection con = new OracleConnection(oradb);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "Select e.*,d.dname,d.loc From emp e,dept d Where d.deptno = e.deptno And e.deptno = :deptno";
            cmd.Connection = con;
            con.Open();

            cmd.Parameters.Add(new OracleParameter("deptno", id));

            OracleDataReader reader = cmd.ExecuteReader();
            

            while (reader.Read())
            {
                EMP employee = new EMP();

                //Detail Table
                if (reader["EMPNO"] != DBNull.Value)
                {
                    employee.EMPNO = Convert.ToInt16(reader["EMPNO"]);
                }

                if (reader["ENAME"] != DBNull.Value)
                {
                    employee.ENAME = reader["ENAME"].ToString();
                }

                if (reader["JOB"] != DBNull.Value)
                {
                    employee.JOB = reader["JOB"].ToString();
                }
                
                if (reader["MGR"] != DBNull.Value)
                {
                    employee.MGR = Convert.ToInt16(reader["MGR"]);
                }

                if (reader["HIREDATE"] != DBNull.Value)
                {
                    employee.HIREDATE = Convert.ToDateTime(reader["HIREDATE"]);
                }
                
                if (reader["SAL"] != DBNull.Value)
                {
                    employee.SAL = Convert.ToInt16(reader["SAL"]);
                }
                

                if (reader["COMM"] != DBNull.Value)
                {
                    employee.COMM = Convert.ToInt16(reader["COMM"]);
                }

                if (reader["DEPTNO"] != DBNull.Value)
                {
                    employee.DEPTNO = Convert.ToInt16(reader["DEPTNO"]);
                }
                

                emp.Add(employee);
            }
            return emp;

        }

        public EMP DetailMaster(int id)
        {
            EMP employee = new EMP();
            OracleConnection con = new OracleConnection(oradb);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "Select * From dept Where deptno = :deptno";
            cmd.Connection = con;
            con.Open();

            cmd.Parameters.Add(new OracleParameter("deptno", id));
            OracleDataReader reader = cmd.ExecuteReader();

            reader.Read();

            if (reader["DEPTNO"] != DBNull.Value)
            {
                employee.Deptid = Convert.ToInt16(reader["DEPTNO"]);
            }
            
            if (reader["DNAME"] != DBNull.Value)
            {
                employee.Dname = reader["DNAME"].ToString();
            }

            if (reader["LOC"] != DBNull.Value)
            {
                employee.Location = reader["LOC"].ToString();
            }
            

            return employee;
        }


        public void updateemp(EMP emp)
        {

            OracleConnection con = new OracleConnection(oradb);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "update emp set ename = :ename,job = :job , mgr = :mgr, hiredate= :hiredate,sal = :sal,comm = :comm,deptno = :deptno  where empno = :empno";
            cmd.Connection = con;
            con.Open();

            cmd.Parameters.Add(new OracleParameter("ename", emp.ENAME));
            cmd.Parameters.Add(new OracleParameter("job", emp.JOB));
            cmd.Parameters.Add(new OracleParameter("mgr", emp.MGR));
            cmd.Parameters.Add(new OracleParameter("hiredate", emp.HIREDATE));
            cmd.Parameters.Add(new OracleParameter("sal", emp.SAL));
            cmd.Parameters.Add(new OracleParameter("comm", emp.COMM));
            cmd.Parameters.Add(new OracleParameter("deptno", emp.DEPTNO));
            cmd.Parameters.Add(new OracleParameter("empno", emp.EMPNO));
            cmd.ExecuteNonQuery();
        }

        public void updatedept(Dept emp)
        {

            OracleConnection con = new OracleConnection(oradb);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "Update dept Set DNAME = :dname , LOC = :loc Where deptno = :deptno";
            cmd.Connection = con;
            con.Open();

            cmd.Parameters.Add(new OracleParameter("dname", emp.Dname));
            cmd.Parameters.Add(new OracleParameter("loc", emp.Location));
            cmd.Parameters.Add(new OracleParameter("deptno", emp.Deptid));
            
            cmd.ExecuteNonQuery();
        }

        public void DeleteEMP(int id)
        {

            OracleConnection con = new OracleConnection(oradb);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "Delete From emp Where empno = :empno";
            cmd.Connection = con;
            con.Open();

            cmd.Parameters.Add(new OracleParameter("empno", id));
            

            cmd.ExecuteNonQuery();
        }
    }
}