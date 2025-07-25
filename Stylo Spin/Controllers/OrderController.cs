using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stylo_Spin.Models;
using Stylo_Spin.Services.Defination;

namespace Stylo_Spin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class OrderController : ControllerBase
    {
        private readonly ICustomerOrderService _customerOrderService;

        public OrderController(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        // --- Order Endpoints (already implemented) ---

        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _customerOrderService.GetAllOrdersAsync();
            return Ok(ApiResponse<List<TblOrder>>.SuccessResponse(orders, "All orders fetched."));
        }

        [HttpGet("GetOrderById/{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _customerOrderService.GetOrderByIdAsync(orderId);
            if (order == null) return NotFound(ApiResponse<TblOrder>.ErrorResponse("Order not found."));
            return Ok(ApiResponse<TblOrder>.SuccessResponse(order, "Order fetched."));
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CustomerOrderDto orderDto)
        {
            if (orderDto == null) return BadRequest(ApiResponse<string>.ErrorResponse("Invalid order data."));
            var result = await _customerOrderService.CreateOrderAsync(orderDto);
            if (!result) return BadRequest(ApiResponse<string>.ErrorResponse("Failed to create order."));
            return Ok(ApiResponse<string>.SuccessResponse("Order created successfully"));
        }

        [HttpDelete("DeleteOrder/{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var result = await _customerOrderService.DeleteOrderAsync(orderId);
            if (!result) return NotFound(ApiResponse<string>.ErrorResponse("Order not found."));
            return Ok(ApiResponse<string>.SuccessResponse("Order deleted successfully"));
        }

        // --- Customer Endpoints ---

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerOrderService.GetAllCustomerOrdersAsync();
            return Ok(ApiResponse<List<TblCustomer>>.SuccessResponse(customers, "All customers fetched."));
        }

        [HttpGet("GetCustomerById/{customerId}")]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            var customer = await _customerOrderService.GetCustomerByIdAsync(customerId);
            if (customer == null) return NotFound(ApiResponse<TblCustomer>.ErrorResponse("Customer not found."));
            return Ok(ApiResponse<TblCustomer>.SuccessResponse(customer, "Customer fetched."));
        }

      /*  [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] TblCustomer customer)
        {
            if (customer == null) return BadRequest(ApiResponse<string>.ErrorResponse("Invalid customer data."));
            var result = await _customerOrderService.CreateCustomerAsync(customer);
            if (!result) return BadRequest(ApiResponse<string>.ErrorResponse("Failed to create customer."));
            return Ok(ApiResponse<string>.SuccessResponse("Customer created successfully"));
        }*/

        [HttpDelete("DeleteCustomer/{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            var result = await _customerOrderService.DeleteCustomerAsync(customerId);
            if (!result) return NotFound(ApiResponse<string>.ErrorResponse("Customer not found."));
            return Ok(ApiResponse<string>.SuccessResponse("Customer deleted successfully"));
        }
    }
}
