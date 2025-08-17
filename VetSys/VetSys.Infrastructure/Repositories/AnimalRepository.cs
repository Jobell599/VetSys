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
    public class AnimalRepository
    {
        private readonly VetSysApplicationContext context;

        public AnimalRepository(VetSysApplicationContext context)
        {
            this.context = context;
        }

        // Registrar un nuevo animal
        public async Task<Animal> AddAnimalAsync(Animal animal)
        {
            await context.Animals.AddAsync(animal);
            await context.SaveChangesAsync();
            return animal;
        }

        // Editar información de un animal existente
        public async Task<Animal> UpdateAnimalAsync(Animal animal)
        {
            var existingAnimal = await context.Animals
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.Id == animal.Id);

            if (existingAnimal == null)
                throw new KeyNotFoundException("Animal not found");

            existingAnimal.Name = animal.Name;
            existingAnimal.Kind = animal.Kind;
            existingAnimal.Race = animal.Race;
            existingAnimal.Sex = animal.Sex;
            existingAnimal.Birth = animal.Birth;
            existingAnimal.Weight = animal.Weight;
            existingAnimal.CustomerId = animal.CustomerId;

            context.Animals.Update(existingAnimal);
            await context.SaveChangesAsync();
            return existingAnimal;
        }

        // Eliminar un animal
        public async Task<bool> DeleteAnimalAsync(int id)
        {
            var animal = await context.Animals.FindAsync(id);
            if (animal == null)
                return false;

            context.Animals.Remove(animal);
            await context.SaveChangesAsync();
            return true;
        }

        // Obtener un animal por Id
        public async Task<Animal> GetAnimalByIdAsync(int id)
        {
            return await context.Animals
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // Obtener todos los animales
        public async Task<List<Animal>> GetAllAnimalsAsync()
        {
            return await context.Animals
                .Include(a => a.Customer)
                .ToListAsync();
        }

        // Buscar animales por nombre
        public async Task<List<Animal>> SearchAnimalsByNameAsync(string name)
        {
            return await context.Animals
                .Include(a => a.Customer)
                .Where(a => a.Name.Contains(name))
                .ToListAsync();
        }

        // Buscar animales por especie
        public async Task<List<Animal>> SearchAnimalsByKindAsync(string kind)
        {
            return await context.Animals
                .Include(a => a.Customer)
                .Where(a => a.Kind.Contains(kind))
                .ToListAsync();
        }

        // Buscar animales por cliente propietario
        public async Task<List<Animal>> SearchAnimalsByCustomerAsync(int customerId)
        {
            return await context.Animals
                .Include(a => a.Customer)
                .Where(a => a.CustomerId == customerId)
                .ToListAsync();
        }
    }
}