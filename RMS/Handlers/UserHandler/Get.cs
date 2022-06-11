using MediatR;
using Microsoft.EntityFrameworkCore;
using RMS.Data;
using RMS.Exceptions;
using RMS.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RMS.Handlers.UserHandler
{
   public class Get : IRequestHandler<Get.GetUserRequest, Get.Response>
   {
      private readonly RMSContext ctx;

      public Get(RMSContext ctx)
      {
         this.ctx = ctx;
      }
      public class GetUserRequest : IRequest<Response>
      {
         public int UserId { get; set; }
      }

      public class Response
      {
         public string Message { get; set; }
         public UserModel Data { get; set; }
      }

      public async Task<Response> Handle(GetUserRequest request, CancellationToken cancellationToken)
      {
         if (request.UserId < 1)
            throw new BadRequestException($"Invalid user id {request.UserId}");

         var user = await ctx.Users
            .AsNoTracking()
            .Where(u => u.Id == request.UserId)
            .SingleOrDefaultAsync();

         if (user == null)
            throw new NotFoundException($"No user found with id {request.UserId}");

         var userModel = UserModel.ToModel<UserModel>(user);

         return new Response
         {
            Message = "Successful",
            Data = userModel
         };
      }

   }
}
