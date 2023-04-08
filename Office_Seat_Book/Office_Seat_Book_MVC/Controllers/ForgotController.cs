using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Office_Seat_Book_Entity;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Office_Seat_Book_MVC.Controllers
{
    public class ForgotController : Controller
    {
        private IConfiguration _configuration;
        public ForgotController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Employee employee)
        {
            TempData["empId1"] = Convert.ToInt32(employee.EmpID);
            TempData.Keep();
            Employee employee1 = new Employee();
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?EmployeeId=" + employee.EmpID;
                //EmployeeId is apicontroleer passing argument name
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        employee1 = JsonConvert.DeserializeObject<Employee>(result);
                    }
                }
            }
            if (employee.Security_Question == employee1.Security_Question)
            {
                return RedirectToAction("UpdatePassword", "Forgot");
            }
            else
            {
                ViewBag.status = "Error";
                ViewBag.message = "Either EmpId or Year of Birth Not Matching Try Again!!";
                return View();
            }

        }
        public IActionResult UpdatePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(Employee employee)
        {
            int id = Convert.ToInt32(TempData["empId1"]);
            if (employee.Password == employee.Security_Question)
            {
                Employee employee1 = new Employee();
                using (HttpClient client = new HttpClient())
                {
                    string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?EmployeeId=" + id;
                    //EmployeeId is apicontroleer passing argument name
                    using (var response = await client.GetAsync(endPoint))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            var result = await response.Content.ReadAsStringAsync();
                            employee1 = JsonConvert.DeserializeObject<Employee>(result);
                        }
                    }
                }
                employee1.Password = employee.Password;

                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(employee1), Encoding.UTF8, "application/json");
                    string endPoint = _configuration["WebApiBaseUrl"] + "Employee/UpdateEmployee";
                    using (var response = await client.PutAsync(endPoint, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            ViewBag.status = "Ok";
                            ViewBag.message = "Password Updated Successfull!!";
                        }
                    }
                }
                return View();
            }

            else
            {
                return RedirectToAction("UpdatePassword", "Forgot");
            }







        }
    }
}