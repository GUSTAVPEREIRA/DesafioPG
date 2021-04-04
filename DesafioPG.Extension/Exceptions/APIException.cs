using System;
using System.Net;

namespace DesafioPG.Extensions.Exceptions
{
    public class APIException : Exception
    {
        private readonly HttpStatusCode statusCode;

        public APIException(string message, HttpStatusCode statusCode) : base(message)
        {
            this.statusCode = statusCode;
        }
        
        public APIException(string message, HttpStatusCode statusCode, Exception innerException) : base(message, innerException)
        {
            this.statusCode = statusCode;
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return this.statusCode;
            }
        }
    }
}
