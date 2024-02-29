
using Microsoft.EntityFrameworkCore;
using ModuloApi.Entities;

namespace ModuloApi.Context
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {

        }

        public DbSet<Contato> Contatos { get; set; } // Tabela que sera criada no Banco, com base nass propriedades da tabela contato
    }
}
