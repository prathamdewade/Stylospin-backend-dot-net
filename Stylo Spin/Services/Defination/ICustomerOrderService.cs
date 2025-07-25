using Stylo_Spin.Models;

namespace Stylo_Spin.Services.Defination
{
    public interface ICustomerOrderService
    {
        Task<List<TblOrder>> GetAllOrdersAsync();
        Task<bool> CreateOrderAsync(CustomerOrderDto dto);
        Task<bool> DeleteOrderAsync(int orderId);
        Task<TblOrder?> GetOrderByIdAsync(int orderId);
        Task<List<TblOrder>> GetOrdersByCustomerIdAsync(int customerId);
        Task<List<TblOrder>> GetOrdersByStatusAsync(string status);

        Task<bool> CreateCustomerAsync(TblCustomer customer);
        Task<bool> DeleteCustomerAsync(int customerId);
        Task<TblCustomer?> GetCustomerByIdAsync(int customerId);
        Task<List<TblCustomer>> GetAllCustomerOrdersAsync();
        Task<List<TblOrder>> GetOrderByProductId(int productId);

    }
}
