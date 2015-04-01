namespace NewsSite.Data.UnitOfWork
{
    using NewsSite.Data.Common.Repository;

    using NewsSite.Data.Models;

    public interface INewsSiteData
    {
        INewsSiteDbContext Context { get; }

        IRepository<User> Users { get; }

        IRepository<Article> Articles { get; }

        IRepository<Category> Categories { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Photo> Photos { get; }

        IRepository<Advertisment> Advertisments { get; }

        int SaveChanges();
    }
}
