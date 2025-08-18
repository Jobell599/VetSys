
using VetSys.Application.Dtos;
using VetSys.Infrastructure.Repositories;
using VetSys.Domain.Entities;

namespace VetSys.Application.Dtos
{
    public class MedicalHistoryService
    {
        private readonly UnityOfWork unitOfWork;

        public MedicalHistoryService(UnityOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Registrar historial médico
        public async Task<MedicalHistoryDto> AddMedicalHistoryAsync(MedicalHistoryDto historyDto)
        {
            var history = new MedicalHistory
            {
                ConsultationId = historyDto.ConsultationId
            };

            await unitOfWork.MedicalHistories.AddMedicalHistoryAsync(history);
            await unitOfWork.CompleteAsync();

            historyDto.Id = history.Id;
            return historyDto;
        }

        // Obtener historial médico por Id
        public async Task<MedicalHistoryDto> GetMedicalHistoryByIdAsync(int id)
        {
            var history = await unitOfWork.MedicalHistories.GetMedicalHistoryByIdAsync(id);
            if (history == null)
                return null;

            return new MedicalHistoryDto
            {
                Id = history.Id,
                ConsultationId = history.ConsultationId,
                Consultation = history.Consultation != null ? new ConsultationDto
                {
                    Id = history.Consultation.Id,
                    Date = history.Consultation.Date,
                    Description = history.Consultation.Description,
                    AnimalId = history.Consultation.AnimalId,
                    Animal = history.Consultation.Animal != null ? new AnimalDto
                    {
                        Id = history.Consultation.Animal.Id,
                        Name = history.Consultation.Animal.Name,
                        Kind = history.Consultation.Animal.Kind,
                        Race = history.Consultation.Animal.Race,
                        Sex = history.Consultation.Animal.Sex,
                        Birth = history.Consultation.Animal.Birth,
                        Weight = history.Consultation.Animal.Weight,
                        CustomerId = history.Consultation.Animal.CustomerId
                    } : null,
                    VetId = history.Consultation.VetId,
                    Vet = history.Consultation.Vet != null ? new VetDto
                    {
                        Id = history.Consultation.Vet.Id,
                        Name = history.Consultation.Vet.Name
                    } : null,
                    Treatments = history.Consultation.Treatments.Select(t => new TreatmentDto
                    {
                        Id = t.Id,
                        Descripcion = t.Descripcion,
                        ConsultationId = t.ConsultationId,
                        StarteDate = t.StarteDate,
                        EndDate = t.EndDate
                    }).ToList()
                } : null
            };
        }

        // Obtener todos los historiales médicos
        public async Task<List<MedicalHistoryDto>> GetAllMedicalHistoriesAsync()
        {
            var histories = await unitOfWork.MedicalHistories.GetAllMedicalHistoriesAsync();
            return histories.Select(h => new MedicalHistoryDto
            {
                Id = h.Id,
                ConsultationId = h.ConsultationId,
                Consultation = h.Consultation != null ? new ConsultationDto
                {
                    Id = h.Consultation.Id,
                    Date = h.Consultation.Date,
                    Description = h.Consultation.Description,
                    AnimalId = h.Consultation.AnimalId,
                    Animal = h.Consultation.Animal != null ? new AnimalDto
                    {
                        Id = h.Consultation.Animal.Id,
                        Name = h.Consultation.Animal.Name,
                        Kind = h.Consultation.Animal.Kind,
                        Race = h.Consultation.Animal.Race,
                        Sex = h.Consultation.Animal.Sex,
                        Birth = h.Consultation.Animal.Birth,
                        Weight = h.Consultation.Animal.Weight,
                        CustomerId = h.Consultation.Animal.CustomerId
                    } : null,
                    VetId = h.Consultation.VetId,
                    Vet = h.Consultation.Vet != null ? new VetDto
                    {
                        Id = h.Consultation.Vet.Id,
                        Name = h.Consultation.Vet.Name
                    } : null,
                    Treatments = h.Consultation.Treatments.Select(t => new TreatmentDto
                    {
                        Id = t.Id,
                        Descripcion = t.Descripcion,
                        ConsultationId = t.ConsultationId,
                        StarteDate = t.StarteDate,
                        EndDate = t.EndDate
                    }).ToList()
                } : null
            }).ToList();
        }

        // Eliminar historial médico
        public async Task<bool> DeleteMedicalHistoryAsync(int id)
        {
            var result = await unitOfWork.MedicalHistories.DeleteMedicalHistoryAsync(id);
            await unitOfWork.CompleteAsync();
            return result;
        }
    }
}