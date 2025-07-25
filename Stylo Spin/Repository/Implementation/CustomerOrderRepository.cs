using Microsoft.EntityFrameworkCore;
using Stylo_Spin.Models;

namespace Stylo_Spin.Repository.Implementation
{
    public class CustomerOrderRepository : ICustomerOrderRepository
    {
        private readonly StyloSpinContext _context; //StyloSpinContext

        public CustomerOrderRepository(StyloSpinContext context)
        {
            _context = context;
        }

        private async Task<bool> SaveAsync() => await _context.SaveChangesAsync() > 0;

        public async Task<bool> CheckCustomerExistsByEmailAndContactAsync(string contact, string email)
        {
            return await _context.TblCustomers
                .AnyAsync(c => c.ContactNumber.Contains(contact) && c.CEmail == email);
        }

        public async Task<bool> CreateCustomerAsync(TblCustomer customer)
        {
            if (await CheckCustomerExistsByEmailAndContactAsync(customer.ContactNumber, customer.CEmail))
                return false;

            await _context.TblCustomers.AddAsync(customer);
            return await SaveAsync();
        }

        public async Task<bool> CreateOrderAsync(TblOrder order)
        {
            await _context.TblOrders.AddAsync(order);
            return await SaveAsync();
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await GetTblOrderByIdAsync(orderId);
            if (order == null) return false;

            _context.TblOrders.Remove(order);
            return await SaveAsync();
        }

        public async Task<List<TblOrder>> GetAllOrdersAsync()
        {
            return await _context.TblOrders
                .Include(o => o.CIdNavigation)
                .Include(o => o.PIdNavigation).ThenInclude(p=> p.CIdNavigation)
                .ToListAsync();
        }

        public async Task<TblOrder?> GetTblOrderByIdAsync(int orderId)
        {
            return await _context.TblOrders
                .Include(o => o.CIdNavigation)
                .Include(o => o.PIdNavigation)
                .FirstOrDefaultAsync(o => o.OId == orderId);
        }

        public async Task<List<TblOrder>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.TblOrders
                .Where(o => o.CId == customerId)
                .Include(o => o.CIdNavigation)
                .Include(o => o.PIdNavigation)
                .ToListAsync();
        }

        public async Task<List<TblOrder>> GetOrdersByStatusAsync(string status)
        {
            return await _context.TblOrders
                .Where(o => o.PaymentStatus == status)
                .Include(o => o.CIdNavigation)
                .Include(o => o.PIdNavigation)
                .ToListAsync();
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            var customer = await GetCustomerByIdAsync(customerId);
            if (customer == null) return false;

            _context.TblCustomers.Remove(customer);
            return await SaveAsync();
        }

        public async Task<TblCustomer?> GetCustomerByIdAsync(int customerId)
        {
            return await _context.TblCustomers.FirstOrDefaultAsync(c => c.Id == customerId);
        }

      

        public Task<TblOrder> GetCustomerOrderByIdAsync(int orderId)
        {
           return _context.TblOrders
                .Include(o => o.CIdNavigation)
                .Include(o => o.PIdNavigation)
                .FirstOrDefaultAsync(o => o.OId == orderId);
        }

        public async Task<TblCustomer> GetCustmorByEmail(string email)
        {
            return await _context.TblCustomers.FirstOrDefaultAsync(c=> c.CEmail==email);
        }

        public async Task<List<TblOrder>> GetOrderByProductIdAsync(int productId)
        {
            return await _context.TblOrders
                .Where(o => o.PId == productId)
                .Include(o => o.CIdNavigation)
                .Include(o => o.PIdNavigation)
                .ToListAsync();
        }

        public async Task<List<TblCustomer>> GetTblCustomers()
        {
           return await _context.TblCustomers
                .Include(c => c.TblOrders)
                .ToListAsync();
        }
    }
}
