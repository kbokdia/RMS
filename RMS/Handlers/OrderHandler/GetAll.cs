using MediatR;
using Microsoft.EntityFrameworkCore;
using RMS.Data;
using RMS.Models;
using System.Threading;
using System.Threading.Tasks;
using RMS.Exceptions;
using System.Linq;

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
      }

      public class Response
      {
         public string Message { get; set; }
         public int Count { get; set; }
         public object Data { get; set; }
      }

      public async Task<Response> Handle(GetAllOrderRequest request, CancellationToken cancellationToken)
      {
         var entities = await ctx.Orders
            .Include(i => i.OrderItems).ThenInclude(i => i.Menu)
            .Include(i => i.Table)
            .Include(i => i.User)
            .AsNoTracking()
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
