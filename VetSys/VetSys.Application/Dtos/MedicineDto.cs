using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSys.Application.Dtos
{
    public class MedicineDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dose { get; set; }
        public string provider { get; set; }

        public virtual ICollection<MedicineTreatmentDto> MedicineTreatments { get; set; } = new List<MedicineTreatmentDto>();

    }
}
