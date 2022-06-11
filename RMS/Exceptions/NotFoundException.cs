using RMS.Exceptions;
using System.Net;

namespace CmacApi.Exceptions
{
   public class NotFoundException: HttpResponseException
   {
      public NotFoundException(string s = null)
       : base(HttpStatusCode.NotFound, s)
      {
      }

   }
}
