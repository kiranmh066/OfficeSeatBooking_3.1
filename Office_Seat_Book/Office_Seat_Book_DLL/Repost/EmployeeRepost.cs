using Office_Seat_Book_Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Office_Seat_Book_DLL.Repost
{
    public class EmployeeRepost : IEmployeeRepost
    {

        Office_DB_Context _dbContext;//default private

        public EmployeeRepost(Office_DB_Context dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddEmployee(Employee employee)
        {
            _dbContext.employee.Add(employee);
            _dbContext.SaveChanges();
        }

        public void DeleteEmployee(int employeeId)
        {
            var employee = _dbContext.employee.Find(employeeId);
            _dbContext.employee.Remove(employee);
            _dbContext.SaveChanges();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _dbContext.employee.Find(employeeId);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _dbContext.employee.ToList();
        }

        public Employee Login(Employee employee)
        {
            Employee employeeinfo = null;
            var result = _dbContext.employee.Where(obj => obj.Email == employee.Email && obj.Password == employee.Password).ToList();
            if (result.Count > 0)
            {
                employeeinfo = result[0];
            }
            return employeeinfo;

        }

        public void UpdateEmployee(Employee employee)
        {

            _dbContext.Entry(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
