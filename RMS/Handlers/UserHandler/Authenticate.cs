using MediatR;
using Microsoft.EntityFrameworkCore;
using RMS.Authorization;
using RMS.Data;
using RMS.Exceptions;
using RMS.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace RMS.Handlers.UserHandler
{
   public class Authenticate : IRequestHandler<Authenticate.AuthenticateUserRequest, Authenticate.Response>
   {
      private readonly RMSContext ctx;
      private readonly IJwtUtils jwtUtils;

      public Authenticate(RMSContext ctx, IJwtUtils jwtUtils)
      {
         this.ctx = ctx;
         this.jwtUtils = jwtUtils;
      }
      public class AuthenticateUserRequest : IRequest<Response>
      {
         [Required]
         public string Mobile { get; set; }

         [Required]
         public string Password { get; set; }
      }

      public class Response
      {
         public string Message { get; set; }
         public ResponseData Data { get; set; }
      }

      public class ResponseData
      {
         public string Token { get; set; }
         public UserModel User { get; set; }
      }

      public async Task<Response> Handle(AuthenticateUserRequest request, CancellationToken cancellationToken)
      {
         var user = await ctx.Users
            .AsNoTracking()
            .Where(u => u.Mobile == request.Mobile)
            .SingleOrDefaultAsync();

         if (user == null || !BCryptNet.Verify(request.Password, user.Password))
            throw new NotFoundException($"Username or password is incorrect");

         var userModel = UserModel.ToModel<UserModel>(user);

         var responseData = new ResponseData
         {
            Token = jwtUtils.GenerateToken(userModel),
            User = userModel
         };

         return new Response
         {
            Message = "Successful",
            Data = responseData

         };
      }

   }
}
