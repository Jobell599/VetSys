using VetSys.Application.Dtos;
using VetSys.Infrastructure.Repositories;
using VetSys.Domain.Entities;

namespace VetSys.Application.Dtos
{
    public class VetService
    {
        private readonly UnityOfWork unitOfWork;

        public VetService(UnityOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Registrar un nuevo veterinario
        public async Task<VetDto> AddVetAsync(VetDto vetDto)
        {
            var vet = new Vet
            {
                Name = vetDto.Name
            };

            await unitOfWork.Vets.AddVetAsync(vet);
            await unitOfWork.CompleteAsync();

            vetDto.Id = vet.Id;
            return vetDto;
        }

        // Editar un veterinario existente
        public async Task<VetDto> UpdateVetAsync(VetDto vetDto)
        {
            var existingVet = await unitOfWork.Vets.GetVetByIdAsync(vetDto.Id);
            if (existingVet == null)
                throw new KeyNotFoundException("Vet not found");

            existingVet.Name = vetDto.Name;

            await unitOfWork.CompleteAsync();

            return vetDto;
        }

        // Eliminar un veterinario
        public async Task<bool> DeleteVetAsync(int id)
        {
            var result = await unitOfWork.Vets.DeleteVetAsync(id);
            await unitOfWork.CompleteAsync();
            return result;
        }

        // Obtener un veterinario por Id
        public async Task<VetDto> GetVetByIdAsync(int id)
        {
            var vet = await unitOfWork.Vets.GetVetByIdAsync(id);
            if (vet == null)
                return null;

            return new VetDto
            {
                Id = vet.Id,
                Name = vet.Name
            };
        }

        // Obtener todos los veterinarios
        public async Task<List<VetDto>> GetAllVetsAsync()
        {
            var vets = await unitOfWork.Vets.GetAllVetsAsync();

            return vets.Select(v => new VetDto
            {
                Id = v.Id,
                Name = v.Name
            }).ToList();
        }
    }
}