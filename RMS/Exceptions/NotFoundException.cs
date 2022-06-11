using System.Net;

namespace RMS.Exceptions
{
   public class NotFoundException: HttpResponseException
   {
      public NotFoundException(string s = null)
       : base(HttpStatusCode.NotFound, s)
      {
      }

   }
}
