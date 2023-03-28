using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Office_Seat_Book_Entity;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Office_Seat_Book_MVC.Controllers
{
    public class UserController : Controller
    {
        IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {

            _configuration = configuration;
        }
        public IActionResult Index()
        {
            #region Sample EmpId
            TempData["EmpId"] = 1;
            TempData.Keep();
            #endregion
            return View();
        }
        public async Task<IActionResult> EnterKey(SecretKey secretKeyInfo)
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, 100);

            secretKeyInfo.EmpID = Convert.ToInt32(TempData["EmpId"]);
            secretKeyInfo.SpecialKey = randomNumber.ToString();

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(secretKeyInfo), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "SecretKey/AddSecretKey";//api controller name and its function

                using (var response = await client.PostAsync(endPoint, content))
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EnterKey()
        {
            ViewBag.status = "";

           
            return View();
        }
    }
}
