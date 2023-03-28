using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Office_Seat_Book_Entity;
using System;
using System.Net.Http;
using System.Threading.Tasks;

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
    }
}
