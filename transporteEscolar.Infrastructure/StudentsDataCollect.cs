using transporteEscolar.Domain;
using MySql.Data;
using MySql.Data.MySqlClient;

public class StudentsData : DataInterface<Student>
{
    public string connection = "host=localhost;user=root;database=transporteEscolar;password=reynaldo066512";
    public async Task Add(Student element)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"INSERT INTO Student(name, address, telefono, email, debt, WayId) VALUES('{element.name}', '{element.address}', '{element.telefono}', '{element.email}', 0, {element.WayId});", connection1);
        command.ExecuteNonQuery();
        command.CommandText = "SELECT LAST_INSERT_ID();";
        int lastStudent = Convert.ToInt32(await command.ExecuteScalarAsync());
        await connection1.CloseAsync();
        BillsData bill = new BillsData();
        await bill.Add(lastStudent);
    }

    public async Task<IEnumerable<Student>> All()
    {
        List<Student> students = new List<Student>();
        MySqlConnection connector = new MySqlConnection(connection);
        await connector.OpenAsync();
        MySqlCommand command = new MySqlCommand("select * from Student", connector);
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
            var debt = 0f;
            float.TryParse(reader["debt"].ToString(), out debt);
            student.debt = debt;
            var WayId = 0;
            Int32.TryParse(reader["WayId"].ToString(), out WayId);
            student.WayId = WayId;
            
            students.Add(student);
        }
        await connector.CloseAsync();

        return students;
    }

    public async Task Delete(int id)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        BillsData bill = new BillsData();
        await bill.Delete(id);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"Delete from Student where id = {id};", connection1);
        command.ExecuteNonQuery();
        await connection1.CloseAsync();
        
    }

    public async Task<Student> Get(int id)
    {
        Student student = new Student();
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"select * from Student where id = {id};", connection1);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            student.id = id;
            student.name = reader["name"].ToString();
            student.address = reader["address"].ToString();
            student.telefono = reader["telefono"].ToString();
            student.email = reader["email"].ToString();
            var WayId = 0;
            Int32.TryParse(reader["WayId"].ToString(), out WayId);
            student.WayId = WayId;
            var debt = 0f;
            float.TryParse(reader["debt"].ToString(), out debt);
            student.debt = debt;
        }
        await connection1.CloseAsync();
        return student;
    }

    public async Task Update(Student element)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"UPDATE Student SET name = '{element.name}', address = '{element.address}', telefono = '{element.telefono}', email = '{element.email}', debt = {element.debt}, WayId = {element.WayId} WHERE id = {element.id};", connection1);
        command.ExecuteNonQuery();
        await connection1.CloseAsync();
    }
    
}
