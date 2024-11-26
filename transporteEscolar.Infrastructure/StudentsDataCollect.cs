using transporteEscolar.Domain;
using MySql.Data;
using MySql.Data.MySqlClient;

public class StudentsData : GetStudentsInterface
{
    public string connection = "server=localhost;user=root;database=transporteEscolar;password=xxxxxxx";
    public void AddStudent(Student student)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"INSERT INTO Student (id, name, address, telefono, email, WayId) VALUES ('{student.name}', '{student.address}', '{student.telefono}', '{student.email}', {student.WayId});", connection1);
        command.ExecuteNonQueryAsync();
        connection1.CloseAsync();
    }

    public IEnumerable<Student> AllStudents()
    {
        List<Student> students = new List<Student>();
        MySqlConnection connector = new MySqlConnection(connection);
        connector.OpenAsync();
        MySqlCommand command = new MySqlCommand("select * from Students", connector);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Student student = new Student();
            var id = 0;
            Int32.TryParse(reader["id"].ToString(), out id);
            student.id = id;
            student.name = reader["name"].ToString();
            student.address = reader["address"].ToString();
            student.telefono = reader["telefono"].ToString();
            student.email = reader["email"].ToString();
            var WayId = 0;
            Int32.TryParse(reader["price"].ToString(), out WayId);
            student.WayId = WayId;
            
            students.Add(student);
        }
        connector.CloseAsync();

        return students;
    }

    public void DeleteStudent(int id)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"Delete from Students where id = {id};", connection1);
        command.ExecuteNonQueryAsync();
        connection1.CloseAsync();
    }

    public Student GetStudent(int id)
    {
        Student student = new Student();
        MySqlConnection connection1 = new MySqlConnection(connection);
        connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"select * from Dish where id = {id};", connection1);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            student.id = id;
            student.name = reader["name"].ToString();
            student.address = reader["address"].ToString();
            student.telefono = reader["telefono"].ToString();
            student.email = reader["email"].ToString();
            var WayId = 0;
            Int32.TryParse(reader["price"].ToString(), out WayId);
            student.WayId = WayId;
        }
        connection1.CloseAsync();
        return student;
    }

    public void UpdateStudent(Student student)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"UPDATE Student SET name = '{student.name}', address = '{student.address}', telefono = '{student.telefono}', email = '{student.email}', WayId = {student.WayId} WHERE id ={student.id};", connection1);
        command.ExecuteNonQueryAsync();
        connection1.CloseAsync();
    }
}
