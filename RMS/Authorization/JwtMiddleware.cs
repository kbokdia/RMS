using Microsoft.AspNetCore.Http;
using RMS.Services;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.Authorization
{
   public class JwtMiddleware
   {
      private readonly RequestDelegate _next;

      public JwtMiddleware(RequestDelegate next)
      {
         _next = next;
      }

      public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
      {
         var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
         var userId = jwtUtils.ValidateToken(token);
         if (userId != null)
         {
            // attach user to context on successful jwt validation
            context.Items["User"] = await userService.GetById(userId.Value);
         }

         await _next(context);
      }

   }
}
