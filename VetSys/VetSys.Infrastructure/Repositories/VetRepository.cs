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
    public class VetRepository
    {
        private readonly VetSysApplicationContext context;

        public VetRepository(VetSysApplicationContext context)
        {
            this.context = context;
        }

        // Registrar un nuevo veterinario
        public async Task<Vet> AddVetAsync(Vet vet)
        {
            await context.Vets.AddAsync(vet);
            await context.SaveChangesAsync();
            return vet;
        }

        // Editar un veterinario existente
        public async Task<Vet> UpdateVetAsync(Vet vet)
        {
            var existingVet = await context.Vets.FindAsync(vet.Id);
            if (existingVet == null)
                throw new KeyNotFoundException("Vet not found");

            existingVet.Name = vet.Name;

            context.Vets.Update(existingVet);
            await context.SaveChangesAsync();
            return existingVet;
        }

        // Eliminar un veterinario
        public async Task<bool> DeleteVetAsync(int id)
        {
            var vet = await context.Vets.FindAsync(id);
            if (vet == null)
                return false;

            context.Vets.Remove(vet);
            await context.SaveChangesAsync();
            return true;
        }

        // Obtener un veterinario por Id
        public async Task<Vet> GetVetByIdAsync(int id)
        {
            return await context.Vets.FindAsync(id);
        }

        // Obtener todos los veterinarios
        public async Task<List<Vet>> GetAllVetsAsync()
        {
            return await context.Vets.ToListAsync();
        }
    }
}