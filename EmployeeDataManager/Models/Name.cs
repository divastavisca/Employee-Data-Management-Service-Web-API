using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EmployeeDataManager.Models
{
    public class Name
    {
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }

        public Name(string firstName,string middleName,string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(FirstName+" ");
            if (MiddleName != null)
                builder.Append(MiddleName+" ");
            builder.Append(LastName);
            return builder.ToString();
        }
    }
}