using AutoMapper;
using Medicare2.APIs.Errors;
using Medicare2.Core.Entities;
using Medicare2.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Medicare2.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartController(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        [HttpGet("{CartId}")]
        public async Task<ActionResult<Usercart>> GetUserCart(string CartId)
        {
            var Cart = await _cartRepository.GetUserCartAsync(CartId);
            return Cart == null ? new Usercart(CartId) : Ok(Cart);

        }
        [HttpPost]
        public async Task<ActionResult<Usercart>> UpdateCart(CustomerCartDTo Cart)
        {
            var Mappedcart = _mapper.Map<CustomerCartDTo, Usercart>(Cart);
            var CreatedOrUpdatedCart = await _cartRepository.UpdateUserCartAsync(Mappedcart);
            if (CreatedOrUpdatedCart == null) return BadRequest(new ApiResponse(400));
            return Ok(CreatedOrUpdatedCart);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteCart(string CartId)
        {
            return await _cartRepository.deleteCartAsync(CartId);
        }

    }

}
