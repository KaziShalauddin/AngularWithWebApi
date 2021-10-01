using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi.Models.DbModels;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            //string query = @"
            //                select EmployeeId, EmployeeName, DepartmentId,
            //                convert(varchar(10), JoiningDate, 120) as JoiningDate,
            //                PhotoPath
            //                from dbo.Employees
            //                ";
            string query = @"
                            SELECT emp.EmployeeId AS EmployeeId, 
                                       emp.EmployeeName AS EmployeeName,
                                       convert(varchar(10), emp.JoiningDate, 120) AS JoiningDate,
                                       emp.PhotoPath AS PhotoPath,
                                       dep.DepartmentId AS DepartmentId,
                                       dep.DepartmentName AS DepartmentName                                       
                                FROM  dbo.Employees emp
                                JOIN  dbo.Departments dep ON emp.DepartmentId = dep.DepartmentId 
                               ";

            DataTable dt = new DataTable();
            using (var connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))

            using (var cmd = new SqlCommand(query, connection))

            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(dt);
            }

            return Request.CreateResponse(HttpStatusCode.OK, dt);
        }

        public string Post(Employee employee)
        {
            try
            {
                string query = @"
                            insert into dbo.Employees values 
                            (
                            '" + employee.EmployeeName + @"'
                            ,'" + employee.DepartmentId + @"'
                            ,'" + employee.JoiningDate + @"'
                            ,'" + employee.PhotoPath + @"'
                            )";
                DataTable dt = new DataTable();
                using (var connection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))

                using (var cmd = new SqlCommand(query, connection))

                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }

                return "Added Successfully!";
            }
            catch (Exception ex)
            {
                return "Failed to add!!";
            }
        }

        public string Put(Employee employee)
        {
            try
            {
                string query = @"
                            update dbo.Employees set 
                            EmployeeName = '" + employee.EmployeeName + @"'
                            ,DepartmentId = '" + employee.DepartmentId + @"'
                            ,JoiningDate = '" + employee.JoiningDate + @"'
                            ,PhotoPath = '" + employee.PhotoPath + @"'
                             where EmployeeId = '" + employee.EmployeeId + @"'
                            ";
                DataTable dt = new DataTable();
                using (var connection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))

                using (var cmd = new SqlCommand(query, connection))

                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }

                return "Updated Successfully!";
            }
            catch (Exception ex)
            {
                return "Failed to Update!!";
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = @"
                            delete from dbo.Employees 
                            where EmployeeId = '" + id + @"'
                            ";
                DataTable dt = new DataTable();
                using (var connection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))

                using (var cmd = new SqlCommand(query, connection))

                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }

                return "Deleted Successfully!";
            }
            catch (Exception)
            {
                return "Failed to delete!!";
            }
        }

        [Route("api/Employee/GetAllDepartments")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartments()
        {
            string query = @"
                            select DepartmentId, DepartmentName from
                            dbo.Departments
                            ";
            DataTable dt = new DataTable();
            using (var connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))

            using (var cmd = new SqlCommand(query, connection))

            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(dt);
            }

            return Request.CreateResponse(HttpStatusCode.OK, dt);
        }

        [Route("api/Employee/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + fileName);

                postedFile.SaveAs(physicalPath);

                return fileName;
            }
            catch (Exception)
            {
                return "NoPhoto.png";
            }
        }
    }
}
