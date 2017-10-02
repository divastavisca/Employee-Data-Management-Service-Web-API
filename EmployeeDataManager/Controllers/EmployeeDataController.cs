using EmployeeDataManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataManager.DataManagers;
using EmployeeDataManager.Contracts;

namespace EmployeeDataManager.Controllers
{
    public class EmployeeDataController : ApiController
    {
        private static IEmployeeDataManager _dataManager;
        public IHttpActionResult GetAll()
        {
            List<Employee> list;
            if (_dataManager.GetAll(out list))
            {
                return Ok(list);
            }
            else return NotFound();
        }

        public IHttpActionResult Get(string id)
        {
            Employee employee;
            if (_dataManager.Get(id, out employee))
            {
                return Ok(employee);
            }
            else return NotFound();
        }

        public IHttpActionResult Post(EmployeeData employeeData)
        {
            string uri;
            if (_dataManager.Add(employeeData, out uri))
                return Created<EmployeeData>(Request.RequestUri.ToString()+@"/" + uri, employeeData);
            return InternalServerError();
        }

        public EmployeeDataController()
        {
            if(_dataManager==null)
                _dataManager = new RandomDataManager();
        }
    }
}
