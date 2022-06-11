using MediatR;
using Microsoft.AspNetCore.Mvc;
using RMS.Exceptions;
using RMS.Handlers.TableHandler;
using RMS.Models;
using System.Threading.Tasks;

namespace RMS.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class TableController : ControllerBase
   {
      private readonly IMediator mediator;

      public TableController(IMediator mediator)
      {
         this.mediator = mediator;
      }

      [HttpGet]
      public async Task<IActionResult> GetAll([FromQuery] GetAll.GetAllTableRequest   requestParams)
      {
         try
         {;
            var result = await mediator.Send(requestParams);
            return Ok(result);
         }
         catch (BadRequestException bre) { return BadRequest(new { Error = bre.Message }); }
         catch (NotFoundException nfe) { return NotFound(new { Error = nfe.Message }); }
         catch (UnauthorizedException ue) { return Unauthorized(new { Error = ue.Message }); }
      }

      [HttpGet("{id}")]
      public async Task<IActionResult> Get(int id)
      {
         try
         {
            var request = new Get.GetTableRequest { Id = id };
            var result = await mediator.Send(request);
            return Ok(result);
         }
         catch (BadRequestException bre) { return BadRequest(new { Error = bre.Message }); }
         catch (NotFoundException nfe) { return NotFound(new { Error = nfe.Message }); }
         catch (UnauthorizedException ue) { return Unauthorized(new { Error = ue.Message }); }
      }

      [HttpPost]
      public async Task<IActionResult> Create([FromBody] CreateTableModel requestBody)
      {
         try
         {
            var request = new Create.CreateTableRequest { Model = requestBody, };
            var result = await mediator.Send(request);
            return Ok(result);
         }
         catch (BadRequestException bre) { return BadRequest(new { Error = bre.Message }); }
         catch (NotFoundException nfe) { return NotFound(new { Error = nfe.Message }); }
         catch (UnauthorizedException ue) { return Unauthorized(new { Error = ue.Message }); }
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> Update(int id, [FromBody] TableModel requestBody)
      {
         try
         {
            var request = new Update.UpdateTableRequest { Id = id, Model = requestBody, };
            var result = await mediator.Send(request);
            return Ok(result);
         }
         catch (BadRequestException bre) { return BadRequest(new { Error = bre.Message }); }
         catch (NotFoundException nfe) { return NotFound(new { Error = nfe.Message }); }
         catch (UnauthorizedException ue) { return Unauthorized(new { Error = ue.Message }); }
      }


      [HttpPut("{id}/{isAvialable}")]
      public async Task<IActionResult> UpdateStatus(int id, TableStatus isAvialable)
      {
         try
         {
            var request = new UpdateAvialablity.UpdateTableRequest { Id = id, IsAvialable = isAvialable };
            var result = await mediator.Send(request);
            return Ok(result);
         }
         catch (BadRequestException bre) { return BadRequest(new { Error = bre.Message }); }
         catch (NotFoundException nfe) { return NotFound(new { Error = nfe.Message }); }
         catch (UnauthorizedException ue) { return Unauthorized(new { Error = ue.Message }); }
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> UpdateStatus(int id)
      {
         try
         {
            var request = new Delete.DeleteTableRequest { Id = id };
            var result = await mediator.Send(request);
            return Ok(result);
         }
         catch (BadRequestException bre) { return BadRequest(new { Error = bre.Message }); }
         catch (NotFoundException nfe) { return NotFound(new { Error = nfe.Message }); }
         catch (UnauthorizedException ue) { return Unauthorized(new { Error = ue.Message }); }
      }

   }
}
