using CmacApi.Exceptions;
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
   public class TableController: ControllerBase
   {
      private readonly IMediator mediator;

      public TableController(IMediator mediator)
      {
         this.mediator = mediator;
      }

      [HttpPost]
      public async Task<IActionResult> Create([FromBody] CreateTableModel requestBody)
      {
         try
         {
            var request = new Create.CreateTableRequest
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
         catch (UnauthorizedException ue)
         {
            return Unauthorized(new { Error = ue.Message });
         }
      }

   }
}
