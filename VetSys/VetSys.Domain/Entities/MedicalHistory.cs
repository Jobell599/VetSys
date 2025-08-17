using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSys.Domain.Entities
{
    public class MedicalHistory
    {
        public int Id { get; set; }
        public int ConsultationId { get; set; }
        public virtual Consultation Consultation { get; set; }


    }
}
