using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataManager.Models;
using EmployeeDataManager.Contracts;

namespace EmployeeDataManager.DataManagers
{
    public class DatabaseManager : IEmployeeDataManager
    {
        private Dictionary<string, Employee> _cache;
        public bool Add(EmployeeData employeeData,out string uri)
        {
            throw new NotImplementedException();
        }
        public bool GetAll(out List<Employee> employees)
        {
            throw new NotImplementedException();
        }
        public bool Get(string id,out Employee employee)
        {
            throw new NotImplementedException();
        }
        public bool Update(string id, EmployeeData employeeData)
        {
            throw new NotImplementedException();
        }
        public bool DeleteAll()
        {
            throw new NotImplementedException();
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

        private void addToCache(string id,Employee employee)
        {
            _cache.Add(id,employee);
        }

        public DatabaseManager()
        {
            _cache = new Dictionary<string, Employee>();
        }
    }
}