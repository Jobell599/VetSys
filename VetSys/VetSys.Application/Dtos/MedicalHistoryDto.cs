using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSys.Application.Dtos
{
    public class MedicalHistoryDto
    {
        public int Id { get; set; }
        public int ConsultationId { get; set; }
        public virtual ConsultationDto Consultation { get; set; }


    }
}
