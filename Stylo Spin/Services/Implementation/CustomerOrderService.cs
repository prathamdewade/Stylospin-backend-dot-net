using Stylo_Spin.Models;
using Stylo_Spin.Services.Defination;

namespace Stylo_Spin.Services.Implementation
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly ICustomerOrderRepository repo;

        public CustomerOrderService(ICustomerOrderRepository repo)
        {
            this.repo = repo;
        }

        public async Task<bool> CreateOrderAsync(CustomerOrderDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CustomerName) ||
                string.IsNullOrWhiteSpace(dto.CustomerEmail) ||
                !dto.CustomerEmail.Contains("@gmail.com") ||
                string.IsNullOrWhiteSpace(dto.ContactNumber))
            {
                return false;
            }

            var customer = new TblCustomer
            {
                CName = dto.CustomerName,
                CEmail = dto.CustomerEmail,
                ContactNumber = dto.ContactNumber,
                Address = dto.Address
            };

            if (!await repo.CreateCustomerAsync(customer))
            {
                customer = await repo.GetCustmorByEmail(dto.CustomerEmail);
            }

            var order = new TblOrder
            {
                CId = customer.Id,
                PId = dto.ProductId,
                PaymentStatus = dto.PaymentStatus,
                ProductQuantity = dto.ProductQuantity,
                TotalPrice = dto.TotalPrice,
                Message = dto.Message,
                OrdaerDate = DateTime.Now
            };

            return await repo.CreateOrderAsync(order);
        }

        public Task<bool> DeleteCustomerAsync(int customerId) => repo.DeleteCustomerAsync(customerId);
        public Task<bool> DeleteOrderAsync(int orderId) => repo.DeleteOrderAsync(orderId);
        public Task<List<TblOrder>> GetAllOrdersAsync() => repo.GetAllOrdersAsync();
        public Task<List<TblCustomer>> GetAllCustomerOrdersAsync() => repo.GetTblCustomers();
        public Task<TblCustomer?> GetCustomerByIdAsync(int customerId) => repo.GetCustomerByIdAsync(customerId);
        public Task<TblOrder?> GetOrderByIdAsync(int orderId) => repo.GetCustomerOrderByIdAsync(orderId);
        public Task<List<TblOrder>> GetOrdersByCustomerIdAsync(int customerId) => repo.GetOrdersByCustomerIdAsync(customerId);
        public Task<List<TblOrder>> GetOrdersByStatusAsync(string status) => repo.GetOrdersByStatusAsync(status);
        public Task<List<TblOrder>> GetOrderByProductId(int productId) => repo.GetOrderByProductIdAsync(productId);
        public Task<bool> CreateCustomerAsync(TblCustomer customer) => repo.CreateCustomerAsync(customer);
    }
}
