using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Office_Seat_Book_BLL.Services;
using Office_Seat_Book_Entity;
using System.Collections.Generic;
using System;

namespace OfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeService _EmployeeService;
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
            _EmployeeService.AddEmployee(employee);


            return Ok("Employee created successfully!!");

        }
        [HttpDelete("DeleteEmployee")]
        public IActionResult DeleteEmployee(int employeeId)
        {
            _EmployeeService.DeleteEmployee(employeeId);
            return Ok("Employee deleted successfully!!");
        }
        [HttpPut("UpdateEmployee")]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {
            _EmployeeService.UpdateEmployee(employee);
            return Ok("Employee updated successfully!!");
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

                    return null;
                }
            }
            catch (NullReferenceException)
            {
                return null;
            }
            #endregion
        }






    }
}
