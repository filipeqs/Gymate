namespace Exercises.Api.Errors
{
    public class ErrorDetails
    {
        public ErrorDetails(int statusCode)
        {
            StatusCode = statusCode;
        }

        public ErrorDetails(int statusCode, string message, string details)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
