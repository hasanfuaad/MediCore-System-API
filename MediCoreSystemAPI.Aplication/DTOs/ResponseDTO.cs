namespace Application.DTOs;

public class ResponseDTO<T>
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = "Success";
    public T? Data { get; set; }
}