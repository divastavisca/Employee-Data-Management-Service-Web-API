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

        public IHttpActionResult Get([FromUri]string id)
        {
            Employee employee;
            if (_dataManager.Get(id, out employee))
            {
                return Ok(employee);
            }
            else return NotFound();
        }

        public IHttpActionResult Post([FromBody]EmployeeData employeeData)
        {
            string uri;
            if (_dataManager.Add(employeeData, out uri))
                return Created<EmployeeData>(Request.RequestUri.ToString()+@"/" + uri, employeeData);
            return InternalServerError();
        }

        public IHttpActionResult DeleteAll()
        {
            if (_dataManager.DeleteAll())
                return Ok();
            else return InternalServerError();
        }

        public IHttpActionResult Delete([FromUri]string id)
        {
            if (_dataManager.Delete(id))
                return Ok();
            else return InternalServerError();
        }

        public IHttpActionResult Put([FromUri]string id,[FromBody]EmployeeData employeeData)
        {
            if (_dataManager.Update(id, employeeData))
                return Ok();
            else return InternalServerError();
        }

        public EmployeeDataController()
        {
            if(_dataManager==null)
                _dataManager = new RandomDataManager();
        }
    }
}
