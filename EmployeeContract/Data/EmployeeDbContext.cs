using Microsoft.EntityFrameworkCore;
using EmployeeContract.Entities;

namespace EmployeeContract.Data;

public class EmployeeDbContext : DbContext
{
    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeOrg> EmployeeOrgs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Указание имен таблиц
        modelBuilder.Entity<Employee>().ToTable("employee");
        modelBuilder.Entity<EmployeeOrg>().ToTable("employee_post");

        // Настройка EmployeePost с указанием имен столбцов
        modelBuilder.Entity<EmployeeOrg>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                  .HasColumnName("id"); // явно указываем имя столбца

            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("name"); // явно указываем имя столбца

            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Настройка Employee с указанием имен столбцов
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                  .HasColumnName("id");

            entity.Property(e => e.FIO)
                  .IsRequired()
                  .HasMaxLength(200)
                  .HasColumnName("fio");

            entity.Property(e => e.Posts)
                  .HasMaxLength(500)
                  .HasColumnName("posts");

            entity.Property(e => e.EmployeeOrgId)
                  .HasColumnName("employee_org_id");

            entity.Property(e => e.WorkExp)
                   .HasColumnName("WorkExp");

            // Связь с орг работника
            entity.HasOne(e => e.EmployeeOrg)
                  .WithMany()
                  .HasForeignKey(e => e.EmployeeOrgId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Настройка EmployeePost
        modelBuilder.Entity<EmployeeOrg>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                  .HasColumnName("id");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100)
                  .HasColumnName("name");
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Настройка типов данных для PostgreSQL
        modelBuilder.Entity<Employee>()
            .Property(e => e.WorkExp)
            .HasColumnName("promotion_date");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>()
            .HaveColumnType("timestamp without time zone");

        configurationBuilder.Properties<DateTime?>()
            .HaveColumnType("timestamp without time zone");
    }
    public async Task<bool> TestDatabaseConnection()
    {
        try
        {
            var optionsBuilder = new DbContextOptionsBuilder<EmployeeDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=employee_db;Username=postgres;Password=postgres");

            using var context = new EmployeeDbContext(optionsBuilder.Options);

            // Простая проверка - попытка выполнить запрос
            var canConnect = await context.Database.CanConnectAsync();

            if (canConnect)
            {
                Console.WriteLine("Подключение к базе данных успешно!");
                return true;
            }
            else
            {
                Console.WriteLine("Не удалось подключиться к базе данных");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка подключения: {ex.Message}");
            return false;
        }

    }
}
