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
            Phones = new HashSet<Phones>();
        }
        public virtual Guid IdUser { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }

        public virtual string Created { get; set; }

        public virtual string Modified { get; set; }

        public virtual string Last_login { get; set; } 
   

        public virtual ICollection<Phones> Phones { get; set; }
    
    }

}
