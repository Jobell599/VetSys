using Microsoft.EntityFrameworkCore;
using VetSys.Domain.Entities;

namespace VetSys.Infrastructure.Repositories
{
    public class ConsultationService
    {
        private readonly UnityOfWork unitOfWork;

        public ConsultationService(UnityOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Registrar nueva consulta
        public async Task<ConsultationDto> AddConsultationAsync(ConsultationDto consultationDto)
        {
            var consultation = new Consultation
            {
                Date = consultationDto.Date,
                Description = consultationDto.Description,
                AnimalId = consultationDto.AnimalId,
                VetId = consultationDto.VetId
            };

            await unitOfWork.Constultations.AddConsultationAsync(consultation);
            await unitOfWork.CompleteAsync();

            consultationDto.Id = consultation.Id;
            return consultationDto;
        }

        // Editar consulta existente
        public async Task<ConsultationDto> UpdateConsultationAsync(ConsultationDto consultationDto)
        {
            var existing = await unitOfWork.Constultations.GetConsultationByIdAsync(consultationDto.Id);
            if (existing == null)
                throw new KeyNotFoundException("Consultation not found");

            existing.Date = consultationDto.Date;
            existing.Description = consultationDto.Description;
            existing.AnimalId = consultationDto.AnimalId;
            existing.VetId = consultationDto.VetId;

            unitOfWork.Constultations.UpdateConsultationAsync(existing);
            await unitOfWork.CompleteAsync();

            return consultationDto;
        }

        // Eliminar consulta
        public async Task<bool> DeleteConsultationAsync(int id)
        {
            var result = await unitOfWork.Constultations.DeleteConsultationAsync(id);
            await unitOfWork.CompleteAsync();
            return result;
        }

        // Obtener consulta por Id
        public async Task<ConsultationDto> GetConsultationByIdAsync(int id)
        {
            var consultation = await unitOfWork.Constultations.GetConsultationByIdAsync(id);
            if (consultation == null)
                return null;

            var consultationDto = new ConsultationDto
            {
                Id = consultation.Id,
                Date = consultation.Date,
                Description = consultation.Description,
                AnimalId = consultation.AnimalId,
                Animal = consultation.Animal != null ? new AnimalDto
                {
                    Id = consultation.Animal.Id,
                    Name = consultation.Animal.Name,
                    Kind = consultation.Animal.Kind,
                    Race = consultation.Animal.Race,
                    Sex = consultation.Animal.Sex,
                    Birth = consultation.Animal.Birth,
                    Weight = consultation.Animal.Weight,
                    CustomerId = consultation.Animal.CustomerId
                } : null,
                VetId = consultation.VetId,
                Vet = consultation.Vet != null ? new VetDto
                {
                    Id = consultation.Vet.Id,
                    Name = consultation.Vet.Name
                } : null,
                Treatments = consultation.Treatments.Select(t => new TreatmentDto
                {
                    Id = t.Id,
                    Descripcion = t.Descripcion,
                    ConsultationId = t.ConsultationId,
                    StarteDate = t.StarteDate,
                    EndDate = t.EndDate
                }).ToList()
            };

            return consultationDto;
        }

        // Obtener todas las consultas
        public async Task<List<ConsultationDto>> GetAllConsultationsAsync()
        {
            var consultations = await unitOfWork.Constultations.GetAllConsultationsAsync();
            return consultations.Select(c => new ConsultationDto
            {
                Id = c.Id,
                Date = c.Date,
                Description = c.Description,
                AnimalId = c.AnimalId,
                Animal = c.Animal != null ? new AnimalDto
                {
                    Id = c.Animal.Id,
                    Name = c.Animal.Name,
                    Kind = c.Animal.Kind,
                    Race = c.Animal.Race,
                    Sex = c.Animal.Sex,
                    Birth = c.Animal.Birth,
                    Weight = c.Animal.Weight,
                    CustomerId = c.Animal.CustomerId
                } : null,
                VetId = c.VetId,
                Vet = c.Vet != null ? new VetDto
                {
                    Id = c.Vet.Id,
                    Name = c.Vet.Name
                } : null,
                Treatments = c.Treatments.Select(t => new TreatmentDto
                {
                    Id = t.Id,
                    Descripcion = t.Descripcion,
                    ConsultationId = t.ConsultationId,
                    StarteDate = t.StarteDate,
                    EndDate = t.EndDate
                }).ToList()
            }).ToList();
        }
    }
}