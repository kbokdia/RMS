using System;

namespace RMS.Authorization
{
   [AttributeUsage(AttributeTargets.Method)]
   public class AllowAnonymousAttribute: Attribute
   {
      
   }
}
