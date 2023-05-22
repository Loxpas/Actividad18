using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad18.Shared.Models
{
    public class Pedidos
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public double total { get; set; }
        public string direccion { get; set; }

        public string tarjeta { get; set; }

        public virtual ICollection<Productos>? Productos { get; set; }

        public int ClientesId { get; set; }
        public virtual Clientes? Clientes { get; set; }

        
    }
}
