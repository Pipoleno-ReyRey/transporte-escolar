using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class WayController : ControllerBase
{
    private readonly Ways_Services _waysService;

    public WayController(Ways_Services waysService)
    {
        _waysService = waysService;
    }

    [HttpPost]
    public async Task<IActionResult> AddWay([FromBody] WayDto wayDto)
    {
        try
        {
            var result = await _waysService.Add(wayDto);
            if (!result.success)
            {
                return BadRequest(new { message = result.message });
            }
            else
            {
                return StatusCode(200, new { message = result.message });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al agregar la ruta", detail = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWays()
    {
        try
        {
            var ways = await _waysService.All();
            return Ok(ways);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener las rutas", detail = ex.Message });
        }
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteWay(int id)
    {
        try
        {
            var result = await _waysService.Delete(id);
            if (!result.success)
            {
                return BadRequest(new { message = result.message });
            }
            else
            {
                return StatusCode(200, new { message = result.message });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al eliminar la ruta", detail = ex.Message });
        }
    }

    [HttpGet("Get/{id}")]
    public async Task<IActionResult> GetWay(int id)
    {
        try
        {
            var way = await _waysService.Get(id);
            if (way.origin == "no existe")
            {
                return NotFound(new { message = "La ruta no existe" });
            }

            return Ok(way);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener la ruta", detail = ex.Message });
        }
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateWay([FromBody] WayDto wayDto, int id)
    {
        try
        {
            var result = await _waysService.Update(wayDto, id);
            if (!result.success)
            {
                return BadRequest(new { message = result.message });
            }
            else
            {
                return StatusCode(200, new { message = result.message });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al actualizar la ruta", detail = ex.Message });
        }
    }

    [HttpGet("/DriverWays/{driverId}")]
    public async Task<IActionResult> GetDriverWays(int driverId)
    {
        try
        {
            var ways = await _waysService.GetDriverWayService(driverId);
            return Ok(ways);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener las rutas", detail = ex.Message });
        }
    }
}

}