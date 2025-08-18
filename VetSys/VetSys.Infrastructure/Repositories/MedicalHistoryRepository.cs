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
    public class MedicalHistoryRepository
    {
        private readonly VetSysApplicationContext context;

        public MedicalHistoryRepository(VetSysApplicationContext context)
        {
            this.context = context;
        }

        // Registrar historial médico
        public async Task<MedicalHistory> AddMedicalHistoryAsync(MedicalHistory history)
        {
            await context.MedicalHistories.AddAsync(history);
            
            return history;
        }

        // Obtener historial médico por Id
        public async Task<MedicalHistory> GetMedicalHistoryByIdAsync(int id)
        {
            return await context.MedicalHistories
                .Include(h => h.Consultation)
                    .ThenInclude(c => c.Treatments)
                .Include(h => h.Consultation)
                    .ThenInclude(c => c.Animal)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        // Obtener todos los historiales médicos
        public async Task<List<MedicalHistory>> GetAllMedicalHistoriesAsync()
        {
            return await context.MedicalHistories
                .Include(h => h.Consultation)
                    .ThenInclude(c => c.Treatments)
                .Include(h => h.Consultation)
                    .ThenInclude(c => c.Animal)
                .ToListAsync();
        }

        // Eliminar historial médico
        public async Task<bool> DeleteMedicalHistoryAsync(int id)
        {
            var history = await context.MedicalHistories.FindAsync(id);
            if (history == null)
                return false;

            context.MedicalHistories.Remove(history);
            
            return true;
        }
    }
}