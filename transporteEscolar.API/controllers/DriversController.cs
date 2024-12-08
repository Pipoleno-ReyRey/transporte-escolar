using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly Drivers_Services _driversService;

        public DriverController(Drivers_Services driversService)
        {
            _driversService = driversService;
        }

        [HttpPost("{wayId}")]
        public async Task<IActionResult> AddDriver([FromBody] DriverDto driverDto, int wayId)
        {
            try
            {
                var result = await _driversService.Add(driverDto);
                var result2 = await _driversService.AddDriverWayService(wayId);
                if(!result.success && !result2.success){
                    return BadRequest(new{message = result.message});
                }else{
                    return StatusCode(200, new{message = result.message});
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al agregar el conductor", detail = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            try
            {
                var drivers = await _driversService.All();
                return Ok(drivers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los conductores", detail = ex.Message });
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            try
            {
                var result = await _driversService.Delete(id);
                if(!result.success){
                    return BadRequest(new{message = result.message});
                }else{
                    return StatusCode(200, new{message = result.message});
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el conductor", detail = ex.Message });
            }
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetDriver(int id)
        {
            try
            {
                var driver = await _driversService.Get(id);
                if (driver.name == "no existe")
                {
                    return NotFound(new { message = "El conductor no existe" });
                }

                return Ok(driver);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el conductor", detail = ex.Message });
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateDriver([FromBody] DriverDto driverDto, int id)
        {
            try
            {
                var result = await _driversService.Update(driverDto, id);
                if(!result.success){
                    return BadRequest(new{message = result.message});
                }else{
                    return StatusCode(200, new{message = result.message});
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el conductor", detail = ex.Message });
            }
        }
    }
}
