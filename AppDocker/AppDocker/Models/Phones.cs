using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppDocker.Models
{
    public class Phones
    {
        [Key]
        public virtual int IdPhone { get; set; }
        public virtual string Number { get; set; }
        public virtual string DDD { get; set; }

        [ForeignKey("Usuarios")]
        public Guid UsuarioFK { get; set; }

        public virtual Usuarios Usuario { get; set; }






    }
}
