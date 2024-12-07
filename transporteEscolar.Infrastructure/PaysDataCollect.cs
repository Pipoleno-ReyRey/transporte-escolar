using transporteEscolar.Domain;
using MySql.Data;
using MySql.Data.MySqlClient;

public class PayData : DataInterface<Pays>
{
    public string connection = "server=localhost;user=root;database=transporteEscolar;password=reynaldo066512";
    public async Task Add(Pays element)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"INSERT INTO Pays (studentId, count, datePay) VALUES({element.studentId}, {element.count}, NOW());", connection1);
        command.ExecuteNonQuery();
        await connection1.CloseAsync();
    }

    public async Task<IEnumerable<Pays>> All()
    {
        List<Pays> pays = new List<Pays>();
        MySqlConnection connector = new MySqlConnection(connection);
        await connector.OpenAsync();
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
            pay.datePay = reader.GetDateTime("datePay");           
            pays.Add(pay);
        }
        await connector.CloseAsync();

        return pays;
    }

    public async Task Delete(int id)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"Delete from Pays where id = {id};", connection1);
        command.ExecuteNonQuery();
        await connection1.CloseAsync();
    }

    public async Task<Pays> Get(int id)
    {
        Pays pay = new Pays();
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
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
        }
        await connection1.CloseAsync();
        return pay;
    }

    public async Task Update(Pays element)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"UPDATE Pays SET studentId = {element.studentId}, count = {element.count}, date = NOW() WHERE id = {element.id};", connection1);
        command.ExecuteNonQuery();
        await connection1.CloseAsync();
    }

}