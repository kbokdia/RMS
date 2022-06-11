using MediatR;
using Microsoft.EntityFrameworkCore;
using RMS.Data;
using RMS.Exceptions;
using RMS.Models;
using System.Threading;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace RMS.Handlers.UserHandler
{
   public class Create : IRequestHandler<Create.CreateUserRequest, Create.Response>
   {
      private readonly RMSContext ctx;

      public Create(RMSContext ctx)
      {
         this.ctx = ctx;
      }
      public class CreateUserRequest : IRequest<Response>
      {
         public CreateUserModel Model { get; set; }
      }

      public class Response
      {
         public string Message { get; set; }
         public ResponseData Data { get; set; }
      }

      public class ResponseData
      {
         public int UserId { get; set; }
      }

      public async Task<Response> Handle(CreateUserRequest request, CancellationToken cancellationToken)
      {
         if (await ctx.Users.AnyAsync(x => x.Mobile == request.Model.Mobile))
            throw new BadRequestException("User with mobile '" + request.Model.Mobile + "' is already taken");

         request.Model.Password = BCryptNet.HashPassword(request.Model.Password);

         var entity = CreateUserModel.ToEntity(request.Model);
         ctx.Users.Add(entity);
         await ctx.SaveChangesAsync();

         return new Response
         {
            Message = "User saved successfully.",
            Data = new ResponseData
            {
               UserId = entity.Id,
            }
         };
      }
   }
}
