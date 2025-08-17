using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSys.Domain.Entities
{
    public class Consultation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public int AnimalId { get; set; }
        public virtual Animal Animal { get; set; }
        public int VetId { get; set; }
        public virtual Vet Vet { get; set; }

        public virtual ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();
        

    }
}
