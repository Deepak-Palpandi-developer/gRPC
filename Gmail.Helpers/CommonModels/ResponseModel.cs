namespace Gmail.Helpers.CommonModels;

public class ResponseModel<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public T? Data { get; set; }
}
