using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Office_Seat_Book_BLL.Services;
using Office_Seat_Book_Entity;
using System.Collections.Generic;

namespace Office_Seat_Book_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorController : ControllerBase
    {
        private FloorService _FloorService;
        public FloorController(FloorService floorService)
        {

            _FloorService = floorService;
        }
        [HttpGet("GetFloors")]
        public IEnumerable<Floor> GetFloors()
        {
            return _FloorService.GetFloor();
        }
        [HttpGet("GetFloorById")]
        public Floor GetFloorById(int floorId)
        {
            return _FloorService.GetByFloorId(floorId);
        }
        [HttpPost("AddFloor")]
        public IActionResult AddFloor([FromBody] Floor floor)
        {
            _FloorService.AddFloor(floor);


            return Ok("Floor created successfully!!");

        }
        [HttpDelete("DeleteFloor")]
        public IActionResult DeleteFloor(int floorId)
        {
            _FloorService.DeleteFloor(floorId);
            return Ok("Floor deleted successfully!!");
        }
        [HttpPut("UpdateFloor")]
        public IActionResult UpdateFloor([FromBody] Floor floor)
        {
            _FloorService.UpdateFloor(floor);
            return Ok("Floor updated successfully!!");
        }
    }
}
