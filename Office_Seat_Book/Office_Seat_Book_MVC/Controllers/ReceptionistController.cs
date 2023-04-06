using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Office_Seat_Book_Entity;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Data;
using Org.BouncyCastle.Ocsp;
using System.Text;

namespace Office_Seat_Book_MVC.Controllers
{
    public class ReceptionistController : Controller
    {
        private IConfiguration _configuration;

        public ReceptionistController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Scan()
        {

            return View();
        }

        public IActionResult ScanQr()
        {
         
            return View();
        }



        public IActionResult GenerateOTP()
        {
            return View();
        }
        public IActionResult ShowSecretKey()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ShowSecretKey(SecretKey secretKey)
        {
            TempData["SecretKeyEmpId"] = Convert.ToInt32(secretKey.EmpID);
            TempData.Keep();
            return RedirectToAction("ShowSecretKeyByEmpId", "Receptionist");
            //return View();
        }
        public async Task<IActionResult> ShowSecretKeyByEmpId()
        {
            int empId = Convert.ToInt32(TempData["SecretKeyEmpId"]);
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
            return View(secretKey);
        }


        public async Task<IActionResult>GetEmpIdBySecretId(string myContent)
        {
           
            /*int EmpIdScanned=0;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "SecretKey/GetEmpIdBySpecialKey?specialKey=" + myContent;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        EmpIdScanned = JsonConvert.DeserializeObject<int>(result);
                    }
                }
            }

            Booking booking = new Booking();
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Booking/GetBookingByEmpId?EmpId=" + EmpIdScanned;
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

            booking.Booking_Status = 1;

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(booking), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Booking/UpdateBooking";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Booking Verified Successfully!!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Not Able To Verify";
                    }

                }
            }*/
            return View();
        }
    }
}
