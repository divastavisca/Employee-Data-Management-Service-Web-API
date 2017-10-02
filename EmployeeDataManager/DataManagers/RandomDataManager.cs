using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataManager.Models;
using EmployeeDataManager.Contracts;

namespace EmployeeDataManager.DataManagers
{
    public class RandomDataManager : IEmployeeDataManager
    {
        private List<Employee> _employeeList;

        public RandomDataManager()
        {
            _employeeList = new List<Employee>();
            _employeeList.Add(new Employee("1", new Name("Divas", null, "Agarwal"), DateTime.Parse("08-10-1993"), "divasag93@gmail.com", "21"));
            _employeeList.Add(new Employee("2", new Name("Suraj", null, "Narayan"), DateTime.Parse("10-05-1995"), "divasag93@gmail.com", "21"));
            _employeeList.Add(new Employee("3", new Name("Amit", null, "Prakash"), DateTime.Parse("26-09-1981"), "divasag93@gmail.com", "21"));
        }

        public bool Add(EmployeeData employeeData,out string uri)
        {
            uri = "4";
            int count=_employeeList.Count;
            _employeeList.Add(getEmployee(uri, employeeData));
            if(count<_employeeList.Count)
            {
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
            if(_employeeList.Count>0)
            {
                employees = _employeeList;
                return true;
            }
            else
            {
                employees = _employeeList;
                return false;
            }
        }
        public bool Get(string id,out Employee employee)
        {
            foreach(Employee emp in _employeeList)
            {
                if(emp.Id == id)
                {
                    employee = emp;
                    return true;
                }
            }
            employee = null;
            return false;
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

        public bool Update(string id,EmployeeData newEmployeeData)
        {
            int k = 0;
            foreach(Employee emp in _employeeList)
            {
                if(emp.Id==id)
                {
                    _employeeList[k] = getEmployee(id, newEmployeeData);
                    return true;
                }
                k++;
            }
            return false;
        }

        public bool DeleteAll()
        {
            if(_employeeList.Count>0)
            {
                _employeeList.Clear();
                return true;
            }
            return false;
        }

        public bool Delete(string id)
        {
            foreach (Employee emp in _employeeList)
            {
                if (emp.Id == id)
                {
                    _employeeList.Remove(emp);
                    return true;
                }
            }
            return false;
        }
    }
}