using MediatR;
using RMS.Data;
using RMS.Exceptions;
using RMS.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RMS.Handlers.TableHandler
{
   public class Update : IRequestHandler<Update.UpdateTableRequest, Update.Response>
   {
      private readonly RMSContext ctx;

      public Update(RMSContext ctx)
      {
         this.ctx = ctx;
      }
      public class UpdateTableRequest : IRequest<Response>
      {
         public int Id { get; set; }
         public TableModel Model { get; set; }
      }

      public class Response
      {
         public string Message { get; set; }
         public object Data { get; set; }
      }

      public async Task<Response> Handle(UpdateTableRequest request, CancellationToken cancellationToken)
      {
         if (request?.Id == null) { throw new BadRequestException("Id must be present"); }
         if (request?.Id == request?.Model?.Id) { throw new BadRequestException("Id must match"); }

         var entity = TableModel.ToEntity(request.Model);
         ctx.RmsTables.Update(entity);
         await ctx.SaveChangesAsync();

         return new Response
         {
            Message = "Table saved successfully.",
            Data = request.Model
         };
      }
   }
}
