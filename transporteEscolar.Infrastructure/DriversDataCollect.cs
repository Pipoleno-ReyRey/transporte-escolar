using transporteEscolar.Domain;
using MySql.Data;
using MySql.Data.MySqlClient;

public class DriverData : GetDataInterface<Driver>
{
    public string connection = "server=localhost;user=root;database=transporteEscolar;password=xxxxxxx";
    public void Add(Driver element)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"INSERT INTO Student (id, name, salary, license, vehicleId) VALUES('{element.name}', '{element.salary}, '{element.license}', {element.vehicleId});", connection1);
        command.ExecuteNonQueryAsync();
        connection1.CloseAsync();
    }

    public IEnumerable<Driver> All()
    {
        List<Driver> drivers = new List<Driver>();
        MySqlConnection connector = new MySqlConnection(connection);
        connector.OpenAsync();
        MySqlCommand command = new MySqlCommand("select * from Drivers", connector);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Driver driver = new Driver();
            var id = 0;
            Int32.TryParse(reader["id"].ToString(), out id);
            driver.id = id;
            driver.name = reader["name"].ToString();
            float salary = 0.0f;
            float.TryParse(reader["salary"].ToString(), out salary);
            driver.salary = salary;
            driver.license = reader["telefono"].ToString();
            var vehicleId = 0;
            Int32.TryParse(reader["vehicleId"].ToString(), out vehicleId);
            driver.vehicleId = vehicleId;
            
            drivers.Add(driver);
        }
        connector.CloseAsync();

        return drivers;
    }

    public void Delete(int id)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"Delete from Drivers where id = {id};", connection1);
        command.ExecuteNonQueryAsync();
        connection1.CloseAsync();
    }

    public Driver Get(int id)
    {
        Driver driver = new Driver();
        MySqlConnection connection1 = new MySqlConnection(connection);
        connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"select * from Drivers where id = {id};", connection1);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            driver.id = id;
            driver.name = reader["name"].ToString();
            float salary = 0.0f;
            float.TryParse(reader["salary"].ToString(), out salary);
            driver.salary = salary;
            driver.license = reader["telefono"].ToString();
            var vehicleId = 0;
            Int32.TryParse(reader["vehicleId"].ToString(), out vehicleId);
            driver.vehicleId = vehicleId;
        }
        connection1.CloseAsync();
        return driver;
    }

    public void Update(Driver element)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"UPDATE Drivers SET name = '{element.name}', salary = {element.salary}, telefono = '{element.license}', vehicleId = {element.vehicleId} WHERE id = {element.id};", connection1);
        command.ExecuteNonQueryAsync();
        connection1.CloseAsync();
    }

}