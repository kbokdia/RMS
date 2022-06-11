using MediatR;
using Microsoft.AspNetCore.Mvc;
using RMS.Authorization;
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
   }
}
