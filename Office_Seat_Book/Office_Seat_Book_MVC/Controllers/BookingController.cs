using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Office_Seat_Book_Entity;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;

namespace Office_Seat_Book_MVC.Controllers
{
    public class BookingController : Controller
    {
        private IConfiguration _configuration;
        public BookingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public  async Task<IActionResult> ViewPass()
        {
            
            Booking booking = new Booking();
            int EmpId= Convert.ToInt32(TempData["empId"]);
            TempData.Keep();
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Booking/GetBookingByEmpId?EmpId=" + EmpId;
                //EmployeeId is apicontroleer passing argument name
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        booking = JsonConvert.DeserializeObject<Booking>(result);
                    }
                }
            }
            return View(booking);
        }

        public async Task<IActionResult> EnterKey(SecretKey secretKeyInfo)
        {
            #region Checking whether already a generated special Key available if not it will be generated
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, 100);

            secretKeyInfo.EmpID = Convert.ToInt32(TempData["empId"]);
            TempData.Keep();
            secretKeyInfo.SpecialKey = randomNumber.ToString();

            using (HttpClient client = new HttpClient())
            {

                SecretKey secretKey = null;
                string endPoint = _configuration["WebApiBaseUrl"] + "SecretKey/GetSecretKeyByEmpId?empId=" + secretKeyInfo.EmpID;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        secretKey = JsonConvert.DeserializeObject<SecretKey>(result);
                    }
                }
                if (secretKey == null)
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(secretKeyInfo), Encoding.UTF8, "application/json");
                    string endPoint2 = _configuration["WebApiBaseUrl"] + "SecretKey/AddSecretKey";//api controller name and its function

                    using (var response = await client.PostAsync(endPoint2, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            ViewBag.status = "Ok";
                            ViewBag.message = "SecretKey Generated Successfull!!";
                        }
                        else
                        {
                            ViewBag.status = "Error";
                            ViewBag.message = "Wrong Entries";
                        }
                    }
                }
                else
                {
                    ViewBag.status = "Error";
                    ViewBag.message = "Alredy Security Key Generated";
                }
            }
            #endregion
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EnterKey(string specialKey)
        {
            #region Changing Employee status if User enters the correct security key
            ViewBag.status = "";
            int empId = Convert.ToInt32(TempData["empId"]);
            TempData.Keep();
            SecretKey secretKey = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "SecretKey/GetSecretKeyByEmpId?empId=" + empId;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        secretKey = JsonConvert.DeserializeObject<SecretKey>(result);
                    }
                }
            }
            Employee employeeinfo = new Employee();
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?EmployeeId=" + empId;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        //It will deserilize the object in the form of JSON
                        employeeinfo = JsonConvert.DeserializeObject<Employee>(result);
                    }
                }
            }
            Booking booking = new Booking();
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Booking/GetBookingByEmpId?EmpId=" + empId;
                //EmployeeId is apicontroleer passing argument name
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        booking = JsonConvert.DeserializeObject<Booking>(result);
                    }
                }
            }

            if (secretKey.SpecialKey == specialKey)
            {
                employeeinfo.EmployeeStatus = true;
                booking.booking_Status = 1;
            }
            else
            {
                employeeinfo.EmployeeStatus=false;
                booking.booking_Status = 0;
            }


            #endregion

            #region updating the status in both employee and booking table
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(booking), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Booking/UpdateBooking";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Seat Booked Successfully!!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Sorry Try Again Not Able to Book!!";
                    }

                }
            }

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(booking), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Booking/UpdateBooking";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Seat Booking Verified Successfully!!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Verification Unsuccessfull Try Again!!";
                    }

                }
            }
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(employeeinfo), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/UpdateEmployee";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Seat Booking Verified Successfully!!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Verification Unsuccessfull Try Again!!";
                    }

                }
            }

            #endregion

            return View();
        }
    }
}
