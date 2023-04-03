using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Office_Seat_Book_BLL.Services;
using Office_Seat_Book_Entity;
using System.Collections.Generic;

namespace OfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private BookingService _BookingService;
        public BookingController(BookingService bookingService)
        {
            _BookingService = bookingService;
        }
        [HttpGet("GetBookings")]
        public IEnumerable<Booking> GetBookings()
        {
            return _BookingService.GetBookings();
        }
        [HttpGet("GetBookingById")]
        public Booking GetBookingById(int bookingId)
        {
            return _BookingService.GetBookingById(bookingId);
        }
        [HttpPost("AddBooking")]
        public int AddBooking([FromBody] Booking booking)
        {


            return _BookingService.AddBooking(booking);

            
           

        }
        [HttpDelete("DeleteBooking")]
        public IActionResult DeleteBooking(int bookingId)
        {
            try
            {
                _BookingService.DeleteBooking(bookingId);
                return Ok("Booking deleted successfully!!");
            }
            catch
            {
                return BadRequest(400);
            }
        }
        [HttpPut("UpdateBooking")]
        public IActionResult UpdateBooking([FromBody] Booking booking)
        {
            try
            {
                _BookingService.UpdateBooking(booking);
                return Ok("Booking updated successfully!!");
            }
            catch
            {
                return BadRequest(400);
            }
        }

        [HttpGet("GetBookingByEmpId")]
        public Booking GetBookingByEmpId(int EmpId)
        {
            
            return _BookingService.GetBookingByEmpId(EmpId);
    
        }

    }
}
