using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models.DbModels;

namespace WebApi.Controllers
{
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                            select DepartmentId, DepartmentName from
                            dbo.Departments
                            ";
            DataTable dt = new DataTable();
            using( var connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))

                using(var cmd = new SqlCommand(query, connection))

                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }

            return Request.CreateResponse(HttpStatusCode.OK, dt);
        }

        public string Post(Department department)
        {
            try
            {
                string query = @"
                            insert into dbo.Departments 
                            values ('"+department.DepartmentName+ @"')
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

                return "Added Successfully!";
            }
            catch (Exception)
            {
                return "Failed to add!!"; 
            }
        }

        public string Put(Department department)
        {
            try
            {
                string query = @"
                            update dbo.Departments set DepartmentName = 
                            '" + department.DepartmentName + @"'
                             where DepartmentId = '" + department.DepartmentId + @"'
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
            catch (Exception)
            {
                return "Failed to Update!!";
            }
        }


        public string Delete(int id)
        {
            try
            {
                string query = @"
                            delete from dbo.Departments 
                            where DepartmentId = '" + id + @"'
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

    }
}
