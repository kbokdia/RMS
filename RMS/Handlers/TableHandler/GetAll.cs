using MediatR;
using Microsoft.EntityFrameworkCore;
using RMS.Data;
using RMS.Models;
using System.Threading;
using System.Threading.Tasks;
using RMS.Exceptions;
using System.Linq;

namespace RMS.Handlers.TableHandler
{
   public class GetAll : IRequestHandler<GetAll.GetAllTableRequest, GetAll.Response>
   {
      private readonly RMSContext ctx;

      public GetAll(RMSContext ctx)
      {
         this.ctx = ctx;
      }
      public class GetAllTableRequest : IRequest<Response>
      {
         public string Name { get; set; } = null;
      }

      public class Response
      {
         public string Message { get; set; }
         public int Count { get; set; }
         public object Data { get; set; }
      }

      public async Task<Response> Handle(GetAllTableRequest request, CancellationToken cancellationToken)
      {
         var rmsTableEntites = await ctx.RmsTables.AsNoTracking().ToListAsync();
         var rmsTableModels = rmsTableEntites.Select(x =>  TableModel.ToModel<TableModel>(x)).ToList();

         return new Response
         {
            Message = "All OK.",
            Count = rmsTableModels.Count,
            Data = rmsTableModels
         };
      }
   }
}
