using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Office_Seat_Book_Entity;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using ZXing;
using System.IO;
using ZXing.QrCode.Internal;
using System.Collections;
using System.Collections.Generic;

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

        public async Task<IActionResult> EnterKey(SecretKey secretKeyInfo,string id)
        {
            #region Checking whether already a generated special Key available if not it will be generated
            Random rnd = new Random();
            /*int randomNumber = rnd.Next(1000, 9999);*/

            int length = 4;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result2 = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result2.Append(chars[rnd.Next(chars.Length)]);
            }
            string randomNum = result2.ToString();

            secretKeyInfo.EmpID = Convert.ToInt32(TempData["empId"]);
            TempData.Keep();
            secretKeyInfo.SpecialKey = randomNum.ToString();

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
        public async Task<IActionResult> EnterKey(SecretKey secretKey3)
        {
            #region Changing Employee status if User enters the correct security key
            ViewBag.status = "";
            string specialKey = secretKey3.SpecialKey + secretKey3.Employee.Name+ secretKey3.Employee.Email+ secretKey3.Employee.Password;
            int empId = Convert.ToInt32(TempData["empId"]);
            TempData.Keep();
            if (specialKey != null)
            {
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
                    booking.Booking_Status = 1;
                }
                else
                {
                    employeeinfo.EmployeeStatus = false;
                    booking.Booking_Status = 0;
                }
                #endregion

                #region updating the status in both employee and booking table
                
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(booking), Encoding.UTF8, "application/json");
                    string endPoint = _configuration["WebApiBaseUrl"] + "Booking/UpdateBooking";
                    using (var response = await client.PutAsync(endPoint, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK && booking.Booking_Status == 1)
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
                        if (response.StatusCode == System.Net.HttpStatusCode.OK && employeeinfo.EmployeeStatus == true)
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
            }
            else 
            { 
                ViewBag.status = "Error";
            }
            #endregion

            return View();
        }
        public async Task<IActionResult> GenerateQR(SecretKey secretKeyInfo)
        {
            #region Checking whether already a generated special Key available if not it will be generated
            Random rnd = new Random();
            /*int randomNumber = rnd.Next(1000, 9999);*/

            int length = 4;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result2 = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result2.Append(chars[rnd.Next(chars.Length)]);
            }
            string randomNum = result2.ToString();

            secretKeyInfo.EmpID = Convert.ToInt32(TempData["empId"]);
            TempData.Keep();
            secretKeyInfo.SpecialKey = randomNum.ToString();

            SecretKey secretKeyNew = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "SecretKey/GetSecretKeyByEmpId?empId=" + secretKeyInfo.EmpID;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        secretKeyNew = JsonConvert.DeserializeObject<SecretKey>(result);
                    }
                }
                if (secretKeyNew == null)
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

            #region Generating Displaying QR
            int empId = Convert.ToInt32(TempData["EmpId"]);
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
                        TempData["SecretIdForQr"] = Convert.ToInt32(secretKey.SecretId);
                        TempData.Keep();
                    }
                }
            }
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            var bitmap = barcodeWriter.Write(secretKey.SpecialKey);

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);
                secretKey.Qr = ms.ToArray();
            }
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(secretKey), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "SecretKey/UpdateSecretKey";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "QR Updated Successfully!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries!";
                    }
                }
            }
            SecretKey secretKey2 = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "SecretKey/GetSecretKeyById?secretKeyId=" + Convert.ToInt32(TempData["SecretIdForQr"]);
                TempData.Keep();

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        secretKey2 = JsonConvert.DeserializeObject<SecretKey>(result);
                    }
                }
            }
            #endregion
            return View(secretKey2);
        }
    }
}
