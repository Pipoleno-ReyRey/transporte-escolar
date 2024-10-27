using transporteEscolar.Domain;
string connection = "server=localhost;user=root;database=transporteEscolar;password=xxxxxxx";

public class StudentsData : GetStudentsInterface
{
    public void AddStudent(Student student)
    {
        
    }

    public IEnumerable<Student> AllStudents()
    {
        throw new NotImplementedException();
    }

    public void DeleteStudent(int id)
    {
        throw new NotImplementedException();
    }

    public Student GetStudent(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateStudent(Student student)
    {
        throw new NotImplementedException();
    }
}
