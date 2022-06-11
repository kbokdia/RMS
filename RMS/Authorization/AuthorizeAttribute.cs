using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RMS.Authorization
{
   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
   public class AuthorizeAttribute : Attribute, IAuthorizationFilter
   {
      private readonly IList<UserType> _roles;

      public AuthorizeAttribute(params UserType[] roles)
      {
         _roles = roles ?? new UserType[] { };
      }
      public void OnAuthorization(AuthorizationFilterContext context)
      {
         // skip authorization if action is decorated with [AllowAnonymous] attribute
         var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
         if (allowAnonymous)
            return;

         // authorization
         var user = (UserModel)context.HttpContext.Items["User"];
         if (user == null || (_roles.Any() && !_roles.Contains(user.Type)))
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
      }
   }
}
