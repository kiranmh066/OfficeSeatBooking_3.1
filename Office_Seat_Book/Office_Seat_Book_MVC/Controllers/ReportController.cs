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
        public IActionResult GetCustomDates()
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetCustomDates(Booking booking)
        {
           
            int d = Convert.ToInt32(booking.From_Date.Day);
            int e = Convert.ToInt32(booking.To_Date.Day);
            int j = 0;

            for (int i = d; i <= e; i++)
            {
                List<Booking> bookings = null;
                DateTime date1 = booking.From_Date.AddDays(j);
                string date2 = date1.ToString("yyyy-MM-dd");
                using (HttpClient client = new HttpClient())
                {
                    string endPoint = _configuration["WebApiBaseUrl"] + "Booking/GetBookingsByDate?date1=" + date2;
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
                foreach(var item in bookings)
                {
                    item.From_Date = date1;
                    bookings1.Add(item);
                }
               

            }
            return RedirectToAction("GetReportByWeek", "Report");
        }


        public async Task<IActionResult> GetReportByWeek()
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
            var tupeluser = new Tuple<List<Employee>, List<Booking>>(employees, bookings1);
            return View(tupeluser);

        }



    }
}
