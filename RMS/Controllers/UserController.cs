using MediatR;
using Microsoft.AspNetCore.Mvc;
using RMS.Exceptions;
using RMS.Handlers.UserHandler;
using RMS.Models;
using System.Threading.Tasks;

namespace RMS.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class UserController: RmsControllerBase
   {
      private readonly IMediator mediator;

      public UserController(IMediator mediator)
      {
         this.mediator = mediator;
      }

      [HttpPost]
      public async Task<IActionResult> CreateUsers([FromBody] CreateUserModel requestBody)
      {
         try
         {
            var request = new Create.CreateUserRequest
            {
               Model = requestBody,
            };
            var result = await mediator.Send(request);

            return Ok(result);
         }
         catch (BadRequestException bre)
         {
            return BadRequest(new { Error = bre.Message });
         }
         catch (NotFoundException nfe)
         {
            return NotFound(new { Error = nfe.Message });
         }
      }
   }
}
