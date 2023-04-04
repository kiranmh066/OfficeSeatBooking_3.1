using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Office_Seat_Book_BLL.Services;
using Office_Seat_Book_Entity;
using System.Collections.Generic;

namespace OfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpController : ControllerBase
    {
        private HelpService _helpService;
        public HelpController(HelpService helpService)
        {

            _helpService = helpService;
        }
        [HttpGet("GetHelps")]
        public IEnumerable<Help> GetHelps()
        {
            return _helpService.GetHelp();
        }
        [HttpGet("GetHelpById")]
        public Help GetHelpById(int helpId)
        {
            return _helpService.GetByHelpId(helpId);
        }
        [HttpPost("AddHelp")]
        public IActionResult AddHelp([FromBody] Help help)
        {
            try
            {
                _helpService.AddHelp(help);


                return Ok("Help created successfully!!");
            }
            catch
            {
                return BadRequest(400);
            }
        }
        [HttpDelete("DeleteHelp")]
        public IActionResult DeleteHelp(int helpId)
        {
            try
            {

                _helpService.GetByHelpId(helpId);
                return Ok("Help deleted successfully!!");
            }
            catch
            {
                return BadRequest(400);
            }
        }
        [HttpPut("UpdateHelp")]
        public IActionResult UpdateHelp([FromBody] Help help)
        {
            try
            {
                _helpService.UpdateHelp(help);
                return Ok("Help updated successfully!!");
            }
            catch
            {
                return BadRequest(400);
            }
        }
    }
}
