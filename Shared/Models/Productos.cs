using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad18.Shared.Models
{
    public class Productos
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public double precio { get; set; }
        public string categoria { get; set; }
        public int disponibilidad { get; set; }

        public int ?PedidosId { get; set; }
        public virtual Pedidos? Pedidos { get; set; }
    }
}
