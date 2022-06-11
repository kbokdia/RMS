using MediatR;
using Microsoft.AspNetCore.Mvc;
using RMS.Exceptions;
using RMS.Handlers.MenuHandler;
using RMS.Models;
using System.Threading.Tasks;

namespace RMS.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class MenuController : ControllerBase
   {
      private readonly IMediator mediator;

      public MenuController(IMediator mediator)
      {
         this.mediator = mediator;
      }

      //[HttpGet]
      //public async Task<IActionResult> GetAll([FromQuery] GetAll.GetAllMenuRequest requestParams)
      //{
      //   try
      //   {
      //      ;
      //      var result = await mediator.Send(requestParams);
      //      return Ok(result);
      //   }
      //   catch (BadRequestException bre) { return BadRequest(new { Error = bre.Message }); }
      //   catch (NotFoundException nfe) { return NotFound(new { Error = nfe.Message }); }
      //   catch (UnauthorizedException ue) { return Unauthorized(new { Error = ue.Message }); }
      //}

      [HttpGet]
      public async Task<IActionResult> GetAllGroupedByCategory([FromQuery] GetAllGroupedByCategory.Request requestParams)
      {
         try
         {
            var result = await mediator.Send(requestParams);
            return Ok(result);
         }
         catch (BadRequestException bre) { return BadRequest(new { Error = bre.Message }); }
         catch (NotFoundException nfe) { return NotFound(new { Error = nfe.Message }); }
         catch (UnauthorizedException ue) { return Unauthorized(new { Error = ue.Message }); }
      }

      [HttpPut("{id}/{status}")]
      public async Task<IActionResult> UpdateStatus([FromRoute] int id, Status status)
      {
         try
         {
            var request = new UpdateStatus.UpdateMenuRequest { Id = id, Status = status };
            var result = await mediator.Send(request);
            return Ok(result);
         }
         catch (BadRequestException bre) { return BadRequest(new { Error = bre.Message }); }
         catch (NotFoundException nfe) { return NotFound(new { Error = nfe.Message }); }
         catch (UnauthorizedException ue) { return Unauthorized(new { Error = ue.Message }); }
      }

   }
}
