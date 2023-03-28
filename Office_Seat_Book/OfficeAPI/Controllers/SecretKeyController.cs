using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Office_Seat_Book_Entity;
using Office_SecretKey_Book_BLL.Services;
using System.Collections.Generic;

namespace OfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretKeyController : ControllerBase
    {
        private SecretKeyService _SecretKeyService;
        public SecretKeyController(SecretKeyService secretKeyService)
        {
            _SecretKeyService = secretKeyService;
        }
        [HttpGet("GetSecretKeys")]
        public IEnumerable<SecretKey> GetSecretKeys()
        {
            return _SecretKeyService.GetSecretKey();
        }
        [HttpGet("GetSecretKeyById")]
        public SecretKey GetSecretKeyById(int secretKeyId)
        {
            return _SecretKeyService.GetBySecretKeyId(secretKeyId);
        }
        [HttpPost("AddSecretKey")]
        public IActionResult AddSecretKey([FromBody] SecretKey secretKey)
        {
            _SecretKeyService.AddSecretKey(secretKey);
            return Ok("SecretKey crecretKeyed successfully!!");

        }
        [HttpDelete("DeleteSecretKey")]
        public IActionResult DeleteSecretKey(int secretKeyId)
        {
            _SecretKeyService.DeleteSecretKey(secretKeyId);
            return Ok("SecretKey deleted successfully!!");
        }
        [HttpPut("UpdateSecretKey")]
        public IActionResult UpdateSecretKey([FromBody] SecretKey secretKey)
        {
            _SecretKeyService.UpdateSecretKey(secretKey);
            return Ok("SecretKey updated successfully!!");
        }
        [HttpGet("GetSecretKeyByEmpId")]
        public SecretKey GetSecretKeyByEmpId(int empId)
        {
            return _SecretKeyService.GetBySecretKeyId(empId);
        }
    }
}
