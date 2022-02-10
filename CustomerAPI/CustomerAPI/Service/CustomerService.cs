using CustomerAPI.Model;
using CustomerAPI.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAPI.Service
{
    public interface ICustomerService
    {
        Task<bool> AddCustomer(Customer customer);
        IEnumerable<Customer> GetCustomer();
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
                await _dbManager.CreateRepository<Customer>().Add(customer);
                await _dbManager.SaveAsync();

            }
            catch (Exception ex)
            {
                //log
                return false;
            }
            return true;
        }


        public IEnumerable<Customer> GetCustomer()
        {
            IEnumerable<Customer> customer = null;
            try
            {
                customer = _dbManager.CreateRepository<Customer>().Get();

            }
            catch (Exception ex)
            {
                //log
                return null;
            }
            return customer;
        }

    }
}
