using Actividad18.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Actividad18.Server.Contexto
{
    public class ContextoTiendaLinea : DbContext
    {
        public ContextoTiendaLinea(DbContextOptions<ContextoTiendaLinea> options): base (options) 
        {
            
        }
        public DbSet<Clientes> Clientes { get; set;}
        public DbSet<Productos> Productos { get; set;}
        public DbSet<Pedidos> Pedidos { get; set;}
    }
}
