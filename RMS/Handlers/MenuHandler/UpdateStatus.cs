using MediatR;
using Microsoft.EntityFrameworkCore;
using RMS.Data;
using RMS.Exceptions;
using RMS.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RMS.Handlers.MenuHandler
{
   public class UpdateStatus : IRequestHandler<UpdateStatus.UpdateMenuRequest, UpdateStatus.Response>
   {
      private readonly RMSContext ctx;

      public UpdateStatus(RMSContext ctx)
      {
         this.ctx = ctx;
      }
      public class UpdateMenuRequest : IRequest<Response>
      {
         public int Id { get; set; }
         public Status Status { get; set; }
      }

      public class Response
      {
         public string Message { get; set; }
         public object Data { get; set; }
      }

      public async Task<Response> Handle(UpdateMenuRequest request, CancellationToken cancellationToken)
      {
         if (request?.Id == null)
            throw new BadRequestException("Id must be present");
         
         var entity = await ctx.Menus
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == request.Id);
         
         if (entity == null)
            throw new NotFoundException("Table not found for the given id");
         
         entity.Status = (byte)request.Status;
         ctx.Menus.Update(entity);
         await ctx.SaveChangesAsync();

         return new Response
         {
            Message = "Menu modified successfully.",
            Data = MenuModel.ToModel<MenuModel>(entity)
         };
      }
   }
}
