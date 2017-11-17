using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataManager.Models;
using EmployeeDataManager.Contracts;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

namespace EmployeeDataManager.DataManagers
{
    public class DatabaseManager : IEmployeeDataManager
    {
        private Dictionary<string, Employee> _cache;

        public bool Add(EmployeeData employeeData, out string uri)
        {
            DataTable queryResult = new DataTable();
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbcs"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "Insert into Employee OUTPUT inserted.Id values(@FirstName,@MiddleName,@LastName,@DateOfBirth,@EmailId,@DepartmentId)";
                sqlCommand.Connection = connection;
                sqlCommand.Parameters.AddWithValue("FirstName", employeeData.Name.FirstName);
                sqlCommand.Parameters.AddWithValue("MiddleName", employeeData.Name.MiddleName);
                sqlCommand.Parameters.AddWithValue("LastName", employeeData.Name.LastName);
                sqlCommand.Parameters.AddWithValue("DateOfBirth", employeeData.DateOfBirth);
                sqlCommand.Parameters.AddWithValue("EmailId", employeeData.EmailId);
                sqlCommand.Parameters.AddWithValue("DepartmentId", employeeData.DepartmentId);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                try
                {
                    dataAdapter.Fill(queryResult);
                }
                catch
                {
                    uri = null;
                    return false;
                }
            }
            if(queryResult.Rows.Count!=0)
            {
                uri = queryResult.Rows[0][0].ToString();
                return true;
            }
            else
            {
                uri = null;
                return false;
            }
        }

        public bool GetAll(out List<Employee> employees)
        {
            DataTable queryResult = new DataTable();
            employees = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbcs"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "Select * from Employee";
                sqlCommand.Connection = connection;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                try
                {
                    dataAdapter.Fill(queryResult);
                }
                catch
                {
                    employees = null;
                    return false;
                }
            }
            if(queryResult.Rows.Count>0)
            {
                foreach(DataRow row  in queryResult.Rows)
                {
                    EmployeeData empData = new EmployeeData
                        (
                            new Name
                            (
                                row["FirstName"].ToString(),
                                row["MiddleName"].ToString() != "" ? row["MiddleName"].ToString() : null,
                                row["LastName"].ToString()
                            ),
                            DateTime.Parse(row["DateOfBirth"].ToString()),
                            row["EmailId"].ToString(),
                            row["DepartmentId"].ToString()
                        );
                    employees.Add(getEmployee(row["Id"].ToString(), empData));
                }
                return true;
            }
            else
            {
                employees = null;
                return false;
            }
        }

        public bool Get(string id, out Employee employee)
        {
            if(_cache.ContainsKey(id))
            {
                employee = _cache[id];
                return true;
            }
            else
            {
                DataTable queryResult = new DataTable();
                using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbcs"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "Select * from Employee where Id=@Id";
                    sqlCommand.Parameters.AddWithValue("Id", id);
                    sqlCommand.Connection = connection;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                    try
                    {
                        dataAdapter.Fill(queryResult);
                    }
                    catch
                    {
                        employee = null;
                        return false;
                    }
                }
                if(queryResult.Rows.Count>0)
                {
                    DataRow row = queryResult.Rows[0];
                    EmployeeData empData = new EmployeeData
                        (
                            new Name
                            (
                                row["FirstName"].ToString(),
                                row["MiddleName"] != null ? row["MiddleName"].ToString() : null,
                                row["LastName"].ToString()
                            ),
                            DateTime.Parse(row["DateOfBirth"].ToString()),
                            row["EmailId"].ToString(),
                            row["DepartmentId"].ToString()
                        );
                    employee = getEmployee(id, empData);
                    _cache.Add(id, employee);
                    return true;
                }
                else
                {
                    employee = null;
                    return false;
                }
            }
        }

        public bool Update(string id, EmployeeData employeeData)
        {
            DataTable queryResult = new DataTable();
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbcs"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "Insert into Employee(FirstName,MiddleName,LastName,DateOfBirth,EmailId,DepartmentId) VALUES(@FirstName,@MiddleName,@LastName,@DateOfBirth,@EmailId,@DepartmentId)";
                sqlCommand.Parameters.AddWithValue("FirstName", employeeData.Name.FirstName);
                sqlCommand.Parameters.AddWithValue("MiddleName", employeeData.Name.MiddleName);
                sqlCommand.Parameters.AddWithValue("LastName", employeeData.Name.LastName);
                sqlCommand.Parameters.AddWithValue("DateOfBirth", employeeData.DateOfBirth);
                sqlCommand.Parameters.AddWithValue("EmailId", employeeData.EmailId);
                sqlCommand.Parameters.AddWithValue("DepartmentId", employeeData.DepartmentId);
                sqlCommand.Connection = connection;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                try
                {
                    dataAdapter.Fill(queryResult);
                }
                catch
                {
                    return false;
                }
                if(_cache.ContainsKey(id))
                {
                    _cache[id] = getEmployee(id, employeeData);
                }
                return true;
            }
        }

        public bool DeleteAll()
        {
            DataTable queryResult = new DataTable();
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbcs"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "Delete from Employee";
                sqlCommand.Connection = connection;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                try
                {
                    dataAdapter.Fill(queryResult);
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        private Employee getEmployee(string id, EmployeeData employeeData)
        {
            return
                (
                    new Employee
                    (
                        id,
                        new Name
                        (
                            employeeData.Name.FirstName,
                            employeeData.Name.MiddleName,
                            employeeData.Name.LastName
                        ),
                        employeeData.DateOfBirth,
                        employeeData.EmailId,
                        employeeData.DepartmentId
                    )
                );
        }

        private void addToCache(string id, Employee employee)
        {
            _cache.Add(id, employee);
        }

        public DatabaseManager()
        {
            _cache = new Dictionary<string, Employee>();
        }
    }
}