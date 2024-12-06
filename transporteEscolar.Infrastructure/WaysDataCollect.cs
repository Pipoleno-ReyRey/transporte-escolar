using transporteEscolar.Domain;
using MySql.Data;
using MySql.Data.MySqlClient;

public class WaysData : DataInterface<Ways>
{
    public string connection = "server=localhost;user=root;database=transporteEscolar;password=reynaldo066512";
    public void Add(Ways element)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"INSERT INTO Ways (origin, destiny, time) VALUES('{element.origin}', '{element.destiny}', '{element.time}');", connection1);
        command.ExecuteNonQueryAsync();
        connection1.CloseAsync();
    }

    public IEnumerable<Ways> All()
    {
        List<Ways> ways = new List<Ways>();
        MySqlConnection connector = new MySqlConnection(connection);
        connector.OpenAsync();
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
            ways.Add(way);
        }
        connector.CloseAsync();

        return ways;
    }

    public void Delete(int id)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"Delete from Ways where id = {id};", connection1);
        command.ExecuteNonQueryAsync();
        connection1.CloseAsync();
    }

    public Ways Get(int id)
    {
        Ways way = new Ways();
        MySqlConnection connection1 = new MySqlConnection(connection);
        connection1.OpenAsync();
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
        }
        connection1.CloseAsync();
        return way;
    }

    public void Update(Ways element)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"UPDATE Ways SET origin = '{element.origin}', destiny = '{element.destiny}', time = '{element.time}' WHERE id = {element.id};", connection1);
        command.ExecuteNonQueryAsync();
        connection1.CloseAsync();
    }

}