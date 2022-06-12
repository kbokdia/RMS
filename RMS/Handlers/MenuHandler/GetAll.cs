using MediatR;
using Microsoft.EntityFrameworkCore;
using RMS.Data;
using RMS.Models;
using System.Threading;
using System.Threading.Tasks;
using RMS.Exceptions;
using System.Linq;
using System.Collections.Generic;

namespace RMS.Handlers.MenuHandler
{
   public class GetAll : IRequestHandler<GetAll.GetAllMenuRequest, GetAll.Response>
   {
      private readonly RMSContext ctx;

      public GetAll(RMSContext ctx)
      {
         this.ctx = ctx;
      }
      public class GetAllMenuRequest : IRequest<Response>
      {
         public string Name { get; set; } = null;
         public bool? IsVeg { get; set; } = null;
      }

      public class Response
      {
         public string Message { get; set; }
         public int Count { get; set; }
         public List<MenuModel> Data { get; set; }
      }

      public async Task<Response> Handle(GetAllMenuRequest request, CancellationToken cancellationToken)
      {
         var query = ctx.Menus.AsNoTracking();
         if(request.Name != null)
         {
            var likeText = $"%{request.Name}%";
            query = query.Where(m => EF.Functions.Like(m.Name, likeText));
         }

         if (request.IsVeg != null)
         {
            var isVeg = (byte?)(request.IsVeg.Value ? 1 : 2);
            query = query.Where(q => q.IsVeg == null || q.IsVeg == isVeg);
         }

         var menuEntites = await query.ToListAsync();
         var menuModels = menuEntites.Select(x => MenuModel.ToModel<MenuModel>(x)).ToList();

         return new Response
         {
            Message = "All OK.",
            Count = menuModels.Count,
            Data = menuModels
         };
      }
   }
}
