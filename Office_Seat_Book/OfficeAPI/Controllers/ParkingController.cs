using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Office_Seat_Book_BLL.Services;
using Office_Seat_Book_Entity;
using System.Collections.Generic;

namespace OfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {


        private ParkingService _ParkingService;
        public ParkingController(ParkingService parkingService)
        {

            _ParkingService = parkingService;
        }
        [HttpGet("GetParkings")]
        public IEnumerable<Parking> GetParkings()
        {
            return _ParkingService.GetParking();
        }
        [HttpGet("GetParkingById")]
        public Parking GetParkingById(int parkingId)
        {
            return _ParkingService.GetByParkingId(parkingId);
        }
        [HttpPost("AddParking")]
        public IActionResult AddParking([FromBody] Parking parking)
        {
            _ParkingService.AddParking(parking);


            return Ok("Parking created successfully!!");

        }
        [HttpDelete("DeleteParking")]
        public IActionResult DeleteParking(int parkingId)
        {
            _ParkingService.DeleteParking(parkingId);
            return Ok("Parking deleted successfully!!");
        }
        [HttpPut("UpdateParking")]
        public IActionResult UpdateParking([FromBody] Parking parking)
        {
            _ParkingService.UpdateParking(parking);
            return Ok("Parking updated successfully!!");
        }
    }
}
