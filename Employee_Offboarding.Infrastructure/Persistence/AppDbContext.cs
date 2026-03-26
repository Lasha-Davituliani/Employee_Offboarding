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

            modelBuilder.Entity<ClearenceFormDepartment>()
                .HasOne(cd => cd.Department)
                .WithMany()
                .HasForeignKey(cd => cd.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClearenceFormItem>()
                .HasOne(fi => fi.ClearanceForm)
                .WithMany(f => f.Items)
                .HasForeignKey(fi => fi.ClearanceFormId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClearenceFormItem>()
                .HasOne(fi => fi.Item)
                .WithMany(i => i.ClearanceFormItems)
                .HasForeignKey(fi => fi.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClearenceFormItem>()
                .HasIndex(fi => new { fi.ClearanceFormId, fi.ItemId })
                .IsUnique();

            modelBuilder.Entity<ClearenceFormItem>()
                .Property(x => x.UpdatedAt)
                .HasPrecision(0);

            modelBuilder.Entity<FormAssignment>(e =>
            {
                e.HasIndex(x => x.Token).IsUnique();
                e.Property(x => x.Email).HasMaxLength(100).IsRequired();

                e.HasOne(x => x.ClearanceForm)
                 .WithMany(f => f.Assigments)
                 .HasForeignKey(x => x.ClearanceFormId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.Department)
                 .WithMany()
                 .HasForeignKey(x=>x.DepartmentId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.DepartmentResponsiblePerson)
                 .WithMany()
                 .HasForeignKey(x => x.DepartmentResponsiblePersonId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.ServiceCenterResponsiblePerson)
                 .WithMany()
                 .HasForeignKey(x => x.ServiceCenterResponsiblePersonId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DepartmentResponsiblePerson>()
                .HasIndex(x => new { x.DepartmentId, x.IsActive })
                .HasFilter("[IsActive] = 1")
                .IsUnique();

            modelBuilder.Entity<ServiceCenterResponsiblePerson>()
                .HasIndex(x => new {x.ServiceCenterId, x.IsActive})
                .HasFilter("[IsActive] = 1")
                .IsUnique();


            modelBuilder.Entity<Department>().HasData(
               new Department { Id = 1, Name = "იურიდიული განყოფილება" },
               new Department { Id = 2, Name = "ადამიანური რესურსების მართვისა და კორპორაციული მდგრადობის განყოფილება" }, 
               new Department { Id = 3, Name = "გაყიდვების ადმინისტრირების განყოფილება" },
               new Department { Id = 4, Name = "უსაფრთხოების განყოფილება" },
               new Department { Id = 6, Name = "ბუღალტერიის განყოფილება" },
               new Department { Id = 12, Name = "ლოჯისტიკის, შესყიდვებისა და აქტივების რეალიზაციის განყოფილება" },               
               new Department { Id = 13, Name = "ინფორმაციული ტექნოლოგიების ინფრასტრუქტურისა და მხარდაჭერის ჯგუფი" }, 
               new Department { Id = 14, Name = "გაყიდვების განყოფილება" },
               new Department { Id = 15, Name = "სერვის ცენტრის უშუალო ხელმძღვანელი" },
               new Department { Id = 16, Name = "უშუალო ხელმძღვანელი" },
               new Department { Id = 17, Name = "მარკეტინგის განყოფილება" },
               new Department { Id = 18, Name = "სატელეფონო და დისტანციური მომსახურების ჯგუფი" },
               new Department { Id = 19, Name = "ცენტრალიზებული ბექ-ოფისის განყოფილება" },
               new Department { Id = 20, Name = "ხაზინის და ანგარიშსწორების განყოფილება" },
               new Department { Id = 21, Name = "დისტანციური გაყიდვების ჯგუფი" },
               new Department { Id = 22, Name = "პრობლემური აქტივების მართვის ჯგუფი" },
               new Department { Id = 23, Name = "გაყიდვების განყოფილება" },
               new Department { Id = 24, Name = "ფინანსური ანგარიშგების განყოფილება" },
               new Department { Id = 25, Name = "აქტივების რეალიზაციის ჯგუფი" },
               new Department { Id = 26, Name = "გადამზადების ჯგუფი" },
               new Department { Id = 27, Name = "რეკრუტინგის ჯგუფი" },
               new Department { Id = 28, Name = "საოპერაციო რისკებისა და AML განყოფილება" },
               new Department { Id = 29, Name = "შეფასების ჯგუფი" },
               new Department { Id = 30, Name = "საკრედიტო რისკების განყოფილება" },
               new Department { Id = 31, Name = "ინფორმაციული ტექნოლოგიების პროექტების მართვის ჯგუფი" },
               new Department { Id = 32, Name = "შიდა აუდიტი" },
               new Department { Id = 33, Name = "ინვესტორებთან ურთიერთობის ჯგუფი" }

           );

            modelBuilder.Entity<ServiceCenter>().HasData(
               new ServiceCenter { Id = 1, Name = "ვარკეთილის სერვის ცენტრი" },
               new ServiceCenter { Id = 2, Name = "ლაგოდეხის სერვის ცენტრი" },
               new ServiceCenter { Id = 3, Name = "ისნის სერვის ცენტრი" },
               new ServiceCenter { Id = 4, Name = "დიდუბის სერვის ცენტრი" },
               new ServiceCenter { Id = 5, Name = "ქუთაისის სერვის ცენტრი" },
               new ServiceCenter { Id = 6, Name = "ახალციხის სერვის ცენტრი" },
               new ServiceCenter { Id = 7, Name = "საბურთალოს სერვის ცენტრი" },
               new ServiceCenter { Id = 8, Name = "გორის სერვის ცენტრი" },
               new ServiceCenter { Id = 9, Name = "ზესტაფონის სერვის ცენტრი" },
               new ServiceCenter { Id = 10, Name = "რუსთავის სერვის ცენტრი" },
               new ServiceCenter { Id = 11, Name = "გლდანის სერვის ცენტრი" },
               new ServiceCenter { Id = 12, Name = "მარნეულის სერვის ცენტრი" },
               new ServiceCenter { Id = 13, Name = "ბათუმის სერვის ცენტრი" },
               new ServiceCenter { Id = 14, Name = "თელავის სერვის ცენტრი" },
               new ServiceCenter { Id = 15, Name = "სამტრედიის სერვის ცენტრი" },
               new ServiceCenter { Id = 16, Name = "ზუგდიდის სერვის ცენტრი" },
               new ServiceCenter { Id = 17, Name = "გურჯაანის სერვის ცენტრი" }
           );


            modelBuilder.Entity<DepartmentResponsiblePerson>().HasData(
                new DepartmentResponsiblePerson { Id = 21, DepartmentId = 1, FullName = "რუსუდანი მაჭარაშვილი", Email = "r.macharashvili@yourdomain.ge" },
                new DepartmentResponsiblePerson { Id = 22, DepartmentId = 2, FullName = "გვანცა ფაცაცია", Email = "g.fatsatsia@yourdomain.ge" },
                new DepartmentResponsiblePerson { Id = 23, DepartmentId = 3, FullName = "გიგლა პაპალაშვილი", Email = "g.papalashvili@yourdomain.ge" },
                new DepartmentResponsiblePerson { Id = 24, DepartmentId = 4, FullName = "ავთანდილ ტაბატაძე", Email = "a.tabatadze@yourdomain.ge" },
                new DepartmentResponsiblePerson { Id = 26, DepartmentId = 6, FullName = "ნინო ნებიერიძე", Email = "n.nebieridze@yourdomain.ge" },
                new DepartmentResponsiblePerson { Id = 32, DepartmentId = 12, FullName = "ვლადიმერ სულაბერიძე", Email = "v.sulaberidze@yourdomain.ge" },
                new DepartmentResponsiblePerson { Id = 33, DepartmentId = 13, FullName = "IT Support", Email = "it@yourdomain.ge" },
                new DepartmentResponsiblePerson { Id = 34, DepartmentId = 14, FullName = "Regional Lead", Email = "regional@yourdomain.ge" }
            );

            modelBuilder.Entity<ServiceCenterResponsiblePerson>().HasData(
               new ServiceCenterResponsiblePerson { Id = 41, ServiceCenterId = 1, FullName = "სოფიო შუბაშიშვილი", Email = "s.shubashvili@yourdomain.ge" },
               new ServiceCenterResponsiblePerson { Id = 42, ServiceCenterId = 2, FullName = "ქეთევან გურგენიძე", Email = "k.gurgenidze@yourdomain.ge" },
               new ServiceCenterResponsiblePerson { Id = 43, ServiceCenterId = 3, FullName = "ნინო ჭაბუკიანი", Email = "n.chabukiani@yourdomain.ge" },
               new ServiceCenterResponsiblePerson { Id = 44, ServiceCenterId = 4, FullName = "გივი დვალაშვილი", Email = "g.dvalashvili@yourdomain.ge" },
               new ServiceCenterResponsiblePerson { Id = 45, ServiceCenterId = 5, FullName = "გიგა კაშმაძე", Email = "g.kashmadze@yourdomain.ge" },
               new ServiceCenterResponsiblePerson { Id = 46, ServiceCenterId = 6, FullName = "ალექსანდრა დათებაშვილი", Email = "a.datebashvili@yourdomain.ge" },
               new ServiceCenterResponsiblePerson { Id = 47, ServiceCenterId = 7, FullName = "საბა ლორთქიფანიძე", Email = "s.lortqifanidze@yourdomain.ge" },
               new ServiceCenterResponsiblePerson { Id = 48, ServiceCenterId = 8, FullName = "ილია ანიაშვილი", Email = "i.aniashvili@yourdomain.ge" },
               new ServiceCenterResponsiblePerson { Id = 49, ServiceCenterId = 9, FullName = "ლევანი სანიკიძე", Email = "l.sanikidze@yourdomain.ge" },
               new ServiceCenterResponsiblePerson { Id = 50, ServiceCenterId = 10, FullName = "თეონა ჯავახიშვილი", Email = "t.javakhishvili@yourdomain.ge" },
               new ServiceCenterResponsiblePerson { Id = 51, ServiceCenterId = 11, FullName = "ლევანი გოგოლაძე", Email = "l.gogoladze@yourdomain.ge" },
               new ServiceCenterResponsiblePerson { Id = 52, ServiceCenterId = 12, FullName = "გიორგი ბესტავაშვილი", Email = "g.bestavashvili@yourdomain.ge" },
               new ServiceCenterResponsiblePerson { Id = 53, ServiceCenterId = 13, FullName = "არჩილ კაკალაძე", Email = "a.kakaladze@yourdomain.ge" },
               new ServiceCenterResponsiblePerson { Id = 54, ServiceCenterId = 14, FullName = "რუსუდანი კუკნიშვილი", Email = "r.kuknishvili@yourdomain.ge" },
               new ServiceCenterResponsiblePerson { Id = 55, ServiceCenterId = 15, FullName = "ირაკლი გელაზონია", Email = "i.gelazonia@yourdomain.ge" },
               new ServiceCenterResponsiblePerson { Id = 56, ServiceCenterId = 16, FullName = "დავით ქორთუა", Email = "d.kortua@yourdomain.ge" }
           );


            modelBuilder.Entity<Item>().HasData(
               new Item { Id = 1001, DepartmentId = 1, Name = "გარე მინდობილობები", StatusType = ItemStatusType.PowerOfAttorney },
               new Item { Id = 1002, DepartmentId = 1, Name = "გარე მინდობილობების დედნები", StatusType = ItemStatusType.SimpleReturned },
               new Item { Id = 1101, DepartmentId = 3, Name = "ოვერდრაფტი", StatusType = ItemStatusType.TypeForSales2, Kind = ItemKind.Text, IsActive = true, DisplayOrder = 1 },
               new Item { Id = 1111, DepartmentId = 3, Name = "საკრედიტო პროდუქტი", StatusType = ItemStatusType.YesNo, Kind = ItemKind.Text, IsActive = true, DisplayOrder = 2 },
               new Item { Id = 1102, DepartmentId = 3, Name = "საკრედიტო ბარათი", StatusType = ItemStatusType.TypeForSales, Kind = ItemKind.Text, IsActive = true, DisplayOrder = 3 },
               new Item { Id = 1103, DepartmentId = 3, Name = "სამომხმარებლო სესხი", StatusType = ItemStatusType.TypeForSales, Kind = ItemKind.Text, IsActive = true, DisplayOrder = 4 },
               new Item { Id = 1104, DepartmentId = 3, Name = "ავტო სესხი", StatusType = ItemStatusType.TypeForSales, Kind = ItemKind.Text, IsActive = true, DisplayOrder = 5 },
               new Item { Id = 1105, DepartmentId = 3, Name = "იპოთეკური სესხი", StatusType = ItemStatusType.TypeForSales, Kind = ItemKind.Text, IsActive = true, DisplayOrder = 6 },
               new Item { Id = 1106, DepartmentId = 3, Name = "აგრო სესხი", StatusType = ItemStatusType.TypeForSales, Kind = ItemKind.Text, IsActive = true, DisplayOrder = 7 },
               new Item { Id = 1107, DepartmentId = 3, Name = "ბიზნეს სესხი", StatusType = ItemStatusType.TypeForSales, Kind = ItemKind.Text, IsActive = true, DisplayOrder = 8 },
               new Item { Id = 1108, DepartmentId = 3, Name = "სტარტაპ სესხი", StatusType = ItemStatusType.TypeForSales, Kind = ItemKind.Text, IsActive = true, DisplayOrder = 9 },
               new Item { Id = 1110, DepartmentId = 3, Name = "ინკასო/ყადაღა", StatusType = ItemStatusType.YesNo, Kind = ItemKind.Text, IsActive = true, DisplayOrder = 10 },
               new Item { Id = 1201, DepartmentId = 12, Name = "მობილური ტელეფონი", StatusType = ItemStatusType.SimpleReturned },
               new Item { Id = 1202, DepartmentId = 12, Name = "საწვავის ბარათი", StatusType = ItemStatusType.SimpleReturned },
               new Item { Id = 1203, DepartmentId = 12, Name = "მაგიდა", StatusType = ItemStatusType.SimpleReturned },
               new Item { Id = 1204, DepartmentId = 12, Name = "სკამი", StatusType = ItemStatusType.SimpleReturned },
               new Item { Id = 1301, DepartmentId = 13, Name = "კომპიუტერი (პროცესორი/მონიტორი)", StatusType = ItemStatusType.SimpleReturned },
               new Item { Id = 1302, DepartmentId = 13, Name = "ლეპტოპი", StatusType = ItemStatusType.SimpleReturned },
               new Item { Id = 1303, DepartmentId = 13, Name = "გარე ვინჩესტერი", StatusType = ItemStatusType.SimpleReturned },
               new Item { Id = 1304, DepartmentId = 13, Name = "უწყვეტი კვების წყარო (UPS)", StatusType = ItemStatusType.SimpleReturned },
               new Item { Id = 1305, DepartmentId = 13, Name = "IP ტელეფონი", StatusType = ItemStatusType.SimpleReturned },
               new Item { Id = 1401, DepartmentId = 4, Name = "დაშვების ბარათი", StatusType = ItemStatusType.SimpleReturned2 },
               new Item { Id = 1501, DepartmentId = 6, Name = "ავანსად დარიცხული ხელფასი", StatusType = ItemStatusType.Debt, Kind = ItemKind.Text },
               new Item { Id = 1502, DepartmentId = 6, Name = "საქვეანგარიშოდ გაცემული თანხა", StatusType = ItemStatusType.Debt, Kind = ItemKind.Text },
               new Item { Id = 1503, DepartmentId = 6, Name = "დაზიანებული ნივთის ანაზღაურების განაწილება", StatusType = ItemStatusType.Debt, Kind = ItemKind.Text },
               new Item { Id = 1504, DepartmentId = 6, Name = "სხვა ვალდებულება (პოზიციის გათვალისწინებით)", StatusType = ItemStatusType.Debt, Kind = ItemKind.Text },
               new Item { Id = 1601, DepartmentId = 14, Name = "სალაროში არსებული დანაკლისი", StatusType = ItemStatusType.CashierResponsibility, Kind = ItemKind.Text },
               new Item { Id = 1701, DepartmentId = 2, Name = "განცხადება შრომითი ხელშეკრულების შეწყვეტის შესახებ", StatusType = ItemStatusType.SimpleReturned2 },
               new Item { Id = 1702, DepartmentId = 2, Name = "საბოლოო გასაუბრების ფორმა (არსებობის შემთხვევაში)", StatusType = ItemStatusType.SimpleReturned2 },
               new Item { Id = 1703, DepartmentId = 2, Name = "გარე ტრენინგები (ბოლო 12 თვე)", StatusType = ItemStatusType.YesNo },
               new Item { Id = 1704, DepartmentId = 2, Name = "ავანსად გაცემული ნებისმიერი ანაზღაურება", StatusType = ItemStatusType.YesNo },
               new Item { Id = 1814, DepartmentId = 15, Name = "საქმეები", DisplayOrder = 1 },
               new Item { Id = 1818, DepartmentId = 15, Name = "შიდა მინდობილობები", StatusType = ItemStatusType.PowerOfAttorney, DisplayOrder = 2 },
               new Item { Id = 1815, DepartmentId = 15, Name = "ხელშეკრულებები (დეპოზიტები, მოზიდული სახსრები და სხვა)", DisplayOrder = 3 },
               new Item { Id = 1816, DepartmentId = 15, Name = "მიღება-ჩაბარების აქტები", Kind = ItemKind.Text, DisplayOrder = 4 },
               new Item { Id = 1817, DepartmentId = 15, Name = "გადათვლის აქტები", Kind = ItemKind.Text, DisplayOrder = 5 },
               new Item { Id = 1801, DepartmentId = 15, Name = "ბრენდირებული ბეიჯი", DisplayOrder = 6 },
               new Item { Id = 1802, DepartmentId = 15, Name = "გასაღებები (რაოდენობრივად)", Kind = ItemKind.Text, DisplayOrder = 7 },
               new Item { Id = 1803, DepartmentId = 15, Name = "საგანგაშო პულტი", DisplayOrder = 8 },
               new Item { Id = 1804, DepartmentId = 15, Name = "მაგიდა", DisplayOrder = 9 },
               new Item { Id = 1805, DepartmentId = 15, Name = "სკამი", DisplayOrder = 10 },
               new Item { Id = 1806, DepartmentId = 15, Name = "მობილური ტელეფონი", DisplayOrder = 11 },
               new Item { Id = 1807, DepartmentId = 15, Name = "საწვავის ბარათი", DisplayOrder = 12 },
               new Item { Id = 1808, DepartmentId = 15, Name = "კომპიუტერი (პროცესორი, მონიტორი)", DisplayOrder = 13 },
               new Item { Id = 1809, DepartmentId = 15, Name = "ლეპტოპი", DisplayOrder = 14 },
               new Item { Id = 1810, DepartmentId = 15, Name = "გარე ვინჩესტერი", DisplayOrder = 15 },
               new Item { Id = 1811, DepartmentId = 15, Name = "უწყვეტი კვების წყარო (UPS)", DisplayOrder = 16 },
               new Item { Id = 1812, DepartmentId = 15, Name = "პრინტერი", DisplayOrder = 17 },
               new Item { Id = 1813, DepartmentId = 15, Name = "IP ტელეფონი", DisplayOrder = 18 },
               new Item { Id = 1901, DepartmentId = 16, Name = "საქმეები", DisplayOrder = 1 },
               new Item { Id = 1908, DepartmentId = 16, Name = "შიდა მინდობილობები", StatusType = ItemStatusType.PowerOfAttorney, DisplayOrder = 2 },
               new Item { Id = 1902, DepartmentId = 16, Name = "ხელშეკრულებები (დეპოზიტები, მოზიდული სახსრები და სხვა)", DisplayOrder = 3 },
               new Item { Id = 1903, DepartmentId = 16, Name = "მიღება-ჩაბარების აქტები", Kind = ItemKind.Text, DisplayOrder = 4 },
               new Item { Id = 1904, DepartmentId = 16, Name = "გადათვლის აქტები", Kind = ItemKind.Text, DisplayOrder = 5 },
               new Item { Id = 1905, DepartmentId = 16, Name = "თოქენი (ავთენტიფიკაციის გასაღები)", Kind = ItemKind.Text, DisplayOrder = 6 },
               new Item { Id = 1906, DepartmentId = 16, Name = "ID ბარათის წამკითხველი (რაოდენობრივად)", Kind = ItemKind.Text, DisplayOrder = 7 }
           );
        }
    }
}
