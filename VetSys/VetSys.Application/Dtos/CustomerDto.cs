using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSys.Domain.Entities
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public virtual ICollection<AnimalDto> Animals { get; set; } = new List<AnimalDto>();
    }
}
