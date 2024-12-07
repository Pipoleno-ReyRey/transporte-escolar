using transporteEscolar.Domain;

public class Students_Services : ServicesInterface<StudentDto>
{
    public async Task<Result> Add(StudentDto element)
    {
        if (string.IsNullOrEmpty(element.name) || 
            string.IsNullOrEmpty(element.address) ||
            string.IsNullOrEmpty(element.telefono) ||
            string.IsNullOrEmpty(element.email) ||
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
            await studentsData.Add(student);
            return new Result(true, "agregado correctamente");
        }
    }

    public async Task<IEnumerable<StudentDto>> All()
    {
        List<StudentDto> students = new List<StudentDto>();
        StudentsData studentsData = new StudentsData();
        var studentsList = await studentsData.All();
        foreach(Student student in studentsList){
            StudentDto studentDto = new StudentDto();
            studentDto.id = student.id;
            studentDto.name = student.name;
            studentDto.address = student.address;
            studentDto.email = student.email;
            studentDto.debt = student.debt;
            studentDto.telefono = student.telefono;
            studentDto.WayId = student.WayId;
            students.Add(studentDto);
        }

        return students;
    }

    public async Task<Result> Delete(int id)
    {
        StudentsData studentsData = new StudentsData();
        var studentsAll = await studentsData.All();
        bool state = true;
        string x = "";
        foreach(Student student in studentsAll){
            if(student.id == id){
                await studentsData.Delete(id);
                state = true;
                x = "estudiante eliminado correctamente";
                break;
            } else{
                state = false;
                x = "el id no existe";
            }
        }

        return new Result(state, x);
    }

    public async Task<StudentDto> Get(int id)
    {
        StudentDto studentDto = new StudentDto();
        StudentsData studentsData = new StudentsData();
        var studentsAll = await studentsData.All();
        foreach(Student student in studentsAll){
            if(student.id == id){
                studentDto.id = id;
                studentDto.name = student.name;
                studentDto.address = student.address;
                studentDto.telefono = student.telefono;
                studentDto.email = student.email;
                studentDto.debt = student.debt;
                studentDto.WayId = student.WayId;
            }
        }

        if(studentDto.name == null){
            studentDto.name = "no existe";
            studentDto.address = "no existe";
            studentDto.telefono = "no existe";
            studentDto.email = "no existe";
            studentDto.debt = 0;
            studentDto.WayId = 0;
        }

        return studentDto;
    }

    public async Task<Result> Update(StudentDto element, int id)
    {
        if (string.IsNullOrEmpty(element.name) || 
            string.IsNullOrEmpty(element.address) ||
            string.IsNullOrEmpty(element.telefono) ||
            string.IsNullOrEmpty(element.email) ||
            element.WayId <= 0)
        {
            return new Result(false, "Todos los campos deben ser completados correctamente.");
        }
        else{
            StudentsData studentsData = new StudentsData();
            var studentsAll = await studentsData.All();
            Student student = new Student();
            bool state = true;
            string message = "";
            foreach(Student student1 in studentsAll){
                if(student1.id == id){
                    student.id = id;
                    student.name = element.name;
                    student.address = element.address;
                    student.telefono = element.telefono;
                    student.email = element.email;
                    student.debt = student1.debt;
                    student.WayId = element.WayId;
                    await studentsData.Update(student);
                    state = true;
                    message = $"estudiante actualizado";
                    break;
                } else{
                    state = false;
                    message = $"el id no existe";
                }
            }    
            return new Result(state, message);  
        }
    }
}
