using Microsoft.AspNetCore.Mvc;
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
    public class HelpController : Controller
    {
        private IConfiguration _configuration;

        public HelpController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Help help)
        {
            help.EmpID =Convert.ToInt32(TempData["EmpId"]);
            TempData.Keep();
            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(help), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Help/AddHelp";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Query Added successfully!";
                        TempData["helpcount1"] = Convert.ToInt32(TempData["helpcount1"]) + 1;
                        TempData.Keep();
                        int a = Convert.ToInt32(TempData["helpcount1"]);
                        TempData["helpcount2"] = TempData["helpcount1"];
                        TempData.Keep();
                        TempData["help"] = "you get one query";
                        TempData.Keep();
                        TempData["TotalCount"] = Convert.ToInt32(TempData["helpcount2"]) + Convert.ToInt32(TempData["regcount2"]);
                        TempData.Keep();
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong entries!";
                    }
                }
            }
            return View();
        }
        public IActionResult GetAllHelps()
        {            
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllHelps(Help help)
        {
            IEnumerable<Help> helpresult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Help/GetHelps";

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {  
                        var result = await response.Content.ReadAsStringAsync();
                        helpresult = JsonConvert.DeserializeObject<IEnumerable<Help>>(result);
                    }
                }
            }
            return View(helpresult);
        }
    }
}
