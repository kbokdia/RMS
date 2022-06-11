using MediatR;
using RMS.Data;
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

      public Create(RMSContext ctx, IUserService userService)
      {
         this.ctx = ctx;
         this.userService = userService;
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
         request.Model.UserId = await userService.GetOrCreate(request.Model.Mobile);

         var entity = CreateOrderModel.ToEntity(request.Model);
         entity.OrderDatetime = DateTime.Now;
         ctx.Orders.Add(entity);
         await ctx.SaveChangesAsync();

         return new Response
         {
            Message = "Order saved successfully.",
            Data = new
            {
               OrderId = entity.Id,
            }
         };
      }
   }
}
