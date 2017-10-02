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
        bool Add(EmployeeData employeeData,out string uri);
        bool GetAll(out List<Employee> employees);
        bool Get(string id,out Employee employee);
        bool Update(string id, EmployeeData newEmployeeData);
        bool DeleteAll();
        bool Delete(string id);
    }
}
