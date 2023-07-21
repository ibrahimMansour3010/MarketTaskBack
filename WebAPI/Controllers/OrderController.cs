using Domain.Dtos;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                var response = _orderService.GetAllOrders();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetOrderById/{Id}")]
        public IActionResult GetOrderById(int Id)
        {
            try
            {
                var response = _orderService.GetOrderById(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("ManageOrder")]
        public async Task<IActionResult> ManageOrder([FromBody] OrderDto orderDto)
        {
            try
            {
                var response = await _orderService.ManageOrder(orderDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteOrderById/{Id}")]
        public IActionResult DeleteOrderById(int Id)
        {
            try
            {
                var response = _orderService.DeleteOrderById(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetOrderLookups")]
        public IActionResult GetOrderLookups()
        {
            try
            {
                var response = _orderService.GetOrderLookups();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetStocksPrices")]
        public IActionResult GetStocksPrices()
        {
            try
            {
                var response = _orderService.GetStocksPrices();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
