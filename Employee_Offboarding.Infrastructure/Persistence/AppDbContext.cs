using Employee_Offboarding.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee_Offboarding.Infrastructure.Persistence
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<ServiceCenter> ServiceCenters => Set<ServiceCenter>();
        public DbSet<DepartmentResponsiblePerson> DepartmentResponsiblePersons => Set<DepartmentResponsiblePerson>();
        public DbSet<ServiceCenterResponsiblePerson> ServiceCenterResponsiblePersons => Set<ServiceCenterResponsiblePerson>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<ClearenceForm> ClearanceForms => Set<ClearenceForm>();
        public DbSet<ClearenceFormItem> ClearenceFormItems => Set<ClearenceFormItem>();
        public DbSet<ClearenceFormDepartment> ClearenceFormDepartments => Set<ClearenceFormDepartment>();
        public DbSet<FormAssignment> FormAssignments => Set<FormAssignment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.ServiceCenter)
                .WithMany()
                .HasForeignKey(e =>e.ServiceCenterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .ToTable(tb =>
                {
                    tb.HasCheckConstraint("Ck_Employee_OneOrg",
                        @"(CASE WHEN [DepartmentId] IS NULL 0 ELSE 1 END) +
                          (CASE WHEN [ServiceCenterId] IS NULL 0 ELSE 1 END) = 1");
                });

            modelBuilder.Entity<Employee>().HasIndex(e => e.DepartmentId);
            modelBuilder.Entity<Employee>().HasIndex(e => e.ServiceCenterId);

            modelBuilder.Entity<Department>()
                .Property(d => d.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<ServiceCenter>()
                .Property(s => s.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Item>()
                .Property(i => i.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Item>()
                .HasIndex(i => new {i.DepartmentId, i.Name})
                .IsUnique();

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Department)
                .WithMany(d=> d.Items)
                .HasForeignKey(i=>i.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClearenceForm>()
                .HasOne(f=> f.Employee)
                .WithMany(e=> e.ClearenceForms)
                .HasForeignKey(f => f.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClearenceForm>()
                .HasOne(f=> f.InitiatedByUser)
                .WithMany()
                .HasForeignKey(f=>f.InitiatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClearenceForm>()
                .HasOne(f=>f.ServiceCenter)
                .WithMany()
                .HasForeignKey(f=> f.ServiceCenterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClearenceForm>().HasIndex(f => f.Status);
            modelBuilder.Entity<ClearenceForm>().HasIndex(f => new { f.ServiceCenterId, f.CreatedAt });
            modelBuilder.Entity<ClearenceForm>().Property(f => f.CreatedAt).HasPrecision(0);
            modelBuilder.Entity<ClearenceForm>().Property(f => f.ConfirmedAt).HasPrecision(0);
            modelBuilder.Entity<ClearenceForm>().Property(f => f.CompletedAt).HasPrecision(0);


            modelBuilder.Entity<ClearenceFormDepartment>()
                .HasKey(cd => new { cd.ClearenceFormId, cd.DepartmentId });

            modelBuilder.Entity<ClearenceFormDepartment>()
                .HasOne(cd => cd.ClearenceForm)
                .WithMany(cf => cf.clearenceFormDepartments)
                .HasForeignKey(cd => cd.ClearenceFormId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
