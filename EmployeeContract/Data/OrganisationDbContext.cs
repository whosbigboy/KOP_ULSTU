using Microsoft.EntityFrameworkCore;
using EmployeeContract.Entities;

namespace EmployeeContract.Data;

public class OrganisationDbContext : DbContext
{
    public OrganisationDbContext(DbContextOptions<OrganisationDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeType> EmployeeTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настройка Employee
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Purpose).HasMaxLength(500);
            entity.Property(e => e.ReportDate).HasColumnType("timestamp without time zone");

            // Связь с типом подразделения
            entity.HasOne(e => e.EmployeeType)
                  .WithMany()
                  .HasForeignKey(e => e.EmployeeTypeId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Иерархия подразделений
            entity.HasOne(e => e.Parent)
                  .WithMany(e => e.Children)
                  .HasForeignKey(e => e.ParentId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Настройка EmployeeType
        modelBuilder.Entity<EmployeeType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Настройка типов данных для PostgreSQL
        modelBuilder.Entity<Employee>()
            .Property(e => e.ReportDate)
            .HasColumnType("timestamp without time zone");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>()
            .HaveColumnType("timestamp without time zone");

        configurationBuilder.Properties<DateTime?>()
            .HaveColumnType("timestamp without time zone");
    }
}
