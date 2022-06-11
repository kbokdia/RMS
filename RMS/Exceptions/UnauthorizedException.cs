using System.Net;

namespace RMS.Exceptions
{
   public class UnauthorizedException : HttpResponseException
   {
      public UnauthorizedException(string s = null)
      : base(HttpStatusCode.Unauthorized, s)
      {
      }
   }
}
