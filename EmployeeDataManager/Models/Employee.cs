using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDataManager.Models
{
    public class Employee
    {
        public string Id { get; private set; }
        public Name Name { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string EmailId { get; private set; }
        public string DepartmentId { get; private set; }

        public Employee(string id,Name name,DateTime dateOfBirth,string emailId,string departmentId)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            EmailId = emailId;
            DepartmentId = departmentId;
        }
    }
}