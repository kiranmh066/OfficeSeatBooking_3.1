﻿using Aspose.BarCode.Generation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;
using Org.BouncyCastle.Ocsp;
using Newtonsoft.Json;
using Office_Seat_Book_Entity;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Office_Seat_Book_Entity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
            Employee employee = null;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?EmployeeId=" + Convert.ToInt32(TempData["empId"]); 
                TempData.Keep();
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
            #region profile
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
                new SelectListItem{Value="Select",Text="Shift Timings"},
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
                new SelectListItem{Value="Select",Text="Type Of Request"},
                new SelectListItem{Value="0",Text="Daily"},
                new SelectListItem{Value="1",Text="Weekly"},
                new SelectListItem{Value="2",Text="Custom"},
            };
            return request;
        }
        public List<SelectListItem> YesorNoDropDown()
        {
            List<SelectListItem> YesorNorequest = new List<SelectListItem>()
            {
                new SelectListItem{Value="Select",Text="Select Yes/No"},
                new SelectListItem{Value=true.ToString(),Text="YES"},
                new SelectListItem{Value=false.ToString(),Text="NO"},
                
            };
            return YesorNorequest;
        }
        public List<SelectListItem> TypeOfVehicle()
        {
            List<SelectListItem> typeofvehicle = new List<SelectListItem>()
            {
                new SelectListItem{Value="Select",Text="select"},
                new SelectListItem{Value="0",Text="2 Wheeler"},
                new SelectListItem{Value="1",Text="4 wheeler"},
            };
            return typeofvehicle;
        }


        public async Task<IActionResult> BookSeat()
        {
           
            //List<Booking> booking2 = null;
            Booking booking2 = new Booking();
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Booking/GetBookingByEmpId?EmpId=" + Convert.ToInt32(TempData["empId"]);
                TempData.Keep(); ;
                //EmployeeId is apicontroleer passing argument name
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        booking2 = JsonConvert.DeserializeObject<Booking>(result);
                    }
                }
            }
            if (booking2!=null ||( booking2.Booking_Status !=0 && booking2.Booking_Status !=1)|| booking2.To_Date < DateTime.Today )
            {
                ViewBag.status = "Error";
                ViewBag.message = "Alredy a seat waiting for you!!";
                return View(booking2);

            }
            ViewBag.shiftTimings = ShiftTiming();
            ViewBag.requests = RequestType();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BookSeat(Booking booking)
        {
          
         
                #region Booking Seat
                booking.From_Date = DateTime.Today;
                booking.To_Date = DateTime.Today;

                int bookingId = 0;
                booking.EmployeeID = Convert.ToInt32(TempData["empId"]);
                TempData.Keep();
                booking.Seat_No = 1;
                booking.Food_Type = 1;
                booking.Vehicle = true;
                booking.Booking_Status = 0;

                booking.Shift_Time = "nothing";

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
                
                #endregion


            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> BookSeat2()
        {
            Booking booking = new Booking();
            Floor floor = new Floor();
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
            ViewBag.yesornorequest = YesorNoDropDown();
            return View(booking);

        }
        [HttpPost]
        public async Task<IActionResult> BookSeat2(Booking booking)
        {
            booking.BookingID = Convert.ToInt32(TempData["Bookid"]);
            TempData.Keep();
            booking.EmployeeID = Convert.ToInt32(TempData["empId"]);
            TempData.Keep();
            booking.Seat_No = 1;
            booking.Food_Type = 1;
            TempData["Vehical"] = booking.Vehicle;
            booking.Booking_Status = 0;
            int floorId = booking.seat.FloorID;
            booking.seat = null;
            ViewBag.status = "";

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(booking), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Booking/UpdateBooking";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Seat Updated Successfully!!";
                       
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

            Seat seat = new Seat();
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Seat/GetSeatById?seatId=" + SeatId;
                //EmployeeId is apicontroleer passing argument name
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        seat = JsonConvert.DeserializeObject<Seat>(result);
                    }
                }
            }
            seat.Seat_flag = false;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(seat), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Seat/UpdateSeat";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Seat Booked Successfully!!";
                        bool x = (bool)TempData["Vehical"];
                        if (x == true)
                        {
                            return RedirectToAction("SelectingTypeofVehicle", "Employee");
                        }
                        else
                        {
                            return RedirectToAction("ViewPass", "Booking");
                        }
                    }
                }
            }

            return View();

        }

        public IActionResult SelectingTypeofVehicle()
        {
            ViewBag.Yes_or_No_Request = YesorNoDropDown();
            return View();
        }
        [HttpPost]
        public IActionResult SelectingTypeofVehicle(Parking parking)
        {
            TempData["ParkingType"] = parking.ParkingType;
            
            return RedirectToAction("SelectingVehicle", "Employee");
        }



        public async Task<IActionResult> SelectingVehicle(int id)
        {
            Parking parking = new Parking();
           
            TempData.Keep();
            List<Parking> parkings = new List<Parking>();
            using (HttpClient client = new HttpClient())
            {
                // LocalHost Adress in endpoint
                string endPoint = _configuration["WebApiBaseUrl"] + "Parking/GetParkings";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        //It will deserilize the object in the form of JSON
                        parkings = JsonConvert.DeserializeObject<List<Parking>>(result);
                    }
                }
            }
            if (id != 0)
            {
               
                parking.BookingID= Convert.ToInt32(TempData["Bookid"]); ;
                TempData.Keep();
               
                parking.Parking_Number = id;
                ViewBag.status = "";
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(parking), Encoding.UTF8, "application/json");
                    string endPoint = _configuration["WebApiBaseUrl"] + "Parking/AddParking";
                    using (var response = await client.PostAsync(endPoint, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            ViewBag.status = "Ok";
                            ViewBag.message = "Parking Booked successfully!";
                            return RedirectToAction("ViewPass", "Booking");

                        }

                    }
                }
            }
          
            return View(parkings);
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
