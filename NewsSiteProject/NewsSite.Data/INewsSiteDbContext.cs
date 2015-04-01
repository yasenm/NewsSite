namespace NewsSite.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using NewsSite.Data.Models;

    public interface INewsSiteDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Article> Articles { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<Photo> Photos { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
