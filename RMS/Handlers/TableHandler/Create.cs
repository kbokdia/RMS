using MediatR;
using RMS.Data;
using RMS.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RMS.Handlers.TableHandler
{
   public class Create : IRequestHandler<Create.CreateTableRequest, Create.Response>
   {
      private readonly RMSContext ctx;

      public Create(RMSContext ctx)
      {
         this.ctx = ctx;
      }
      public class CreateTableRequest : IRequest<Response>
      {
         public CreateTableModel Model { get; set; }
      }

      public class Response
      {
         public string Message { get; set; }
         public object Data { get; set; }
      }

      public async Task<Response> Handle(CreateTableRequest request, CancellationToken cancellationToken)
      {
         var entity = CreateTableModel.ToEntity(request.Model);
         ctx.RmsTables.Add(entity);
         await ctx.SaveChangesAsync();

         return new Response
         {
            Message = "Table saved successfully.",
            Data = new
            {
               TableId = entity.Id,
            }
         };
      }
   }
}
