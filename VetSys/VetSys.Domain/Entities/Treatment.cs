using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSys.Domain.Entities
{
    public class Treatment
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int ConsultationId { get; set; }
        public string StarteDate { get; set; }
        public string EndDate { get; set; }
        public virtual ICollection<MedicineTreatment> MedicineTreatments { get; set; } = new List<MedicineTreatment>();

    }
}
