using Microsoft.AspNetCore.Mvc;

namespace RMS.Controllers
{
   [ApiController]
   public class RmsControllerBase : ControllerBase
   {
      //protected readonly IUserService userService;

      public RmsControllerBase()//IUserService userService)
      {
         //this.userService = userService;
      }

      //protected UserModel GetLoggedInUser()
      //{
      //   var currentUser = (UserModel)HttpContext.Items["User"];
      //   if (currentUser == null || currentUser.Id == null)
      //      throw new UnauthorizedException("Unauthorized");
      //   return currentUser;
      //}
   }
}
