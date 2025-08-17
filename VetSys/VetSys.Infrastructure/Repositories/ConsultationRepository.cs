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
    public class ConsultationRepository
    {
        private readonly VetSysApplicationContext context;

        public ConsultationRepository(VetSysApplicationContext context)
        {
            this.context = context;
        }

        // Registrar nueva consulta
        public async Task<Consultation> AddConsultationAsync(Consultation consultation)
        {
            await context.Consultations.AddAsync(consultation);
            await context.SaveChangesAsync();
            return consultation;
        }

        // Editar consulta existente
        public async Task<Consultation> UpdateConsultationAsync(Consultation consultation)
        {
            var existing = await context.Consultations
                .Include(c => c.Animal)
                .Include(c => c.Vet)
                .Include(c => c.Treatments)
                .FirstOrDefaultAsync(c => c.Id == consultation.Id);

            if (existing == null)
                throw new KeyNotFoundException("Consultation not found");

            existing.Date = consultation.Date;
            existing.Description = consultation.Description;
            existing.AnimalId = consultation.AnimalId;
            existing.VetId = consultation.VetId;

            context.Consultations.Update(existing);
            await context.SaveChangesAsync();
            return existing;
        }

        // Eliminar consulta
        public async Task<bool> DeleteConsultationAsync(int id)
        {
            var consultation = await context.Consultations.FindAsync(id);
            if (consultation == null)
                return false;

            context.Consultations.Remove(consultation);
            await context.SaveChangesAsync();
            return true;
        }

        // Obtener consulta por Id
        public async Task<Consultation> GetConsultationByIdAsync(int id)
        {
            return await context.Consultations
                .Include(c => c.Animal)
                .Include(c => c.Vet)
                .Include(c => c.Treatments)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Obtener todas las consultas
        public async Task<List<Consultation>> GetAllConsultationsAsync()
        {
            return await context.Consultations
                .Include(c => c.Animal)
                .Include(c => c.Vet)
                .Include(c => c.Treatments)
                .ToListAsync();
        }
    }
}