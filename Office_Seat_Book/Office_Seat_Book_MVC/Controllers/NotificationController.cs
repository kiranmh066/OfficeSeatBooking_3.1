using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace Office_Seat_Book_MVC.Controllers
{
    public class NotificationController : Controller
    {
        private IConfiguration _configuration;
        public NotificationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        //public DateTime GetReminderTime(DateTime Shift_Time)
        //{
        //    return Shift_Time.AddMinutes(-15);
        //}

    }
}
