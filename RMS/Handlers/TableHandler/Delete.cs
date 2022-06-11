using MediatR;
using Microsoft.EntityFrameworkCore;
using RMS.Data;
using RMS.Models;
using System.Threading;
using System.Threading.Tasks;
using RMS.Exceptions;

namespace RMS.Handlers.TableHandler
{
   public class Delete : IRequestHandler<Delete.DeleteTableRequest, Delete.Response>
   {
      private readonly RMSContext ctx;

      public Delete(RMSContext ctx)
      {
         this.ctx = ctx;
      }
      public class DeleteTableRequest : IRequest<Response>
      {
         public int Id { get; set; }
      }

      public class Response
      {
         public string Message { get; set; }
         public object Data { get; set; }
      }

      public async Task<Response> Handle(DeleteTableRequest request, CancellationToken cancellationToken)
      {
         if (request?.Id == null) { throw new BadRequestException("Id must be present"); }

         var entity = await ctx.RmsTables.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);

         if (entity == null) { throw new NotFoundException("Entity does not exist"); }

         ctx.RmsTables.Remove(entity);
         await ctx.SaveChangesAsync(cancellationToken);

         return new Response { Message = "Deleted successfully.", Data = true };
      }
   }
}
