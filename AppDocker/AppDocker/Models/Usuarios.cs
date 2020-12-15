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
        [Key]
        public Guid IdUser { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<UsuarioPhone> Phone { get; set; }



    }
}
