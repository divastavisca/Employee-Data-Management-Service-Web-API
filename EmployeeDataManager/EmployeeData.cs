using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataManager.Models;

namespace EmployeeDataManager
{
    public class EmployeeData
    {
        public Name Name { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string EmailId { get; private set; }
        public string DepartmentId { get; private set; }
        public EmployeeData(Name name,DateTime dateOfBirth,string emailId,string departmentId)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            EmailId = emailId;
            DepartmentId = departmentId;
        }
    }
}