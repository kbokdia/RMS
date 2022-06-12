using MediatR;
using RMS.Authorization;
using RMS.Data;
using RMS.Handlers.UserHandler;
using RMS.Models;
using RMS.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RMS.Handlers.OrderHandler
{
   public class Create : IRequestHandler<Create.CreateOrderRequest, Create.Response>
   {
      private readonly RMSContext ctx;
      private readonly IUserService userService;
      private readonly IJwtUtils jwtUtils;

      public Create(RMSContext ctx, IUserService userService, IJwtUtils jwtUtils)
      {
         this.ctx = ctx;
         this.userService = userService;
         this.jwtUtils = jwtUtils;
      }
      public class CreateOrderRequest : IRequest<Response>
      {
         public CreateOrderModel Model { get; set; }
      }

      public class Response
      {
         public string Message { get; set; }
         public object Data { get; set; }
      }

      public async Task<Response> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
      {
         var userModel = await userService.GetOrCreateModel(request.Model.Mobile);
         request.Model.UserId = userModel.Id;

         var entity = CreateOrderModel.ToEntity(request.Model);
         entity.OrderDatetime = DateTime.Now;
         ctx.Orders.Add(entity);
         await ctx.SaveChangesAsync();

         var responseData = new Authenticate.ResponseData
         {
            Token = jwtUtils.GenerateToken(userModel),
            User = userModel
         };

         return new Response
         {
            Message = "Order saved successfully.",
            Data = new
            {
               OrderId = entity.Id,
               UserData = responseData
            }
         };
      }
   }
}
