using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Office_Seat_Book_Entity;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace Office_Seat_Book_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            int PatientProfileId = 1;
            Employee employee = null;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?EmployeeId=" + PatientProfileId;
                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        employee = JsonConvert.DeserializeObject<Employee>(result);
                    }
                }
            }
            return View(employee);
        }

    
        public async Task<IActionResult> Profile()
        {
            #region Patient profile
            //storing the profile Id
            /* int PatientProfileId = Convert.ToInt32(TempData["ProfileID"]);
             TempData.Keep();*/
            int PatientProfileId = 1;

            Employee employee = null;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?EmployeeId=" + PatientProfileId;
                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        employee = JsonConvert.DeserializeObject<Employee>(result);
                    }
                }
            }
            return View(employee);
            #endregion
        }
        public List<SelectListItem> ShiftTiming()
        {
            List<SelectListItem> shiftTiming = new List<SelectListItem>()
            {
                new SelectListItem{Value="Select",Text="select"},
                new SelectListItem{Value="02:00pm-10:00pm",Text="02:00pm-10:00pm"},
                new SelectListItem{Value="10:00am-06:00pm",Text="10:00am-06:00pm"},
                new SelectListItem{Value="06:00am-02:00pm",Text="06:00am-02:00pm"},
                new SelectListItem{Value="09:00am-06:00pm",Text="09:00am-06:00pm"},
            };
           

            return shiftTiming;
        }

        public IActionResult BookSeat()
        {
            ViewBag.shiftTimings = ShiftTiming();
            ViewBag.requests = RequestType();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BookSeat(Booking booking)
        {
            booking.Shift_Time = "Nothing";
            booking.From_Date = DateTime.Today;
            booking.To_Date = DateTime.Today;

            int bookingId = 0;
            booking.EmployeeID = 1;
            booking.Seat_No = 1;
            booking.Emp_Status = 1;
            booking.Food_Type = 1;
            booking.Vehicle = true;
            booking.booking_Status = 9;

            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(booking), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Booking/AddBooking";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        var result = await response.Content.ReadAsStringAsync();
                        bookingId = JsonConvert.DeserializeObject<int>(result);
                        TempData["Bookid"] = bookingId;
                        TempData.Keep();
                        ViewBag.status = "Ok";
                        ViewBag.message = "Booked successfully!";
                        return RedirectToAction("BookSeat2", "Employee");
                       
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong entries!";
                    }
                }
            }
            return View();
        }
        public async Task<IActionResult> BookSeat2()
        {
            Booking booking = new Booking();
            //it will fetch the Doctor Details by using DoctorID
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Booking/GetBookingById?bookingId=" + Convert.ToInt32(TempData["Bookid"]);
                TempData.Keep();
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        //It will deserilize the object in the form of JSON
                        booking = JsonConvert.DeserializeObject<Booking>(result);
                    }
                }
            }
            ViewBag.shiftTimings = ShiftTiming();
            return View(booking);

        }
        [HttpPost]
        public async Task<IActionResult> BookSeat2(Booking booking)
        {
            booking.BookingID = Convert.ToInt32(TempData["Bookid"]);
            booking.EmployeeID = 1;
            booking.Seat_No = 1;
            booking.Emp_Status = 1;
            booking.Food_Type = 1;
            booking.Vehicle = true;
            booking.booking_Status = 0;
            ViewBag.status = "";
            //it will update the doctor details after Admin Changes
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(booking), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Booking/UpdateBooking";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Doctor Details Updated Successfully!";
                  
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries!";
                    }
                }
            }
            return View();
        }


        public List<SelectListItem> RequestType()
        {
            List<SelectListItem> request = new List<SelectListItem>()
            {
                new SelectListItem{Value="Select",Text="select"},
                new SelectListItem{Value="0",Text="Daily"},
                new SelectListItem{Value="1",Text="Weekly"},
                new SelectListItem{Value="2",Text="Custom"},
            };
            return request;
        }

        [HttpGet]
        public async Task<IActionResult> Booking_history()
        {
            IEnumerable<Booking> empresult = null;
            using (HttpClient client = new HttpClient())
            {
                // LocalHost Adress in endpoint
                string endPoint = _configuration["WebApiBaseUrl"] + "Booking/GetBookings";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        //It will deserilize the object in the form of JSON
                        empresult = JsonConvert.DeserializeObject<IEnumerable<Booking>>(result);
                    }
                }
            }
            return View(empresult);
        }
    }
}
