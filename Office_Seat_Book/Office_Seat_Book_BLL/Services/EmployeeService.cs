using Office_Seat_Book_DLL.Repost;
using Office_Seat_Book_Entity;
using System.Collections.Generic;

namespace Office_Seat_Book_BLL.Services
{
    public class EmployeeService
    {
        IEmployeeRepost _employeeRepost;
        public EmployeeService(IEmployeeRepost employeeRepost)
        {
            _employeeRepost = employeeRepost;
        }

        //Add Appointment
        public void AddEmployee(Employee employee)
        {
            _employeeRepost.AddEmployee(employee);
        }

        //Delete Appointment

        public void DeleteEmployee(int EmployeeID)
        {
            _employeeRepost.DeleteEmployee(EmployeeID);
        }

        //Update Appointment

        public void UpdateEmployee(Employee employee)
        {
            _employeeRepost.UpdateEmployee(employee);
        }

        //Get getAppointments

        public IEnumerable<Employee> GetEmployee()
        {
            return _employeeRepost.GetEmployees();
        }
        public Employee GetByEmployeeId(int EmployeeID)
        {
            return _employeeRepost.GetEmployeeById(EmployeeID);
        }
        public Employee Login(Employee employee)
        {
            return _employeeRepost.Login(employee);
        }
    }
}
