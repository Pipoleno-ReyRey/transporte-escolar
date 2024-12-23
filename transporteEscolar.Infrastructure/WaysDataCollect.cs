using transporteEscolar.Domain;
using MySql.Data;
using MySql.Data.MySqlClient;

public class WaysData : DataInterface<Ways>
{
    public string connection = "server=localhost;user=root;database=transporteEscolar;password=reynaldo066512";
    public async Task Add(Ways element)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        string? time = TimeOnly.Parse(element.time.ToString()).ToString("HH:mm:ss");
        MySqlCommand command = new MySqlCommand($"INSERT INTO Ways (origin, destiny, time, cost) VALUES('{element.origin}', '{element.destiny}', '{time}', {element.cost});", connection1);
        command.ExecuteNonQuery();
        await connection1.CloseAsync();
    }

    public async Task<IEnumerable<Ways>> All()
    {
        List<Ways> ways = new List<Ways>();
        MySqlConnection connector = new MySqlConnection(connection);
        await connector.OpenAsync();
        MySqlCommand command = new MySqlCommand("select * from Ways", connector);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Ways way = new Ways();
            int id;
            Int32.TryParse(reader["id"].ToString(), out id);
            way.id = id;
            way.origin = reader["origin"].ToString();
            way.destiny = reader["destiny"].ToString();
            TimeOnly time = new TimeOnly();
            TimeOnly.TryParse(reader["time"].ToString(), out time);
            way.time = time;
            float cost;
            float.TryParse(reader["cost"].ToString(), out cost);
            way.cost = cost;
            ways.Add(way);
        }
        await connector.CloseAsync();

        return ways;
    }

    public async Task Delete(int id)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"Delete from Ways where id = {id};", connection1);
        command.ExecuteNonQuery();
        await connection1.CloseAsync();
    }

    public async Task<Ways> Get(int id)
    {
        Ways way = new Ways();
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"select * from Pays where id = {id};", connection1);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            way.id = id;
            way.origin = reader["origin"].ToString();
            way.destiny = reader["destiny"].ToString();
            TimeOnly time = new TimeOnly();
            TimeOnly.TryParse(reader["time"].ToString(), out time);
            way.time = time;
            float cost;
            float.TryParse(reader["cost"].ToString(), out cost);
            way.cost = cost;
        }
        await connection1.CloseAsync();
        return way;
    }

    public async Task Update(Ways element)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        string? time = TimeOnly.Parse(element.time.ToString()).ToString("HH:mm:ss");
        MySqlCommand command = new MySqlCommand($"UPDATE Ways SET origin = '{element.origin}', destiny = '{element.destiny}', time = '{time}', cost = {element.cost} WHERE id = {element.id};", connection1);
        command.ExecuteNonQuery();
        await connection1.CloseAsync();
    }

    public async Task<IEnumerable<Ways>> GetDriverWays(int id)
    {
        List<Ways> ways = new List<Ways>();
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"SELECT Ways.id AS WayId, Ways.origin AS Origin, Ways.destiny AS Destiny, Ways.cost AS Cost, Ways.time AS Time FROM Drivers_Ways INNER JOIN Ways ON Drivers_Ways.idWay = Ways.id WHERE Drivers_Ways.idDriver = {id};", connection1);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Ways way = new Ways();
            way.id = int.Parse(reader["WayId"].ToString());
            way.origin = reader["Origin"].ToString();
            way.destiny = reader["Destiny"].ToString();
            float cost = 0.0f;
            float.TryParse(reader["Cost"].ToString(), out cost);
            way.cost = cost;
            way.time = TimeOnly.Parse(reader["Time"].ToString());
            ways.Add(way);
        }
        await connection1.CloseAsync();
        return ways;
    }
}