using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using Office_Seat_Book_Entity;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Office_Seat_Book_MVC.Controllers
{
    public class UserController : Controller
    {

        private IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static List<Seat> seats = new List<Seat>();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FloorSelect()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FloorSelect(Seat seat)
        {
           
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Seat/GetSeatsByFloorId?floorId=" + seat.FloorID;
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
            return RedirectToAction("GetFloorLayout", "User");



        }
        [HttpGet]
        public async Task<IActionResult> BookSeatByUpdatingSeatId(int SeatId)
        {
            int bookingId = 0;
            Booking booking=new Booking();
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






        public IActionResult GetFloorLayout()
        {
            return View(seats);
        }

    }


}
