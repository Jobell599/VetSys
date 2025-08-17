using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSys.Domain.Entities
{
    public class MedicineTreatment
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public int TreatmentId { get; set; }
        public virtual Medicine Medicine { get; set; }
        public virtual Treatment Treatment { get; set; }

       
        
    }
}
