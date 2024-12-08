using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayController : ControllerBase
    {
        private readonly Pays_Services _payService;

        public PayController(Pays_Services payService)
        {
            _payService = payService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddPay([FromBody] PayDto payDto)
        {
            try
            {
                var result = await _payService.Add(payDto);
                if(!result.success){
                    return BadRequest(new{message = result.message});
                }else{
                    return StatusCode(200, new{message = result.message});
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al agregar el pago", detail = ex.Message });
            }
        }

        [HttpGet("GetByStudent/{studentId}")]
        public async Task<IActionResult> GetPaysByStudent(int studentId)
        {
            try
            {
                var pays = await _payService.All(studentId);
                return Ok(pays);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los pagos", detail = ex.Message });
            }
        }
    }
}