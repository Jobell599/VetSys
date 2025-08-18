using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSys.Domain.Entities
{
    public class AnimalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public string Race { get; set; }
        public string Sex { get; set; }
        public DateTime Birth {  get; set; }
        public int Weight { get; set; }
        public int CustomerId { get; set; }
        public virtual CustomerDto Customer { get; set; }

    }
}
