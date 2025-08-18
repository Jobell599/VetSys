using Microsoft.EntityFrameworkCore;
using VetSys.Domain.Entities;

namespace VetSys.Infrastructure.Repositories
{
    public class TreatmentService
    {
        private readonly UnityOfWork unitOfWork;

        public TreatmentService(UnityOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Registrar un nuevo tratamiento
        public async Task<TreatmentDto> AddTreatmentAsync(TreatmentDto treatmentDto)
        {
            var treatment = new Treatment
            {
                Descripcion = treatmentDto.Descripcion,
                ConsultationId = treatmentDto.ConsultationId,
                StarteDate = treatmentDto.StarteDate,
                EndDate = treatmentDto.EndDate
            };

            await unitOfWork.Treatments.AddTreatmentAsync(treatment);
            await unitOfWork.CompleteAsync();

            treatmentDto.Id = treatment.Id;
            return treatmentDto;
        }

        // Editar un tratamiento existente
        public async Task<TreatmentDto> UpdateTreatmentAsync(TreatmentDto treatmentDto)
        {
            var existingTreatment = await unitOfWork.Treatments.GetTreatmentByIdAsync(treatmentDto.Id);

            if (existingTreatment == null)
                throw new KeyNotFoundException("Treatment not found");

            existingTreatment.Descripcion = treatmentDto.Descripcion;
            existingTreatment.ConsultationId = treatmentDto.ConsultationId;
            existingTreatment.StarteDate = treatmentDto.StarteDate;
            existingTreatment.EndDate = treatmentDto.EndDate;

            // Opcional: actualizar relaciones de MedicineTreatment si se proporcionan
            existingTreatment.MedicineTreatments = treatmentDto.MedicineTreatments?
                .Select(mt => new MedicineTreatment
                {
                    Id = mt.Id,
                    MedicineId = mt.MedicineId,
                    TreatmentId = mt.TreatmentId
                }).ToList() ?? existingTreatment.MedicineTreatments;

            await unitOfWork.CompleteAsync();

            return treatmentDto;
        }

        // Eliminar un tratamiento
        public async Task<bool> DeleteTreatmentAsync(int id)
        {
            var result = await unitOfWork.Treatments.DeleteTreatmentAsync(id);
            await unitOfWork.CompleteAsync();
            return result;
        }

        // Obtener un tratamiento por Id
        public async Task<TreatmentDto> GetTreatmentByIdAsync(int id)
        {
            var treatment = await unitOfWork.Treatments.GetTreatmentByIdAsync(id);
            if (treatment == null)
                return null;

            return new TreatmentDto
            {
                Id = treatment.Id,
                Descripcion = treatment.Descripcion,
                ConsultationId = treatment.ConsultationId,
                StarteDate = treatment.StarteDate,
                EndDate = treatment.EndDate,
                MedicineTreatments = treatment.MedicineTreatments?
                    .Select(mt => new MedicineTreatmentDto
                    {
                        Id = mt.Id,
                        MedicineId = mt.MedicineId,
                        TreatmentId = mt.TreatmentId,
                        Medicine = mt.Medicine != null ? new MedicineDto
                        {
                            Id = mt.Medicine.Id,
                            Name = mt.Medicine.Name,
                            Dose = mt.Medicine.Dose,
                            provider = mt.Medicine.provider
                        } : null
                    }).ToList() ?? new List<MedicineTreatmentDto>()
            };
        }

        // Obtener todos los tratamientos
        public async Task<List<TreatmentDto>> GetAllTreatmentsAsync()
        {
            var treatments = await unitOfWork.Treatments.GetAllTreatmentsAsync();

            return treatments.Select(t => new TreatmentDto
            {
                Id = t.Id,
                Descripcion = t.Descripcion,
                ConsultationId = t.ConsultationId,
                StarteDate = t.StarteDate,
                EndDate = t.EndDate,
                MedicineTreatments = t.MedicineTreatments?
                    .Select(mt => new MedicineTreatmentDto
                    {
                        Id = mt.Id,
                        MedicineId = mt.MedicineId,
                        TreatmentId = mt.TreatmentId,
                        Medicine = mt.Medicine != null ? new MedicineDto
                        {
                            Id = mt.Medicine.Id,
                            Name = mt.Medicine.Name,
                            Dose = mt.Medicine.Dose,
                            provider = mt.Medicine.provider
                        } : null
                    }).ToList() ?? new List<MedicineTreatmentDto>()
            }).ToList();
        }
    }
}