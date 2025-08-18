
using VetSys.Application.Dtos;
using VetSys.Infrastructure.Repositories;
using VetSys.Domain.Entities;

namespace VetSys.Application.Dtos
{
    public class MedicineService
    {
        private readonly UnityOfWork unitOfWork;

        public MedicineService(UnityOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Registrar un nuevo medicamento
        public async Task<MedicineDto> AddMedicineAsync(MedicineDto medicineDto)
        {
            var medicine = new Medicine
            {
                Name = medicineDto.Name,
                Dose = medicineDto.Dose,
                provider = medicineDto.provider
            };

            await unitOfWork.Medicines.AddMedicineAsync(medicine);
            await unitOfWork.CompleteAsync();

            medicineDto.Id = medicine.Id;
            return medicineDto;
        }

        // Editar un medicamento existente
        public async Task<MedicineDto> UpdateMedicineAsync(MedicineDto medicineDto)
        {
            var existingMedicine = await unitOfWork.Medicines.GetMedicineByIdAsync(medicineDto.Id);
            if (existingMedicine == null)
                throw new KeyNotFoundException("Medicine not found");

            existingMedicine.Name = medicineDto.Name;
            existingMedicine.Dose = medicineDto.Dose;
            existingMedicine.provider = medicineDto.provider;
            // Aquí podemos mapear MedicineTreatments si es necesario
            existingMedicine.MedicineTreatments = medicineDto.MedicineTreatments.Select(mt => new MedicineTreatment
            {
                Id = mt.Id,
                MedicineId = mt.MedicineId,
                TreatmentId = mt.TreatmentId
            }).ToList();

            unitOfWork.Medicines.UpdateMedicineAsync(existingMedicine);
            await unitOfWork.CompleteAsync();

            return medicineDto;
        }

        // Eliminar un medicamento
        public async Task<bool> DeleteMedicineAsync(int id)
        {
            var result = await unitOfWork.Medicines.DeleteMedicineAsync(id);
            await unitOfWork.CompleteAsync();
            return result;
        }

        // Obtener un medicamento por Id
        public async Task<MedicineDto> GetMedicineByIdAsync(int id)
        {
            var medicine = await unitOfWork.Medicines.GetMedicineByIdAsync(id);
            if (medicine == null)
                return null;

            return new MedicineDto
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Dose = medicine.Dose,
                provider = medicine.provider,
                MedicineTreatments = medicine.MedicineTreatments.Select(mt => new MedicineTreatmentDto
                {
                    Id = mt.Id,
                    MedicineId = mt.MedicineId,
                    TreatmentId = mt.TreatmentId
                }).ToList()
            };
        }

        // Obtener todos los medicamentos
        public async Task<List<MedicineDto>> GetAllMedicinesAsync()
        {
            var medicines = await unitOfWork.Medicines.GetAllMedicinesAsync();
            return medicines.Select(m => new MedicineDto
            {
                Id = m.Id,
                Name = m.Name,
                Dose = m.Dose,
                provider = m.provider,
                MedicineTreatments = m.MedicineTreatments.Select(mt => new MedicineTreatmentDto
                {
                    Id = mt.Id,
                    MedicineId = mt.MedicineId,
                    TreatmentId = mt.TreatmentId
                }).ToList()
            }).ToList();
        }
    }
}