using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web_miniCRM.Domain.Entity;
using Info = Web_miniCRM.Domain.Entity.Information; // создание псевдонима, класс Information уже используется - Microsoft.VisualBasic

namespace Web_miniCRM.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> //DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<HeadDepartment> HeadDepartments { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<InvoiceItemInfo> InvoicesItemInfo { get; set; }
        public DbSet<Info> Informations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Конфигурация Company

            modelBuilder.Entity<Company>()
                .HasKey(c => c.CompanyId);

            modelBuilder.Entity<Company>()
                    .HasMany(i => i.Informations)
                    .WithOne(ii => ii.Company)
                    .HasForeignKey(ii => ii.CompanyId);

            modelBuilder.Entity<Company>()
                        .HasMany(c => c.Invoices)       // Компания имеет много счетов
                        .WithOne(i => i.Company)   // Счет обязательно должен иметь компанию
                        .HasForeignKey(i => i.CompanyId) // Внешний ключ на CompanyId в Invoice
                        .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Company>()
                        .HasMany(c => c.Calls)       // Компания имеет много счетов
                        .WithOne(i => i.Company)   // Счет обязательно должен иметь компанию
                        .HasForeignKey(i => i.CompanyId) // Внешний ключ на CompanyId в Invoice
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Company>()
                        .HasMany(c => c.Meetings)       // Компания имеет много счетов
                        .WithOne(i => i.Company)   // Счет обязательно должен иметь компанию
                        .HasForeignKey(i => i.CompanyId) // Внешний ключ на CompanyId в Invoice
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Company>()
                        .Property(c => c.INN)
                        .HasMaxLength(10)
                        .IsRequired();

            modelBuilder.Entity<Company>()
                        .Property(c => c.NameContactPerson)
                        .HasMaxLength(500)
                        .IsRequired();

            modelBuilder.Entity<Company>()
                        .Property(c => c.PhoneNumber)
                        .HasMaxLength(500)
                        .IsRequired();

            modelBuilder.Entity<Company>()
                        .Property(c => c.Email)
                        .HasMaxLength(500);

            modelBuilder.Entity<Company>()
                        .Property(c => c.CompanyName)
                        .HasMaxLength(500)
                        .IsRequired();

            modelBuilder.Entity<Company>()
                        .HasOne(c => c.Manager)
                        .WithMany(m => m.Companies)
                        .HasForeignKey(c => c.ManagerId)
                        .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.MainCompany)
                    .HasDefaultValue(false)
                    .IsRequired()
                    .HasColumnType("bit");
            });

            #endregion
            #region Конфигурация Product

            modelBuilder.Entity<Product>()
                .HasKey(c => c.ProductId);

            modelBuilder.Entity<Product>()
                        .Property(c => c.Name)
                        .HasMaxLength(500)
                        .IsRequired();

            modelBuilder.Entity<Product>()
                        .Property(c => c.Price)
                        .HasPrecision(18, 2)
                        .IsRequired();

            modelBuilder.Entity<Product>()
                        .Property(c => c.CountAllProduct)
                        .IsRequired();

            modelBuilder.Entity<Product>()
                       .HasMany(p => p.InvoiceItems)
                       .WithOne(ii => ii.Product)
                       .HasForeignKey(ii => ii.ProductId)
                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                       .HasMany(p => p.Informations)
                       .WithOne(ii => ii.Product)
                       .HasForeignKey(ii => ii.ProductId)
                       .OnDelete(DeleteBehavior.Restrict);
            #endregion
            #region Конфигурация Invoice

            modelBuilder.Entity<Invoice>()
                .HasKey(i => i.InvoiceId);

            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.Informations) // Один Invoice может иметь множество связанных Info
                .WithOne(ii => ii.Invoice) // Каждое Info связано с одним Invoice
                .HasForeignKey(ii => ii.InvoiceId) // Внешний ключ для связи - InvoiceId
                .OnDelete(DeleteBehavior.Restrict); // Отключение каскадного удаления


            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.InvoiceItems) // Счет имеет много items
                .WithOne(ii => ii.Invoice) // InvoiceItems обязательно должен иметь к какому счету items привязаны
                .HasForeignKey(ii => ii.InvoiceId) // Внешний ключ на InvoiceId в InvoiceItems
                .IsRequired();

            modelBuilder.Entity<Invoice>()
                .HasOne(c => c.Manager)
                .WithMany(m => m.Invoices)
                .HasForeignKey(c => c.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
            #region Конфигурация InvoiceItemInfo
            modelBuilder.Entity<InvoiceItemInfo>()
                .HasKey(c => c.InvoiceItemId);
            modelBuilder.Entity<InvoiceItemInfo>()
                .Property(c => c.Quantity);

            #endregion
            #region Конфигурация Manager
            modelBuilder.Entity<Manager>()
            .HasKey(i => i.ManagerId);

            modelBuilder.Entity<ApplicationUser>()
            .HasIndex(i => i.Email)
            .IsUnique();

            modelBuilder.Entity<Manager>()
            .Property(i => i.DepartmentNumber)
            .IsRequired();

            modelBuilder.Entity<Manager>()
            .Property(i => i.FirstName)
            .IsRequired();
            modelBuilder.Entity<Manager>()
            .Property(i => i.LastName)
            .IsRequired();

            modelBuilder.Entity<Manager>()
            .HasMany(i => i.Calls)
            .WithOne(ii => ii.Manager)
            .HasForeignKey(ii => ii.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Manager>()
            //.HasMany(i => i.MainCompanies)
            //.WithOne(ii => ii.MainManager)
            //.HasForeignKey(ii => ii.MainManagerId)
            //.OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Manager>()
            //.HasMany(i => i.NotMainCompanies)
            //.WithOne(ii => ii.NotMainManager)
            //.HasForeignKey(ii => ii.NotMainManagerId)
            //.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Manager>()
            .HasMany(i => i.Meetings)
            .WithOne(ii => ii.Manager)
            .HasForeignKey(ii => ii.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);
            #endregion
            #region Конфигурация Call
            modelBuilder.Entity<Call>()
            .HasKey(i => i.CallId);

            modelBuilder.Entity<Call>()
            .Property(c => c.Details)
            .IsRequired();

            modelBuilder.Entity<Call>()
            .Property(c => c.CompanyId)
            .IsRequired();
            #endregion
            #region Конфигурация Meeting
            modelBuilder.Entity<Meeting>()
            .HasKey(i => i.MeetingId);

            modelBuilder.Entity<Meeting>()
            .Property(c => c.ManagerId)
            .IsRequired();

            modelBuilder.Entity<Meeting>()
            .Property(c => c.Address)
            .IsRequired();
            #endregion
            #region Конфигурация HeadDepartment

            modelBuilder.Entity<HeadDepartment>()
                .HasKey(c => c.HeadDepartmentId);

            modelBuilder.Entity<HeadDepartment>()
        .HasMany(i => i.Managers)
        .WithOne(ii => ii.HeadDepartment)
        .HasForeignKey(ii => ii.HeadDepartmentId)
        .OnDelete(DeleteBehavior.Restrict);
            #endregion
            #region Конфигурация ApplicationUser
            modelBuilder.Entity<ApplicationUser>()
            .HasOne(a => a.Manager)
            .WithMany()
            .HasForeignKey(a => a.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.HeadDepartment)
                .WithMany()
                .HasForeignKey(a => a.HeadDepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
