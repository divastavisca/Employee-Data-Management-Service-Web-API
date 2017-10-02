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
        public bool Add(string firstName, string middleName, string lastName, string dateOfBirth, string emailId, string departmentId)
        {
            throw new NotImplementedException();
        }
        public List<Employee> GetAll()
        {
            throw new NotImplementedException();
        }
        public Employee Get(string id)
        {
            throw new NotImplementedException();
        }
        public bool Update(string id, Name name, string emailId, string departmentId)
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

        private Employee getEmployee(string id, string firstName, string middleName, string lastName, DateTime dateOfBirth, string emailId, string departmentId)
        {
            return
                (
                    new Employee
                    (
                        id,
                        new Name
                        (
                            firstName,
                            middleName,
                            lastName
                        ),
                        dateOfBirth,
                        emailId,
                        departmentId
                    )
                );
        }

        private void fillCache()
        {
            if (_cache.Count > 0)
            {
                _cache.Clear();
            }

        }

        public DatabaseManager()
        {
            _cache = new Dictionary<string, Employee>();
            fillCache();
        }
    }
}