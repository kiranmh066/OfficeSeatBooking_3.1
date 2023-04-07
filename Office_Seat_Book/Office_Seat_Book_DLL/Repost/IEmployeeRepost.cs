using Office_Seat_Book_Entity;
using System.Collections.Generic;

namespace Office_Seat_Book_DLL.Repost
{
    public interface IEmployeeRepost
    {
        void UpdateEmployee(Employee employee);

        void DeleteEmployee(int employeeId);

        Employee GetEmployeeById(int employeeId);

        IEnumerable<Employee> GetEmployees();

        void AddEmployee(Employee employee);
        Employee Login(Employee employee);
    }
}
