using Microsoft.EntityFrameworkCore;

namespace KioskoW.Data
{
    public class KioskoWContext : DbContext
    {
        public KioskoWContext (DbContextOptions<KioskoWContext> options)
            : base(options)
        {
        }

        public DbSet<KioskoApiEntidad.KioskoEntidad> Kioskos { get; set; }

        public DbSet<KioskoApiEntidad.UsuarioEntidad> Usuarios { get; set; }
    }
}
