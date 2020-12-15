using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDocker.Models
{
    public class UsuarioPhone
    {
        public Guid UsuariosId { get; set; }
        public Usuarios Usuario { get; set; }

        public Guid PhonesId { get; set; }
        public Phones Phone { get; set; }
    }
}
