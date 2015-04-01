namespace NewsSite.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using NewsSite.Data.Common.Contracts.CodeFirstConventions;
    using NewsSite.Data.Common.Models;
    using NewsSite.Data.Models;
    using NewsSite.Data.Migrations;

    public class NewsSiteDbContext : IdentityDbContext<User>, INewsSiteDbContext
    {
        public NewsSiteDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<NewsSiteDbContext>(new MigrateDatabaseToLatestVersion<NewsSiteDbContext, Configuration>());
        }

        public IDbSet<Article> Articles { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Photo> Photos { get; set; }

        public IDbSet<Advertisment> Advertisments { get; set; }

        public static NewsSiteDbContext Create()
        {
            return new NewsSiteDbContext();
        }

        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();
            return base.SaveChanges();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Add(new IsUnicodeAttributeConvention());

            base.OnModelCreating(modelBuilder); // Without this call EntityFramework won't be able to configure the identity model
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}
