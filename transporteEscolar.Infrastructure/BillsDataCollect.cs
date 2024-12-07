using transporteEscolar.Domain;
using MySql.Data;
using MySql.Data.MySqlClient;

public class BillsData : BillsInterface<Bills>
{
    public string connection = "server=localhost;user=root;database=transporteEscolar;password=reynaldo066512";

    public async Task Add(int studentId)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"INSERT INTO Bills(count, studentId, state) values(0, {studentId}, true);", connection1);
        command.ExecuteNonQuery();
        await connection1.CloseAsync();    
    }

    public async Task Update(Bills bill)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"UPDATE Bills SET count = {bill.count}, state = {bill.state} WHERE studentId ={bill.id};", connection1);
        command.ExecuteNonQuery();
        await connection1.CloseAsync();
    }

    public async Task<Bills> Get(int id)
    {
        Bills bill = new Bills();
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"select * from Bills where studentId = {id};", connection1);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            
            var count = 0f;
            float.TryParse(reader["count"].ToString(), out count);
            bill.count = count;
            var studentId = 0;
            Int32.TryParse(reader["studentId"].ToString(), out studentId);
            bill.studentId = studentId;
            var state = true;
            bool.TryParse(reader["state"].ToString(), out state);
            bill.state = state;
        }
        await connection1.CloseAsync();
        return bill;
    }

    public async Task Delete(int StudentId)
    {
        MySqlConnection connection1 = new MySqlConnection(connection);
        await connection1.OpenAsync();
        MySqlCommand command = new MySqlCommand($"Delete from Bills where studentId = {StudentId};", connection1);
        command.ExecuteNonQuery();
        await connection1.CloseAsync();
    }
}