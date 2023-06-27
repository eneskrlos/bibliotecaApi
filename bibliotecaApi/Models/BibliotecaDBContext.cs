using Microsoft.EntityFrameworkCore;

namespace bibliotecaApi.Models
{
    public class BibliotecaDBContext :DbContext
    {
        public virtual DbSet<Libro> Libros { get; set; }
        public virtual DbSet<Lector> Lectores { get; set; }
        public virtual DbSet<Prestamo> Prestamos { get; set; }

        public BibliotecaDBContext()
        {
            
        }

        public BibliotecaDBContext(DbContextOptions<BibliotecaDBContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Libro>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Libro__3213E83F6980B911");

                entity.ToTable("Libro");

                entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
                entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
                entity.Property(e => e.ISBN)
                .HasMaxLength(255)
                .IsUnicode(false);
                entity.Property(e => e.Prestado)
                .HasColumnName("prestado");

                //entity.HasMany(l => l.Prestamos).WithOne(p => p.LibroNavigation);
            });

            modelBuilder.Entity<Lector>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Lector__3213E83F1DEED1A4");

                entity.ToTable("Lector");

                entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
                entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);

                //entity.HasMany(l => l.Prestamos).WithOne(p => p.LectorNavigation);

            });

            modelBuilder.Entity<Prestamo>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__Prestamo__3213E83F1980B911");

                entity.ToTable("Prestamo");

                entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
                entity.Property(e => e.FechaPrestamo).HasColumnType("datetime");

                entity.HasOne(p => p.LectorNavigation)
                .WithMany(l => l.Prestamos)
                .HasForeignKey(p => p.LectorId).HasConstraintName("FK_Prest_Lector"); ;

                entity.HasOne(p => p.LibroNavigation)
                .WithMany(l => l.Prestamos)
                .HasForeignKey(p => p.IdLibro).HasConstraintName("FK_Prest_Libro"); ;
            });


            Sedd(modelBuilder);

        }


        protected void Sedd(ModelBuilder modelBuilder) 
        {
            
            modelBuilder.Entity<Libro>().HasData(
                new Libro { Id = Guid.NewGuid(), Nombre = "Pinocho", ISBN = "1234567890", Prestado = false }
            );

            modelBuilder.Entity<Lector>().HasData(
                new Lector { Id = Guid.NewGuid(), Nombre = "Ernesto" }
            );
            modelBuilder.Entity<Lector>().HasData(
                new Lector { Id = Guid.NewGuid(), Nombre = "Carlos" }
            );
        }

    }
}
