using transporteEscolar.Domain;


public interface GetStudentsInterface
{
    Student GetStudent(int id);
    IEnumerable<Student> AllStudents();
    void AddStudent(Student student);
    void DeleteStudent(int id);
    void UpdateStudent(Student student);

}

