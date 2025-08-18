using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSys.Application.Dtos
{
    public class MedicineTreatmentDto
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public int TreatmentId { get; set; }
        public virtual MedicineDto Medicine { get; set; }
        public virtual TreatmentDto Treatment { get; set; }

       
        
    }
}
