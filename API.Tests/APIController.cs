using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeDataManager;
using EmployeeDataManager.Contracts;
using EmployeeDataManager.Controllers;
using EmployeeDataManager.DataManagers;
using EmployeeDataManager.Models;
using System.Collections.Generic;

namespace API.Tests
{
    [TestClass]
    public class APIController
    {
        private EmployeeDataController _apiController;
        private List<Employee> _employeeList;
        private IEmployeeDataManager _dataManager;

        public APIController()
        {
            _dataManager = new RandomDataManager();
            _apiController = new EmployeeDataController(_dataManager);
            _dataManager.GetAll(out _employeeList);
        }

        [TestMethod]
        public void GetAll_Should_Return_All_The_Data()
        {
            
        }
    }
}
