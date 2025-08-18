using VetSys.Domain.Entities;
using VetSys.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace VetSys.Infrastructure.Repositories
{
    public class AnimalService
    {
        private readonly UnityOfWork unitOfWork;

        public AnimalService(UnityOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Registrar un nuevo animal
        public async Task<AnimalDto> AddAnimalAsync(AnimalDto animalDto)
        {
            var animal = new Animal
            {
                Name = animalDto.Name,
                Kind = animalDto.Kind,
                Race = animalDto.Race,
                Sex = animalDto.Sex,
                Birth = animalDto.Birth,
                Weight = animalDto.Weight,
                CustomerId = animalDto.CustomerId
            };

            await unitOfWork.Animals.AddAnimalAsync(animal);
            await unitOfWork.CompleteAsync();

            // Convertir a DTO
            animalDto.Id = animal.Id;
            return animalDto;
        }

        // Editar un animal existente
        public async Task<AnimalDto> UpdateAnimalAsync(AnimalDto animalDto)
        {
            var existingAnimal = await unitOfWork.Animals.GetAnimalByIdAsync(animalDto.Id);
            if (existingAnimal == null)
                throw new KeyNotFoundException("Animal not found");

            existingAnimal.Name = animalDto.Name;
            existingAnimal.Kind = animalDto.Kind;
            existingAnimal.Race = animalDto.Race;
            existingAnimal.Sex = animalDto.Sex;
            existingAnimal.Birth = animalDto.Birth;
            existingAnimal.Weight = animalDto.Weight;
            existingAnimal.CustomerId = animalDto.CustomerId;

            unitOfWork.Animals.UpdateAnimalAsync(existingAnimal);
            await unitOfWork.CompleteAsync();

            return animalDto;
        }

        // Eliminar un animal
        public async Task<bool> DeleteAnimalAsync(int id)
        {
            var result = await unitOfWork.Animals.DeleteAnimalAsync(id);
            await unitOfWork.CompleteAsync();
            return result;
        }

        // Obtener un animal por Id
        public async Task<AnimalDto> GetAnimalByIdAsync(int id)
        {
            var animal = await unitOfWork.Animals.GetAnimalByIdAsync(id);
            if (animal == null)
                return null;

            return new AnimalDto
            {
                Id = animal.Id,
                Name = animal.Name,
                Kind = animal.Kind,
                Race = animal.Race,
                Sex = animal.Sex,
                Birth = animal.Birth,
                Weight = animal.Weight,
                CustomerId = animal.CustomerId
            };
        }

        // Obtener todos los animales
        public async Task<List<AnimalDto>> GetAllAnimalsAsync()
        {
            var animals = await unitOfWork.Animals.GetAllAnimalsAsync();
            return animals.Select(a => new AnimalDto
            {
                Id = a.Id,
                Name = a.Name,
                Kind = a.Kind,
                Race = a.Race,
                Sex = a.Sex,
                Birth = a.Birth,
                Weight = a.Weight,
                CustomerId = a.CustomerId
            }).ToList();
        }

        // Buscar animales por nombre
        public async Task<List<AnimalDto>> SearchAnimalsByNameAsync(string name)
        {
            var animals = await unitOfWork.Animals.SearchAnimalsByNameAsync(name);
            return animals.Select(a => new AnimalDto
            {
                Id = a.Id,
                Name = a.Name,
                Kind = a.Kind,
                Race = a.Race,
                Sex = a.Sex,
                Birth = a.Birth,
                Weight = a.Weight,
                CustomerId = a.CustomerId
            }).ToList();
        }

        // Buscar animales por especie
        public async Task<List<AnimalDto>> SearchAnimalsByKindAsync(string kind)
        {
            var animals = await unitOfWork.Animals.SearchAnimalsByKindAsync(kind);
            return animals.Select(a => new AnimalDto
            {
                Id = a.Id,
                Name = a.Name,
                Kind = a.Kind,
                Race = a.Race,
                Sex = a.Sex,
                Birth = a.Birth,
                Weight = a.Weight,
                CustomerId = a.CustomerId
            }).ToList();
        }

        // Buscar animales por cliente propietario
        public async Task<List<AnimalDto>> SearchAnimalsByCustomerAsync(int customerId)
        {
            var animals = await unitOfWork.Animals.SearchAnimalsByCustomerAsync(customerId);
            return animals.Select(a => new AnimalDto
            {
                Id = a.Id,
                Name = a.Name,
                Kind = a.Kind,
                Race = a.Race,
                Sex = a.Sex,
                Birth = a.Birth,
                Weight = a.Weight,
                CustomerId = a.CustomerId
            }).ToList();
        }
    }
}