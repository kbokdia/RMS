using MediatR;
using Microsoft.EntityFrameworkCore;
using RMS.Data;
using RMS.Models;
using System.Threading;
using System.Threading.Tasks;
using RMS.Exceptions;

namespace RMS.Handlers.TableHandler
{
   public class Get : IRequestHandler<Get.GetTableRequest, Get.Response>
   {
      private readonly RMSContext ctx;

      public Get(RMSContext ctx)
      {
         this.ctx = ctx;
      }
      public class GetTableRequest : IRequest<Response>
      {
         public int Id { get; set; }
      }

      public class Response
      {
         public string Message { get; set; }
         public object Data { get; set; }
      }

      public async Task<Response> Handle(GetTableRequest request, CancellationToken cancellationToken)
      {
         if (request?.Id == null) { throw new BadRequestException("Id must be present"); }

         var rmsTableEntity = await ctx.RmsTables
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == request.Id);
         if (rmsTableEntity == null) { throw new NotFoundException("Table not found for the given id"); }
         var rmsTableModel = TableModel.ToModel<TableModel>(rmsTableEntity);

         return new Response
         {
            Message = "All Ok",
            Data = rmsTableModel
         };
      }
   }
}
