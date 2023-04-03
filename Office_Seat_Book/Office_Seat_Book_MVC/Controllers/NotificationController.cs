using Microsoft.AspNetCore.Mvc;

namespace Office_Seat_Book_MVC.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
