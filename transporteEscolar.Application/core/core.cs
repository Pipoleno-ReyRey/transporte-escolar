
public class ResultServices{
    public bool success { get; set; }
    public string message { get; set; }

    public ResultServices(bool success, string message){
        this.message = message;
        this.success = success;
    }
}