using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Office_Seat_Book_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Office_Seat_Book_MVC.Controllers
{
    public class ReportController : Controller
    {
        private IConfiguration _configuration;
        public static List<Booking> bookings1 = new List<Booking>();
        public static List<Booking> bookings = new List<Booking>();

        public ReportController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {


            return View();
        }


        
        [HttpPost]
        public async Task<IActionResult> Index(Booking booking)
        {
            DateTime date1 = booking.To_Date;
            string date2 = date1.ToString("yyyy-MM-dd");
            IEnumerable<Booking> bookings2 = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Booking/GetBookingsByDate?date1=" + date2;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        //It will deserilize the object in the form of JSON
                        bookings2 = JsonConvert.DeserializeObject<IEnumerable<Booking>>(result);
                    }
                }
                bookings=bookings2.ToList();
            }
            return RedirectToAction("GetReportByDate","Report");  
        }
        public async Task<IActionResult> GetReportByDate()
        {
            List<Employee> employees = null;
            using (HttpClient client = new HttpClient())
            {
                // LocalHost Adress in endpoint
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployees";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        //It will deserilize the object in the form of JSON
                        employees = JsonConvert.DeserializeObject<List<Employee>>(result);
                    }
                }
            }
            var tupeluser = new Tuple<List<Employee>, List<Booking>>(employees, bookings);
            return View(tupeluser);

        }


        public async Task<IActionResult> GetReportByWeek(DateTime fromdate1, DateTime todate1)
        {
            int d = Convert.ToInt32(fromdate1.Date);
            int e = Convert.ToInt32(todate1.Date);
            int j = 0;

            for(int i=d;i<=e;i++)
            {
                List<Booking> bookings = null;

                using (HttpClient client = new HttpClient())
                {
                    string endPoint = _configuration["WebApiBaseUrl"] + "Booking/GetBookingsByDate?date1=" + fromdate1.AddDays(j);
                    TempData.Keep();
                    using (var response = await client.GetAsync(endPoint))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            //It will deserilize the object in the form of JSON
                            bookings = JsonConvert.DeserializeObject<List<Booking>>(result);
                        }
                    }

                }
                j++;
                bookings1 = bookings1.Concat(bookings).ToList();

            }
            return View(bookings1);
        }



    }
}
