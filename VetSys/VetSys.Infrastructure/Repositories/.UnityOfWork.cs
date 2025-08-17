using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSys.Infrastructure.Data;

namespace VetSys.Infrastructure.Repositories
{
    public class UnityOfWork
    {
        private readonly VetSysApplicationContext context;
        public AnimalRepository Animals { get; set; }
        public ConsultationRepository Constultations  { get; set; }
        public CustomerRepository Customers { get; set; }
        public MedicalHistoryRepository MedicalHistories { get; set; }
        public MedicineRepository Medicines { get; set; }
        public MedicineTreatmentRepository MedicineTreatments { get;set; }
        public TreatmentRepository Treatments { get; set; }
        public VetRepository Vets { get; set; }
        public UnityOfWork(VetSysApplicationContext context, AnimalRepository animalRepository, ConsultationRepository constultationRepository, CustomerRepository customerRepository,
           MedicalHistoryRepository medicalHistoryRepository, MedicineRepository medicineRepository, MedicineTreatmentRepository medicineTreatmentRepository, TreatmentRepository treatmentRepository,
            VetRepository vetRepository)
        {
            this.context = context;
            Animals = animalRepository;
            Constultations = constultationRepository;
            Customers = customerRepository;
            MedicalHistories = medicalHistoryRepository;
            Medicines = medicineRepository;
            Treatments = treatmentRepository;
            MedicineTreatments = medicineTreatmentRepository;
            Vets = vetRepository;

        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
