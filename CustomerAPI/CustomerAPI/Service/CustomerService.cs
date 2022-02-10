using CustomerAPI.Model;
using CustomerAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Service
{
    public interface ICustomerService
    {
        Task<bool> AddCustomer(Customer customer);
        Task<IEnumerable<Customer>> GetCustomer();
        Task<Customer> FindCustomer(string searchWord);

        Task<bool> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(Customer customer);
    }

    public class CustomerService : ICustomerService
    {
        readonly IDBManager _dbManager;
        public CustomerService(IDBManager dbManager)
        {
            _dbManager = dbManager;
        }

        public async Task<bool> AddCustomer(Customer customer)
        {
            try
            {
                await _dbManager.GetRepository<Customer>().Add(customer);
                await _dbManager.SaveAsync();

            }
            catch (Exception)
            {
                //log
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteCustomer(Customer customer)
        {
            try
            {
                _dbManager.GetRepository<Customer>().Delete(customer);
                await _dbManager.SaveAsync();
            }
            catch (Exception)
            {
                //log
                return false;
            }
            return true;
        }

        public async Task<Customer> FindCustomer(string searchWord)
        {
            IEnumerable<Customer> customerList = null;
            Customer customer = null;
            try
            {
                customerList = await Task.Run(() => _dbManager.GetRepository<Customer>().Get());
                customer = customerList?.ToList().FirstOrDefault(c => c.firstName.StartsWith(searchWord, StringComparison.InvariantCultureIgnoreCase)
                || c.lastName.StartsWith(searchWord, StringComparison.InvariantCultureIgnoreCase));

            }
            catch (Exception)
            {
                //log
            }
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomer()
        {
            IEnumerable<Customer> customer = null;
            try
            {
                customer = await Task.Run(() => _dbManager.GetRepository<Customer>().Get());

            }
            catch (Exception)
            {
                //log
                return null;
            }
            return customer;
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            try
            {
                _dbManager.GetRepository<Customer>().Update(customer);
                await _dbManager.SaveAsync();
            }
            catch (Exception)
            {
                //log
                return false;
            }

            return true;
        }
    }
}
