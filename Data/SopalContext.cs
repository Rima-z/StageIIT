using iit.Models;
using Microsoft.EntityFrameworkCore;


namespace iit.Data
{
    public class SopalContext : DbContext
    {
        public SopalContext(DbContextOptions<SopalContext> options) : base(options)
        {

        }

        //Added DBSet for Categories Table
        public DbSet<Fonction> Fonctions { get; set; }
        public DbSet<Menus> Menus { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<ProfilFonction> ProfilFonction { get; set; }

        public DbSet<Droits> Droits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Droits>()
                .HasOne(d => d.ProfilFonction)
                .WithMany(pf => pf.Droits)
                .HasForeignKey(d => d.CodPrf);

            modelBuilder.Entity<Droits>()
                .HasOne(d => d.Fonction)
                .WithMany(f => f.Droits)
                .HasForeignKey(d => d.CodeF);
        }

        public DbSet<ProfilMenu> ProfilMenu { get; set; }




    }
}
