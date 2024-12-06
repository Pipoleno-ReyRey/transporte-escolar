using transporteEscolar.Domain;

public class Students_Services : DataInterface<StudentDto>
{
    public Result Add(StudentDto element)
    {
        if (string.IsNullOrWhiteSpace(element.name) || 
            string.IsNullOrWhiteSpace(element.address) ||
            string.IsNullOrWhiteSpace(element.telefono) ||
            string.IsNullOrWhiteSpace(element.email) ||
            element.WayId <= 0)
        {
            return new Result(false, "Todos los campos deben ser completados correctamente.");
        }
        else{
            Student student = new Student();
            student.name = element.name;
            student.address = element.address;
            student.telefono = element.telefono;
            student.email = element.email;
            student.WayId = element.WayId;
            
            StudentsData studentsData = new StudentsData();
            studentsData.Add(student);
            return new Result(true, "agregado correctamente");

        }
    }

    public IEnumerable<StudentDto> All()
    {
        List<StudentDto> students = new List<StudentDto>();
        StudentsData studentsData = new StudentsData();
        foreach(Student student in studentsData.All()){
            StudentDto studentDto = new StudentDto();
            studentDto.name = student.name;
            studentDto.address = student.address;
            studentDto.email = student.email;
            studentDto.telefono = student.telefono;
            studentDto.WayId = student.WayId;
            students.Add(studentDto);
        }

        return students;
    }

    public Result Delete(int id)
    {
        StudentsData studentsData = new StudentsData();
        if(studentsData.All().Any(s => s.id != id)){
            return new Result(false, "ese id no existe");
        }
        else{
            studentsData.Delete(id);
            return new Result(true, "eliminado correctamente");
        }
    }

    public StudentDto Get(int id)
    {
        StudentDto studentDto = new StudentDto();
        StudentsData studentsData = new StudentsData();
        if(studentsData.All().Any(s => s.id != id)){
            studentDto.name = "no existe";
            studentDto.address = "no existe";
            studentDto.telefono = "no existe";
            studentDto.email = "no existe";
            studentDto.WayId = 0;
        }
        else{
            var student = studentsData.Get(id);
            studentDto.name = student.name;
            studentDto.address = student.address;
            studentDto.telefono = student.telefono;
            studentDto.email = student.email;
            studentDto.WayId = student.WayId;
           
        }
         return studentDto;
    }

    public Result Update(StudentDto element)
    {
        if (string.IsNullOrWhiteSpace(element.name) || 
            string.IsNullOrWhiteSpace(element.address) ||
            string.IsNullOrWhiteSpace(element.telefono) ||
            string.IsNullOrWhiteSpace(element.email) ||
            element.WayId <= 0)
        {
            return new Result(false, "Todos los campos deben ser completados correctamente.");
        }
        else{
            Student student = new Student();
            student.name = element.name;
            student.address = element.address;
            student.telefono = element.telefono;
            student.email = element.email;
            student.WayId = element.WayId;
            
            StudentsData studentsData = new StudentsData();
            studentsData.Update(student);
            return new Result(true, "estudiante actualizado");
        }
    }
}
