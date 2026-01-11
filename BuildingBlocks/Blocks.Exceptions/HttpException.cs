using System.Net;

namespace Blocks.Exceptions
{
    public class HttpException : Exception
    {
        public HttpException(HttpStatusCode statusCode,string message)
            : base(string.IsNullOrWhiteSpace(message) ? statusCode.ToString() : message)
        {
            StatusCode = statusCode;
        }

        public HttpException(HttpStatusCode statusCode, string message, Exception ex)
            : base(message, ex)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
