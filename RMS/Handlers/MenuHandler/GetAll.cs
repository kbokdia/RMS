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
      }

      public class Response
      {
         public string Message { get; set; }
         public int Count { get; set; }
         public List<MenuModel> Data { get; set; }
      }

      public async Task<Response> Handle(GetAllMenuRequest request, CancellationToken cancellationToken)
      {
         var menuEntites = await ctx.Menus.AsNoTracking().ToListAsync();
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
