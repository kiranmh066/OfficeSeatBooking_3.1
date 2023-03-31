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
        public static List<Seat> seats = new List<Seat>();
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
            booking.EmployeeID =Convert.ToInt32(TempData["empId"]);
            TempData.Keep();
            booking.Seat_No =1;
            booking.Emp_Status = true;
            booking.Food_Type = 1;
            booking.Vehicle = true;
            booking.booking_Status = 0;

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
                
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> BookSeat2()
        {
            Booking booking = new Booking();
            Floor floor=new Floor();
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
            List<Floor> floors = new List<Floor>();
            using (HttpClient client = new HttpClient())
            {
                // LocalHost Adress in endpoint
                string endPoint = _configuration["WebApiBaseUrl"] + "Floor/GetFloors";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        //It will deserilize the object in the form of JSON
                        floors = JsonConvert.DeserializeObject<List<Floor>>(result);
                    }
                }
            }

            List<SelectListItem> floor1 = new List<SelectListItem>();



            //fetching the departments and adding to the Viewbag for selecting appointment
            floor1.Add(new SelectListItem { Value = null, Text = "Select Floor" });
            foreach (var item in floors)
            {
                floor1.Add(new SelectListItem { Value = item.FloorID.ToString(), Text = item.FloorName });
            }



            ViewBag.FloorList = floor1;

            ViewBag.shiftTimings = ShiftTiming();
            return View(booking);

        }
        [HttpPost]
        public async Task<IActionResult> BookSeat2(Booking booking)
        {
            booking.BookingID = Convert.ToInt32(TempData["Bookid"]);
            booking.EmployeeID = Convert.ToInt32(TempData["empId"]);
            TempData.Keep();
            booking.Seat_No = 1;
            booking.Emp_Status = true;
            booking.Food_Type = 1;
            booking.Vehicle = true;
            booking.booking_Status = 0;
            int floorId = booking.seat.FloorID;
            ViewBag.status = "";

            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Seat/GetSeatsByFloorId?floorId=" + floorId;
                //EmployeeId is apicontroleer passing argument name
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        seats = JsonConvert.DeserializeObject<List<Seat>>(result);
                    }
                }
            }
            return RedirectToAction("GetFloorLayout", "Employee");
        }

        public IActionResult GetFloorLayout()
        {
            return View(seats);
        }


        [HttpGet]
        public async Task<IActionResult> BookSeatByUpdatingSeatId(int SeatId)
        {
            int bookingId = Convert.ToInt32(TempData["Bookid"]);
            TempData.Keep();
            Booking booking = new Booking();
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Booking/GetBookingById?bookingId=" + bookingId;
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
            booking.Seat_No = SeatId;
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


            return View();

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
