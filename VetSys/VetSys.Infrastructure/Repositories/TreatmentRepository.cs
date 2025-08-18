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
    public class TreatmentRepository
    {
        private readonly VetSysApplicationContext context;

        public TreatmentRepository(VetSysApplicationContext context)
        {
            this.context = context;
        }

        // Registrar un nuevo tratamiento
        public async Task<Treatment> AddTreatmentAsync(Treatment treatment)
        {
            await context.Treatments.AddAsync(treatment);
            
            return treatment;
        }

        // Editar un tratamiento existente
        public async Task<Treatment> UpdateTreatmentAsync(Treatment treatment)
        {
            var existingTreatment = await context.Treatments
                .Include(t => t.MedicineTreatments)
                .FirstOrDefaultAsync(t => t.Id == treatment.Id);

            if (existingTreatment == null)
                throw new KeyNotFoundException("Treatment not found");

            existingTreatment.Descripcion = treatment.Descripcion;
            existingTreatment.ConsultationId = treatment.ConsultationId;
            existingTreatment.StarteDate = treatment.StarteDate;
            existingTreatment.EndDate = treatment.EndDate;
            existingTreatment.MedicineTreatments = treatment.MedicineTreatments;

            context.Treatments.Update(existingTreatment);
            
            return existingTreatment;
        }

        // Eliminar un tratamiento
        public async Task<bool> DeleteTreatmentAsync(int id)
        {
            var treatment = await context.Treatments.FindAsync(id);
            if (treatment == null)
                return false;

            context.Treatments.Remove(treatment);
            
            return true;
        }

        // Obtener un tratamiento por Id
        public async Task<Treatment> GetTreatmentByIdAsync(int id)
        {
            return await context.Treatments
                .Include(t => t.MedicineTreatments)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        // Obtener todos los tratamientos
        public async Task<List<Treatment>> GetAllTreatmentsAsync()
        {
            return await context.Treatments
                .Include(t => t.MedicineTreatments)
                .ToListAsync();
        }
    }
}