using System;
using System.Net;

namespace RMS.Exceptions
{
   public class HttpResponseException : Exception
   {
      public HttpStatusCode StatusCode { get; set; }

      public HttpResponseException(HttpStatusCode statusCode, string s = null)
         : base(s)
      {
         StatusCode = statusCode;
      }
   }
}