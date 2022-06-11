using MediatR;
using Microsoft.EntityFrameworkCore;
using RMS.Data;
using RMS.Exceptions;
using RMS.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RMS.Handlers.OrderHandler
{
   public class UpdateStatus : IRequestHandler<UpdateStatus.UpdateOrderStatus, UpdateStatus.Response>
   {
      private readonly RMSContext ctx;

      public UpdateStatus(RMSContext ctx)
      {
         this.ctx = ctx;
      }
      public class UpdateOrderStatus : IRequest<Response>
      {
         public int Id { get; set; }
         public OrderStatus Status { get; set; }
      }

      public class Response
      {
         public string Message { get; set; }
         public object Data { get; set; }
      }

      public async Task<Response> Handle(UpdateOrderStatus request, CancellationToken cancellationToken)
      {
         if (request?.Id == null)
            throw new BadRequestException("Id must be present");
         
         var entity = await ctx.Orders
            .Include(i => i.OrderItems).ThenInclude(i => i.Menu)
            .Include(i => i.Table)
            .Include(i => i.User)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == request.Id);
         
         if (entity == null)
            throw new NotFoundException("Order not found for the given id");
         
         entity.Status = (byte)request.Status;
         ctx.Orders.Update(entity);
         await ctx.SaveChangesAsync();

         return new Response
         {
            Message = "Order modified successfully.",
            Data = GetOrderModel.ToModel(entity)
         };
      }
   }
}
