using Microsoft.AspNetCore.Mvc;

namespace StudentsControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly Students_Services _studentsService;

        public StudentController(Students_Services studentsService)
        {
            _studentsService = studentsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _studentsService.All();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los estudiantes", detail = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            try
            {
                var student = await _studentsService.Get(id);
                if (student.name == "no existe") 
                {
                    return NotFound(new { message = "Estudiante no encontrado" });
                }

                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el estudiante", detail = ex.Message });
            }
        }

    
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentDto studentDto)
        {
            try
            {
                
                var result = await _studentsService.Add(studentDto);
                if(!result.success){
                    return BadRequest(new{message = result.message});
                }else{
                    return StatusCode(200, new{message = result.message});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al agregar el estudiante", detail = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentDto studentDto)
        {
            if (studentDto == null)
            {
                return BadRequest(new { message = "El cuerpo de la solicitud no puede estar vacío." });
            }

            try
            { 
                var result = await _studentsService.Update(studentDto, id);

                if(!result.success){
                    return BadRequest(new{message = result.message});
                }else{
                    return StatusCode(200, new{message = result.message});
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el estudiante", detail = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var result = await _studentsService.Delete(id);

                if(!result.success){
                    return BadRequest(new{message = result.message});
                }else{
                    return StatusCode(200, new{message = result.message});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el estudiante", detail = ex.Message });
            }
        }
    }
}
