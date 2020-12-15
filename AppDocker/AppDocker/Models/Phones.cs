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
        public Guid IdPhone { get; set; }
        public string Number { get; set; }
        public string Ddd { get; set; }

        public List<UsuarioPhone> Usuario { get; set; }

    }
}
