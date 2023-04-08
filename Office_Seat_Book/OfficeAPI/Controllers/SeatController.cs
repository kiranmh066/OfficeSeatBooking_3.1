﻿using Microsoft.AspNetCore.Mvc;
using Office_Seat_Book_BLL.Services;
using Office_Seat_Book_Entity;
using System.Collections.Generic;

namespace OfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private SeatService _SeatService;
        public SeatController(SeatService seatService)
        {

            _SeatService = seatService;
        }
        [HttpGet("GetSeats")]
        public IEnumerable<Seat> GetSeats()
        {
            return _SeatService.GetSeat();
        }
        [HttpGet("GetSeatById")]
        public Seat GetSeatById(int seatId)
        {
            return _SeatService.GetBySeatId(seatId);
        }
        [HttpPost("AddSeat")]
        public IActionResult AddSeat([FromBody] Seat seat)
        {
            try
            {
                _SeatService.AddSeat(seat);


                return Ok("Seat created successfully!!");
            }
            catch
            {
                return BadRequest(400);
            }
        }
        [HttpDelete("DeleteSeat")]
        public IActionResult DeleteSeat(int seatId)
        {
            try
            {
                _SeatService.DeleteSeat(seatId);
                return Ok("Seat deleted successfully!!");
            }
            catch
            {
                return BadRequest(400);
            }
        }
        [HttpPut("UpdateSeat")]
        public IActionResult UpdateSeat([FromBody] Seat seat)
        {
            try
            {
                _SeatService.UpdateSeat(seat);
                return Ok("Seat updated successfully!!");
            }
            catch
            {
                return BadRequest(400);
            }
        }
        [HttpGet("GetSeatsByFloorId")]
        public IEnumerable<Seat> GetSeats(int floorId)
        {
            return _SeatService.GetSeatsByFloorId(floorId);
        }

    }
}
