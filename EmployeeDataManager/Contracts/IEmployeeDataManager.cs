using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeDataManager.Models;

namespace EmployeeDataManager.Contracts
{
    interface IEmployeeDataManager
    {
        bool Add(string firstName, string middleName, string lastName, string dateOfBirth, string emailId, string departmentId);
        List<Employee> GetAll();
        Employee Get(string id);
        bool Update(string id, Name name, string emailId, string departmentId);
        bool DeleteAll();
        bool Delete(string id);
    }
}
