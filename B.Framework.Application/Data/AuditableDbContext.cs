using System.Collections.Generic;
using System.Linq;
using B.Framework.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace B.Framework.Application.Data
{
    public class AuditableDbContext : DbContext
    {
        public IEnumerable<EntityEntry> Changes => ChangeTracker.Entries().Where(b =>
            b.State == EntityState.Added || b.State == EntityState.Deleted || b.State == EntityState.Modified);


        public DbSet<AuditLog> AuditLog { get; set; }
        
        
        
        public override int SaveChanges()
        {
            var changes = Changes;
            foreach (var change in Changes)
            {
                
                
                
            }

            return default;
        }
    }
}