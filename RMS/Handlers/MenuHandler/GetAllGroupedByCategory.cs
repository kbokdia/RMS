using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RMS.Handlers.MenuHandler
{
   public class GetAllGroupedByCategory : IRequestHandler<GetAllGroupedByCategory.Request, GetAllGroupedByCategory.Response>
   {
      IMediator mediator;
      public GetAllGroupedByCategory(IMediator mediator)
      {
         this.mediator = mediator;
      }
      public class Request : IRequest<Response>
      {
         public string Name { get; set; } = null;
         public bool? IsVeg { get; set; } = null;
      }
      public class Response
      {
         public string Message { get; set; }
         public object Data { get; set; }
      }
      public async Task<Response> Handle(GetAllGroupedByCategory.Request request, CancellationToken cancellationToken)
      {

         var getItemRequest = new GetAll.GetAllMenuRequest() 
         { 
            Name = request.Name,
            IsVeg = request.IsVeg,
         };

         var menuItems = await mediator.Send(getItemRequest);
         // var groupedItems = menuItems.Data.GroupBy(x => x.CategoryType, x=> x);
         var groupedItems = menuItems.Data
            .GroupBy(x => x.CategoryType, x => x)
            .Select(x => new { category = x?.Key, items = x?.Select(y => y).ToList() });
         // var groupedItemArray = groupedItems.Select(x => new { category = x.Key, items = x[x.Key] })


         return new Response
         {
            Message = "All OK.",
            Data = groupedItems
         };
      }
   }
}
