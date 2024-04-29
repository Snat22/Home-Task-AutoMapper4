using System.Net;
using Domain.Enums;

namespace Domain.Responses;

public class Response<T>
{
    public int StatusCode { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; } = new List<string>();


    public Response(T data)
    {
        Data = data;
        StatusCode = 200;
    }

    public Response(T data, HttpStatusCode statusCode)
    {
        Data = data;
        StatusCode = (int)statusCode;
    }

    public Response(T data, HttpStatusCode statusCode, string error)
    {
        Data = data;
        StatusCode = (int)statusCode;
        Errors.Add(error);
    }

    public Response(HttpStatusCode statusCode, string error)
    {
        StatusCode = (int)statusCode;
        Errors.Add(error);
    }



}
