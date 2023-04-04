using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Office_Seat_Book_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Drawing.Imaging;
using ZXing;
using System.Drawing;
using System.IO;
using ZXing.QrCode;
using System.Collections.Generic;

namespace Office_Seat_Book_MVC.Controllers
{
    public class UserController : Controller
    {

        public static List<Seat> seats = new List<Seat>();
        private IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
        public IActionResult GetFloorLayout()
        {
            return View(seats);
        }
        public async Task<IActionResult> EnterKey(SecretKey secretKeyInfo)
        {
            #region Checking whether already a generated special Key available if not it will be generated
            /*Random rnd = new Random();
            int randomNumber = rnd.Next(1, 100);*/

            Random random = new Random();
            int length = 4;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result2 = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result2.Append(chars[random.Next(chars.Length)]);
            }

            secretKeyInfo.EmpID = Convert.ToInt32(TempData["EmpId"]);
            TempData.Keep();
            secretKeyInfo.SpecialKey = result2.ToString();

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
                if (secretKey.SpecialKey.Count() == 0 || secretKey == null)
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
            int empId = Convert.ToInt32(TempData["EmpId"]);
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
            Employee employeeinfo = null;
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
            if (secretKey.SpecialKey == specialKey)
            {
                //employeeinfo.Emp_Statu = true;
            }
            else
            {
                //employeeinfo.Emp_Statu = false;
            }
            #endregion
            return View();
        }

        public async Task<IActionResult> GenerateQR()
        {
            #region Generating Displaying QR
            int empId = Convert.ToInt32(TempData["EmpId"]);
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
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            var bitmap = barcodeWriter.Write(secretKey.SpecialKey);
            bitmap.Save(@"C:\POC\OfficeSeatBooking_3.1\Office_Seat_Book\Office_Seat_Book_MVC\wwwroot\QRCode.bmp", ImageFormat.Bmp);
            #endregion
            return View();
        }
    }

}
