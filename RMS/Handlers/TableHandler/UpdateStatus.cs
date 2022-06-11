using MediatR;
using Microsoft.EntityFrameworkCore;
using RMS.Data;
using RMS.Exceptions;
using RMS.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RMS.Handlers.TableHandler
{
   public class UpdateStatus : IRequestHandler<UpdateStatus.UpdateTableRequest, UpdateStatus.Response>
   {
      private readonly RMSContext ctx;

      public UpdateStatus(RMSContext ctx)
      {
         this.ctx = ctx;
      }
      public class UpdateTableRequest : IRequest<Response>
      {
         public int Id { get; set; }
         public TableStatus Status { get; set; }
      }

      public class Response
      {
         public string Message { get; set; }
         public object Data { get; set; }
      }

      public async Task<Response> Handle(UpdateTableRequest request, CancellationToken cancellationToken)
      {
         if (request?.Id == null)
            throw new BadRequestException("Id must be present");
         
         var rmsTableEntity = await ctx.RmsTables
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == request.Id);
         
         if (rmsTableEntity == null)
            throw new NotFoundException("Table not found for the given id");

         rmsTableEntity.Status = (byte)request.Status;
         ctx.RmsTables.Update(rmsTableEntity);
         await ctx.SaveChangesAsync();

         return new Response
         {
            Message = "Table modified successfully.",
            Data = TableModel.ToModel<TableModel>(rmsTableEntity)
         };
      }
   }
}
