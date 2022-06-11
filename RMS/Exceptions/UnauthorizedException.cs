using RMS.Exceptions;
using System.Net;

namespace CmacApi.Exceptions
{
   public class UnauthorizedException : HttpResponseException
   {
      public UnauthorizedException(string s = null)
      : base(HttpStatusCode.Unauthorized, s)
      {
      }
   }
}
