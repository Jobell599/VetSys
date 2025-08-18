
using VetSys.Application.Dtos;
using VetSys.Infrastructure.Repositories;
using VetSys.Domain.Entities;

namespace VetSys.Application.Dtos
{
    public class MedicineTreatmentService
    {
        private readonly UnityOfWork unitOfWork;

        public MedicineTreatmentService(UnityOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Registrar relación medicamento-tratamiento
        public async Task<MedicineTreatmentDto> AddMedicineTreatmentAsync(MedicineTreatmentDto mtDto)
        {
            var mt = new MedicineTreatment
            {
                MedicineId = mtDto.MedicineId,
                TreatmentId = mtDto.TreatmentId
            };

            await unitOfWork.MedicineTreatments.AddMedicineTreatmentAsync(mt);
            await unitOfWork.CompleteAsync();

            mtDto.Id = mt.Id;
            return mtDto;
        }

        // Eliminar relación medicamento-tratamiento
        public async Task<bool> DeleteMedicineTreatmentAsync(int id)
        {
            var result = await unitOfWork.MedicineTreatments.DeleteMedicineTreatmentAsync(id);
            await unitOfWork.CompleteAsync();
            return result;
        }

        // Obtener relación por Id
        public async Task<MedicineTreatmentDto> GetMedicineTreatmentByIdAsync(int id)
        {
            var mt = await unitOfWork.MedicineTreatments.GetMedicineTreatmentByIdAsync(id);
            if (mt == null)
                return null;

            return new MedicineTreatmentDto
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
                } : null,
                Treatment = mt.Treatment != null ? new TreatmentDto
                {
                    Id = mt.Treatment.Id,
                    Descripcion = mt.Treatment.Descripcion,
                    ConsultationId = mt.Treatment.ConsultationId,
                    StarteDate = mt.Treatment.StarteDate,
                    EndDate = mt.Treatment.EndDate
                } : null
            };
        }

        // Obtener todas las relaciones
        public async Task<List<MedicineTreatmentDto>> GetAllMedicineTreatmentsAsync()
        {
            var mts = await unitOfWork.MedicineTreatments.GetAllMedicineTreatmentsAsync();
            return mts.Select(mt => new MedicineTreatmentDto
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
                } : null,
                Treatment = mt.Treatment != null ? new TreatmentDto
                {
                    Id = mt.Treatment.Id,
                    Descripcion = mt.Treatment.Descripcion,
                    ConsultationId = mt.Treatment.ConsultationId,
                    StarteDate = mt.Treatment.StarteDate,
                    EndDate = mt.Treatment.EndDate
                } : null
            }).ToList();
        }

        // Obtener relaciones por TreatmentId
        public async Task<List<MedicineTreatmentDto>> GetByTreatmentIdAsync(int treatmentId)
        {
            var mts = await unitOfWork.MedicineTreatments.GetByTreatmentIdAsync(treatmentId);
            return mts.Select(mt => new MedicineTreatmentDto
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
                } : null,
                Treatment = mt.Treatment != null ? new TreatmentDto
                {
                    Id = mt.Treatment.Id,
                    Descripcion = mt.Treatment.Descripcion,
                    ConsultationId = mt.Treatment.ConsultationId,
                    StarteDate = mt.Treatment.StarteDate,
                    EndDate = mt.Treatment.EndDate
                } : null
            }).ToList();
        }

        // Obtener relaciones por MedicineId
        public async Task<List<MedicineTreatmentDto>> GetByMedicineIdAsync(int medicineId)
        {
            var mts = await unitOfWork.MedicineTreatments.GetByMedicineIdAsync(medicineId);
            return mts.Select(mt => new MedicineTreatmentDto
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
                } : null,
                Treatment = mt.Treatment != null ? new TreatmentDto
                {
                    Id = mt.Treatment.Id,
                    Descripcion = mt.Treatment.Descripcion,
                    ConsultationId = mt.Treatment.ConsultationId,
                    StarteDate = mt.Treatment.StarteDate,
                    EndDate = mt.Treatment.EndDate
                } : null
            }).ToList();
        }
    }
}