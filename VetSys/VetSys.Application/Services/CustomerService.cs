using Microsoft.EntityFrameworkCore;
using VetSys.Domain.Entities;

namespace VetSys.Infrastructure.Repositories
{
    public class CustomerService
    {
        private readonly UnityOfWork unitOfWork;

        public CustomerService(UnityOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Registrar un nuevo cliente
        public async Task<CustomerDto> AddCustomerAsync(CustomerDto customerDto)
        {
            var customer = new Customer
            {
                Name = customerDto.Name,
                Address = customerDto.Address,
                Phone = customerDto.Phone,
                Email = customerDto.Email
            };

            await unitOfWork.Customers.AddCustomerAsync(customer);
            await unitOfWork.CompleteAsync();

            customerDto.Id = customer.Id;
            return customerDto;
        }

        // Editar un cliente existente
        public async Task<CustomerDto> UpdateCustomerAsync(CustomerDto customerDto)
        {
            var existingCustomer = await unitOfWork.Customers.GetCustomerByIdAsync(customerDto.Id);
            if (existingCustomer == null)
                throw new KeyNotFoundException("Customer not found");

            existingCustomer.Name = customerDto.Name;
            existingCustomer.Address = customerDto.Address;
            existingCustomer.Phone = customerDto.Phone;
            existingCustomer.Email = customerDto.Email;

            unitOfWork.Customers.UpdateCustomerAsync(existingCustomer);
            await unitOfWork.CompleteAsync();

            return customerDto;
        }

        // Eliminar un cliente
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var result = await unitOfWork.Customers.DeleteCustomerAsync(id);
            await unitOfWork.CompleteAsync();
            return result;
        }

        // Obtener un cliente por Id
        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await unitOfWork.Customers.GetCustomerByIdAsync(id);
            if (customer == null)
                return null;

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Phone = customer.Phone,
                Email = customer.Email,
                Animals = customer.Animals.Select(a => new AnimalDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Kind = a.Kind,
                    Race = a.Race,
                    Sex = a.Sex,
                    Birth = a.Birth,
                    Weight = a.Weight,
                    CustomerId = a.CustomerId
                }).ToList()
            };
        }

        // Obtener todos los clientes
        public async Task<List<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await unitOfWork.Customers.GetAllCustomersAsync();
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Phone = c.Phone,
                Email = c.Email,
                Animals = c.Animals.Select(a => new AnimalDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Kind = a.Kind,
                    Race = a.Race,
                    Sex = a.Sex,
                    Birth = a.Birth,
                    Weight = a.Weight,
                    CustomerId = a.CustomerId
                }).ToList()
            }).ToList();
        }
    }
}