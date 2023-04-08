using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Office_Seat_Book_Entity;
using Office_Seat_Book_MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Office_Seat_Book_MVC.Controllers
{
    public class HomeController : Controller
    {

        private IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public async Task<IActionResult> Index()
        {
            var sixPM = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0);//0
            var twoPM = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0);//1
            var tenAM = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0);//2
            var onePM = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 0, 0);//3

            Booking booking = null;
            IEnumerable<Booking> bookingList = null;
            {
                using (HttpClient client = new HttpClient())
                {
                    string endPoint = _configuration["WebApiBaseUrl"] + "Booking/GetBookings";//api controller name and httppost name given inside httppost in moviecontroller of api
                    using (var response = await client.GetAsync(endPoint))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            var result = await response.Content.ReadAsStringAsync();
                            bookingList = JsonConvert.DeserializeObject<IEnumerable<Booking>>(result);
                        }
                    }
                }
                if (bookingList != null)
                {
                    foreach (var item in bookingList)
                    {
                        if (item.To_Date < DateTime.Today)
                        {
                            item.Booking_Status = 3;
                            using (HttpClient client = new HttpClient())
                            {
                                StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                                string endPoint = _configuration["WebApiBaseUrl"] + "Booking/UpdateBooking";
                                using (var response = await client.PutAsync(endPoint, content))
                                {
                                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        ViewBag.status = "Ok";
                                        ViewBag.message = "Booking Viewpass Updated sucessfully!!";
                                    }
                                    else
                                    {
                                        ViewBag.status = "Error";
                                        ViewBag.message = "Wrong entries!";
                                    }
                                }
                            }
                        }
                        if ((item.Type_Of_Request == 0 && DateTime.Now > sixPM) || (item.Type_Of_Request == 1 && DateTime.Now > twoPM) || (item.Type_Of_Request == 2 && DateTime.Now > tenAM) || (item.Type_Of_Request == 3 && DateTime.Now > onePM))
                        {
                            item.Booking_Status = 2;
                            //item.seat.Seat_flag = true;
                            using (HttpClient client = new HttpClient())
                            {
                                StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                                string endPoint = _configuration["WebApiBaseUrl"] + "Booking/UpdateBooking";
                                using (var response = await client.PutAsync(endPoint, content))
                                {
                                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        ViewBag.status = "Ok";
                                        ViewBag.message = "Booking Viewpass Updated sucessfully!!";
                                    }
                                    else
                                    {
                                        ViewBag.status = "Error";
                                        ViewBag.message = "Wrong entries!";
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(Employee employee)
        {
            if (employee.Email != null && employee.Password != null)
            {
                #region Logging in of Employee using Email and Password and Will Redirect using Employee designation
                try
                {
                    Employee employee1 = null;
                    ViewBag.status = "";
                    using (HttpClient client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
                        string endPoint = _configuration["WebApiBaseUrl"] + "Employee/Login";
                        using (var response = await client.PostAsync(endPoint, content))
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            employee1 = JsonConvert.DeserializeObject<Employee>(result);
                            if (employee1 != null)
                            {
                                TempData["EmpName"] = (employee1.Name).ToString();
                                TempData.Keep();
                                string employee_role = (employee1.Role).ToString();
                                TempData["employee_role"] = employee_role;
                                TempData.Keep();
                                TempData["empId"] = Convert.ToInt32(employee1.EmpID);
                                TempData.Keep();
                                TempData["TotalCount"] = Convert.ToInt32(TempData["helpcount2"]) + Convert.ToInt32(TempData["regcount2"]);
                                TempData.Keep();
                                int a = Convert.ToInt32(TempData["TotalCount"]);
                                //TempData["helpcount1"] = Convert.ToInt32(TempData["helpcount2"]);
                                //TempData.Keep();
                                //TempData["regcount1"] = Convert.ToInt32(TempData["regcount2"]);
                                //TempData.Keep();
                                TempData["register"] = "profile was added";
                                TempData.Keep();
                                TempData["help"] = "you get one query";
                                TempData.Keep();
                                if (employee_role == "ADMIN")
                                    return RedirectToAction("Index", "Admin");
                                else if (employee_role == "USER")
                                    return RedirectToAction("Index", "Employee");
                                else if (employee_role == "RECEPTIONIST")
                                    return RedirectToAction("Index", "Receptionist");
                            }
                            else
                            {
                                ViewBag.status = "Error";
                                ViewBag.message = "Wrong credentials!";
                            }
                        }
                    }
                }
                catch (NullReferenceException e)
                {
                    ViewBag.status = "Error";
                    ViewBag.message = "Enter Correct Credentials!";


                }


            }
            else

            {
                ViewBag.status = "Error";
                ViewBag.message = "Please Enter The Credentials!";

            }

            return View();

            #endregion
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}