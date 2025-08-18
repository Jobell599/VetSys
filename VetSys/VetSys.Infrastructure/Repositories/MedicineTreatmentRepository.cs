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
    public class MedicineTreatmentRepository
    {
        private readonly VetSysApplicationContext context;

        public MedicineTreatmentRepository(VetSysApplicationContext context)
        {
            this.context = context;
        }

        // Registrar relación medicamento-tratamiento
        public async Task<MedicineTreatment> AddMedicineTreatmentAsync(MedicineTreatment mt)
        {
            await context.MedicineTreatments.AddAsync(mt);
            
            return mt;
        }

        // Eliminar relación medicamento-tratamiento
        public async Task<bool> DeleteMedicineTreatmentAsync(int id)
        {
            var mt = await context.MedicineTreatments.FindAsync(id);
            if (mt == null)
                return false;

            context.MedicineTreatments.Remove(mt);
            
            return true;
        }

        // Obtener relación por Id
        public async Task<MedicineTreatment> GetMedicineTreatmentByIdAsync(int id)
        {
            return await context.MedicineTreatments
                .FirstOrDefaultAsync(mt => mt.Id == id);
        }

        // Obtener todas las relaciones
        public async Task<List<MedicineTreatment>> GetAllMedicineTreatmentsAsync()
        {
            return await context.MedicineTreatments
                .Include(mt => mt.Medicine)
                .Include(mt => mt.Treatment)
                .ToListAsync();
        }

        // Obtener relaciones por TreatmentId
        public async Task<List<MedicineTreatment>> GetByTreatmentIdAsync(int treatmentId)
        {
            return await context.MedicineTreatments
                .Include(mt => mt.Medicine)
                .Where(mt => mt.TreatmentId == treatmentId)
                .ToListAsync();
        }

        // Obtener relaciones por MedicineId
        public async Task<List<MedicineTreatment>> GetByMedicineIdAsync(int medicineId)
        {
            return await context.MedicineTreatments
                .Include(mt => mt.Treatment)
                .Where(mt => mt.MedicineId == medicineId)
                .ToListAsync();
        }
    }
}