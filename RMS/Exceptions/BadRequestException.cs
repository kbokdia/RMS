using System.Net;

namespace RMS.Exceptions
{
   public class BadRequestException : HttpResponseException
   {
      public BadRequestException(string s = null)
      : base(HttpStatusCode.BadRequest, s)
      {
      }
   }
}
