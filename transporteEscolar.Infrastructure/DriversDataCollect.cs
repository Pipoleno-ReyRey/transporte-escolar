using transporteEscolar.Domain;
using MySql.Data;
using MySql.Data.MySqlClient;

public class DriverData : DataInterface<Driver>
{
    public string connection = "server=localhost;user=root;database=transporteEscolar;password=reynaldo066512";
    public async Task Add(Driver element)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"INSERT INTO Driver (name, salary, license, vehicle) VALUES('{element.name}', {element.salary}, '{element.license}', '{element.vehicle}');", connection1);
        command.ExecuteNonQuery();
        await connection1.CloseAsync();
    }

    public async Task<IEnumerable<Driver>> All()
    {
        List<Driver> drivers = new List<Driver>();
        MySqlConnection connector = new MySqlConnection(connection);
        await connector.OpenAsync();
        MySqlCommand command = new MySqlCommand("select * from Driver", connector);
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
            driver.license = reader["license"].ToString();
            driver.vehicle = reader["vehicle"].ToString();
            
            drivers.Add(driver);
        }
        await connector.CloseAsync();

        return drivers;
    }

    public async Task Delete(int id)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"Delete from Driver where id = {id};", connection1);
        command.ExecuteNonQuery();
        await connection1.CloseAsync();
    }

    public async Task<Driver> Get(int id)
    {
        Driver driver = new Driver();
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"select * from Driver where id = {id};", connection1);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            driver.id = id;
            driver.name = reader["name"].ToString();
            float salary = 0.0f;
            float.TryParse(reader["salary"].ToString(), out salary);
            driver.salary = salary;
            driver.license = reader["license"].ToString();
            driver.vehicle = reader["vehicle"].ToString();
        }
        await connection1.CloseAsync();
        return driver;
    }

    public async Task Update(Driver element)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"UPDATE Driver SET name = '{element.name}', salary = {element.salary}, license = '{element.license}', vehicle = '{element.vehicle}' WHERE id = {element.id};", connection1);
        command.ExecuteNonQuery();
        await connection1.CloseAsync();
    }

    public async Task AddDriverWay(int idWay)
    {
        int ultimoId = 0;
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"SELECT MAX(id) AS ultimo_id FROM Driver;", connection1);
        MySqlDataReader reader = command.ExecuteReader();
        while(reader.Read()){
            ultimoId = int.Parse(reader["ultimo_id"].ToString());
        }
        await connection1.CloseAsync();
        
        await connection1.OpenAsync();
        command = new MySqlCommand($"insert into Drivers_Ways(idWay, idDriver) values({idWay}, {ultimoId});", connection1);
        command.ExecuteNonQuery();
        await connection1.CloseAsync();
    }
}