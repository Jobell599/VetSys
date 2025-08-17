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
    public class MedicineRepository
    {
        private readonly VetSysApplicationContext context;

        public MedicineRepository(VetSysApplicationContext context)
        {
            this.context = context;
        }

        // Registrar un nuevo medicamento
        public async Task<Medicine> AddMedicineAsync(Medicine medicine)
        {
            await context.Medicines.AddAsync(medicine);
            await context.SaveChangesAsync();
            return medicine;
        }

        // Editar un medicamento existente
        public async Task<Medicine> UpdateMedicineAsync(Medicine medicine)
        {
            var existingMedicine = await context.Medicines
                .Include(m => m.MedicineTreatments)
                .FirstOrDefaultAsync(m => m.Id == medicine.Id);

            if (existingMedicine == null)
                throw new KeyNotFoundException("Medicine not found");

            existingMedicine.Name = medicine.Name;
            existingMedicine.Dose = medicine.Dose;
            existingMedicine.provider = medicine.provider;
            existingMedicine.MedicineTreatments = medicine.MedicineTreatments;

            context.Medicines.Update(existingMedicine);
            await context.SaveChangesAsync();
            return existingMedicine;
        }

        // Eliminar un medicamento
        public async Task<bool> DeleteMedicineAsync(int id)
        {
            var medicine = await context.Medicines.FindAsync(id);
            if (medicine == null)
                return false;

            context.Medicines.Remove(medicine);
            await context.SaveChangesAsync();
            return true;
        }

        // Obtener un medicamento por Id
        public async Task<Medicine> GetMedicineByIdAsync(int id)
        {
            return await context.Medicines
                .Include(m => m.MedicineTreatments)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        // Obtener todos los medicamentos
        public async Task<List<Medicine>> GetAllMedicinesAsync()
        {
            return await context.Medicines
                .Include(m => m.MedicineTreatments)
                .ToListAsync();
        }
    }
}