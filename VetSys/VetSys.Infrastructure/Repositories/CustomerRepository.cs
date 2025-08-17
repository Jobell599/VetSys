using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSys.Domain.Entities;
using VetSys.Infrastructure.Data;

namespace VetSys.Infrastructure.Repositories
{
    public class CustomerRepository
    {
        private readonly VetSysApplicationContext context;

        public CustomerRepository(VetSysApplicationContext context)
        {
            this.context = context;
        }

        // Registrar un nuevo cliente
        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();
            return customer;
        }

        // Editar un cliente existente
        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = await context.Customers
                .Include(c => c.Animals)
                .FirstOrDefaultAsync(c => c.Id == customer.Id);

            if (existingCustomer == null)
                throw new KeyNotFoundException("Customer not found");

            existingCustomer.Name = customer.Name;
            existingCustomer.Address = customer.Address;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Email = customer.Email;

            context.Customers.Update(existingCustomer);
            await context.SaveChangesAsync();
            return existingCustomer;
        }

        // Eliminar un cliente
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer == null)
                return false;

            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
            return true;
        }

        // Obtener un cliente por Id
        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await context.Customers
                .Include(c => c.Animals)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Obtener todos los clientes
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await context.Customers
                .Include(c => c.Animals)
                .ToListAsync();
        }
    }
}
