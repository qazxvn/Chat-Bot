namespace ChatBot.Models;

public class ApiResponseModel
{
    public int Count { get; set; }
    public string Next { get; set; }
    public string Previous { get; set; }
    public List<ApiModel> Results { get; set; }
}