using MediatR;
using RMS.Data;
using RMS.Handlers.Users;
using RMS.Models;
using System.Threading.Tasks;

namespace RMS.Services
{
   public interface IUserService
   {
      Task<UserModel> GetById(int id);
   }

   public class UserService : IUserService
   {
      private readonly IMediator mediator;
      private readonly RMSContext ctx;

      public UserService(IMediator mediator, RMSContext ctx)
      {
         this.mediator = mediator;
         this.ctx = ctx;
      }

      public async Task<UserModel> GetById(int id)
      {
         var request = new Get.GetUserRequest
         {
            UserId = id
         };
         var result = await mediator.Send(request);
         return result.Data;

      }
   }
}
