using Famot.ScannerReportsService.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel.DataAnnotations.Schema;

namespace Famot.ScannerReportsService.UnitOfWork.SrsDbContext
{
    public class SrsContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<ScannerFile> ScannerFiles { get; set; }
        public SrsContext() : base("SrsContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Order>()
                .HasMany(o => o.ScannerFiles)
                .WithRequired(sf => sf.Order)
                .HasForeignKey(sf => sf.OrderID);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            var currentUsername = !string.IsNullOrWhiteSpace(Thread.CurrentPrincipal.Identity.Name) ? Thread.CurrentPrincipal.Identity.Name : "Anonymous";
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    (entity.Entity as BaseEntity).DateCreated = DateTime.Now;
                    (entity.Entity as BaseEntity).UserCreated = currentUsername;
                }
                (entity.Entity as BaseEntity).DateModified = DateTime.Now;
                (entity.Entity as BaseEntity).UserModified = currentUsername;
            }
        }
    }
}
