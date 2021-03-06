using MediatR;
using Microsoft.AspNetCore.Mvc;
using RMS.Exceptions;
using RMS.Handlers.OrderHandler;
using RMS.Models;
using System.Threading.Tasks;

namespace RMS.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class OrderController: RmsControllerBase
   {
      private readonly IMediator mediator;

      public OrderController(IMediator mediator)
      {
         this.mediator = mediator;
      }

      //[Authorize(UserType.Staff, UserType.Admin)]
      [HttpGet]
      public async Task<IActionResult> GetAllAsync([FromQuery] GetAll.GetAllOrderRequest request)
      {
         try
         {
            var result = await mediator.Send(request);
            return Ok(result);
         }
         catch (BadRequestException bre)
         {
            return BadRequest(new { Error = bre.Message });
         }
      }


      [HttpPost]
      public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel requestBody)
      {
         try
         {
            var request = new Create.CreateOrderRequest
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

      [HttpPut("{id}/{status}")]
      public async Task<IActionResult> UpdateStatus([FromRoute] int id, OrderStatus status)
      {
         try
         {
            var request = new UpdateStatus.UpdateOrderStatus { Id = id, Status = status };
            var result = await mediator.Send(request);
            return Ok(result);
         }
         catch (BadRequestException bre) { return BadRequest(new { Error = bre.Message }); }
         catch (NotFoundException nfe) { return NotFound(new { Error = nfe.Message }); }
         catch (UnauthorizedException ue) { return Unauthorized(new { Error = ue.Message }); }
      }
   }
}
