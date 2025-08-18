using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSys.Application.Dtos
{
    public class ConsultationDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public int AnimalId { get; set; }
        public virtual AnimalDto Animal { get; set; }
        public int VetId { get; set; }
        public virtual VetDto Vet { get; set; }

        public virtual ICollection<TreatmentDto> Treatments { get; set; } = new List<TreatmentDto>();
        

    }
}
