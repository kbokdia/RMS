using MediatR;
using Microsoft.EntityFrameworkCore;
using RMS.Data;
using RMS.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RMS.Handlers.OrderHandler
{
   public class GetAll : IRequestHandler<GetAll.GetAllOrderRequest, GetAll.Response>
   {
      private readonly RMSContext ctx;

      public GetAll(RMSContext ctx)
      {
         this.ctx = ctx;
      }
      public class GetAllOrderRequest : IRequest<Response>
      {
         public string Name { get; set; } = null;
         public string Mobile { get; set; } = null;
         public OrderStatus? Status { get; set; } = null;
      }

      public class Response
      {
         public string Message { get; set; }
         public int Count { get; set; }
         public object Data { get; set; }
      }

      public async Task<Response> Handle(GetAllOrderRequest request, CancellationToken cancellationToken)
      {
         var query = ctx.Orders
            .Include(i => i.OrderItems).ThenInclude(i => i.Menu)
            .Include(i => i.Table)
            .Include(i => i.User)
            .AsNoTracking();

         if (request.Mobile != null)
            query = query
               .Where(i => i.User != null)
               .Where(i => i.User.Mobile == request.Mobile);

         if (request.Status != null)
            query = query
               .Where(i => i.Status == (byte)request.Status.Value);

         var entities = await query
            .OrderByDescending(o => o.OrderDatetime)
            .ToListAsync();

         var models = entities.Select(GetOrderModel.ToModel).ToList();

         return new Response
         {
            Message = "All OK.",
            Count = models.Count,
            Data = models
         };
      }
   }
}
