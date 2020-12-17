using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppDocker.Models
{
    public class Usuarios
    {
        public Usuarios()
        {
            phones = new HashSet<Phones>();
        }
        public virtual Guid IdUser { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }

        public virtual ICollection<Phones> phones { get; set; }
    
    }

}
