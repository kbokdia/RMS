using MediatR;
using Microsoft.EntityFrameworkCore;
using RMS.Data;
using RMS.Handlers.UserHandler;
using RMS.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.Services
{
   public interface IUserService
   {
      Task<UserModel> GetById(int id);
      Task<int> GetOrCreate(string mobile);
      Task<int> Create(string mobile);
      Task<UserModel> GetOrCreateModel(string mobile);
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

      public async Task<int> GetOrCreate(string mobile)
      {
         var user = await ctx.Users
           .AsNoTracking()
           .Where(u => u.Mobile == mobile)
           .SingleOrDefaultAsync();

         if (user == null)
            return await Create(mobile);

         return user.Id;
      }

      public async Task<int> Create(string mobile)
      {
         var userModel = new CreateUserModel
         {
            Mobile = mobile,
            Password = "password",
            Type = UserType.Customer,
            Status = Status.Active,
         };

         var request = new Create.CreateUserRequest
         {
            Model = userModel
         };

         var result = await mediator.Send(request);
         return result.Data.UserId;
      }

      public async Task<UserModel> GetOrCreateModel(string mobile)
      {
         var userId = await GetOrCreate(mobile);
         return await GetById(userId);
      }
   }
}
