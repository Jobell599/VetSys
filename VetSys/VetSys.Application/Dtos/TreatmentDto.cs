using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSys.Application.Dtos
{
    public class TreatmentDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int ConsultationId { get; set; }
        public string StarteDate { get; set; }
        public string EndDate { get; set; }
        public virtual ICollection<MedicineTreatmentDto> MedicineTreatments { get; set; } = new List<MedicineTreatmentDto>();

    }
}
