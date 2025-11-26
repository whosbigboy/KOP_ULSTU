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

        // Указание имен таблиц согласно вашей БД
        modelBuilder.Entity<Employee>().ToTable("employees");
        modelBuilder.Entity<EmployeeOrg>().ToTable("employee_orgs");

        // Настройка EmployeeOrg (employee_orgs)
        modelBuilder.Entity<EmployeeOrg>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                  .HasColumnName("id");

            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("name");

            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Настройка Employee (employees)
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

            // СООТВЕТСТВИЕ С БД: work_exp -> WorkExp
            entity.Property(e => e.WorkExp)
                  .HasColumnName("work_exp")
                  .HasDefaultValue(0);

            // Связь с подразделением
            entity.HasOne(e => e.EmployeeOrg)
                  .WithMany()
                  .HasForeignKey(e => e.EmployeeOrgId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
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