using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Office_Seat_Book_BLL.Services;
using Office_Seat_Book_Entity;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Serilog;

namespace OfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeService _EmployeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(EmployeeService employeeService)
        {
            
            _EmployeeService = employeeService;
           
        }

        [HttpGet("GetEmployees")]
        public IEnumerable<Employee> GetEmployees()
        {
            return _EmployeeService.GetEmployee();
        }
        [HttpGet("GetEmployeeById")]
        public Employee GetEmployeeById(int EmployeeId)
        {
            return _EmployeeService.GetByEmployeeId(EmployeeId);
        }
        [HttpPost("AddEmployee")]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            #region Function for updating  the employee  by its object
            try
            {
                _EmployeeService.AddEmployee(employee);
                return Ok("Employee Added successfully!!");
            }
            catch
            {
                return BadRequest(400);
            }
            #endregion

        }
        [HttpDelete("DeleteEmployee")]
        public IActionResult DeleteEmployee(int employeeId)
        {
            #region Function for deleting the employee by its employeeId.
            try
            {
                _EmployeeService.DeleteEmployee(employeeId);
                return Ok("Employee deleted Successfully");
            }
            catch
            {
                return BadRequest(400);
            }
            #endregion
        }
        [HttpPut("UpdateEmployee")]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {
            #region Function for updating  the employee its object
            try
            {
                _EmployeeService.UpdateEmployee(employee);
                return Ok("Employee Updated Successfully");
            }
            catch
            {
                return BadRequest(400);
            }
            #endregion
        }

        [HttpPost("Login")]
        public Employee Login(Employee employee)
        {
            #region Function of login
            try
            {
                Employee Employee = _EmployeeService.Login(employee);
                if (Employee != null)
                {
                    return Employee;
                }
                else
                {
                    //_logger.LogInformation("Logging demo");
                    //_logger.LogWarning("logging Warning");
                    //_logger.LogError("Log Errror");
                    //_logger.LogCritical("Email Log");
                    return null;
                }
            }
            catch (NullReferenceException)
            {
                _logger.LogInformation("Logging demo");
                _logger.LogWarning("logging Warning");
                _logger.LogError("Log Errror");
                _logger.LogCritical("Email Log");
                return null;
            }
            #endregion
        }






    }
}
