using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad18.Shared.Models
{
    public class Clientes
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string direcion { get; set; }
        public string facturacion { get; set; }

        public virtual ICollection<Pedidos>? Pedidos { get; set; }
    }
}
