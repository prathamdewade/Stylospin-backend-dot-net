using Stylo_Spin.Models;

public interface ICustomerOrderRepository
{
    Task<List<TblOrder>> GetAllOrdersAsync();
    Task<bool> CreateCustomerAsync(TblCustomer customer);
    Task<bool> CheckCustomerExistsByEmailAndContactAsync(string contact, string email);
    Task<TblCustomer> GetCustmorByEmail(string email);
    Task<List<TblCustomer>> GetTblCustomers();
    Task<bool> DeleteCustomerAsync(int customerId);
    Task<TblCustomer> GetCustomerByIdAsync(int customerId);

    Task<bool> CreateOrderAsync(TblOrder order);
    Task<bool> DeleteOrderAsync(int orderId);
    Task<List<TblOrder>> GetOrdersByCustomerIdAsync(int customerId);
    Task<TblOrder> GetCustomerOrderByIdAsync(int orderId);
    Task<TblOrder> GetTblOrderByIdAsync(int orderId);
    Task<List<TblOrder>> GetOrdersByStatusAsync(string status);
    Task<List<TblOrder>> GetOrderByProductIdAsync(int productId);
}
