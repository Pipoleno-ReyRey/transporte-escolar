using transporteEscolar.Domain;
using MySql.Data;
using MySql.Data.MySqlClient;

public class PayData : DataInterface<Pays>
{
    public string connection = "server=localhost;user=root;database=transporteEscolar;password=reynaldo066512";
    public void Add(Pays element)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"INSERT INTO Pays (studentId, count, date, state) VALUES({element.studentId}, {element.count}, '{element.datePay}', {element.state});", connection1);
        command.ExecuteNonQueryAsync();
        connection1.CloseAsync();
    }

    public IEnumerable<Pays> All()
    {
        List<Pays> pays = new List<Pays>();
        MySqlConnection connector = new MySqlConnection(connection);
        connector.OpenAsync();
        MySqlCommand command = new MySqlCommand("select * from Pays", connector);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Pays pay = new Pays();
            int id;;
            Int32.TryParse(reader["id"].ToString(), out id);
            pay.id = id;
            int studentId;;
            Int32.TryParse(reader["studentId"].ToString(), out studentId);
            pay.studentId = studentId;
            float count;;
            float.TryParse(reader["count"].ToString(), out count);
            pay.count = count;
            pay.datePay = reader.GetDateTime("date");
            bool state;
            bool.TryParse(reader["state"].ToString(), out state);
            pay.state = state;            
            pays.Add(pay);
        }
        connector.CloseAsync();

        return pays;
    }

    public void Delete(int id)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"Delete from Pays where id = {id};", connection1);
        command.ExecuteNonQueryAsync();
        connection1.CloseAsync();
    }

    public Pays Get(int id)
    {
        Pays pay = new Pays();
        MySqlConnection connection1 = new MySqlConnection(connection);
        connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"select * from Pays where id = {id};", connection1);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            pay.id = id;
            int studentId = 0;
            Int32.TryParse(reader["studentId"].ToString(), out studentId);
            pay.studentId = studentId;
            float count = 0.0f;
            float.TryParse(reader["count"].ToString(), out count);
            pay.count = count;
            pay.datePay = reader.GetDateTime("date");
            bool state;
            bool.TryParse(reader["state"].ToString(), out state);
            pay.state = state;
        }
        connection1.CloseAsync();
        return pay;
    }

    public void Update(Pays element)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"UPDATE Pays SET studentId = {element.studentId}, count = {element.count}, date = '{element.datePay}', state = {element.state} WHERE id = {element.id};", connection1);
        command.ExecuteNonQueryAsync();
        connection1.CloseAsync();
    }

}