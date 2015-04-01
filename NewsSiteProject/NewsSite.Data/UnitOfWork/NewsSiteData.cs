namespace NewsSite.Data.UnitOfWork
{
    using System;
    using System.Collections.Generic;

    using NewsSite.Data.Common.Models;
    using NewsSite.Data.Common.Repository;
    using NewsSite.Data.Models;
    using NewsSite.Data.Repositories;

    public class NewsSiteData : INewsSiteData
    {
        private IDictionary<Type, object> repositories;

        public NewsSiteData(INewsSiteDbContext context)
        {
            this.Context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public INewsSiteDbContext Context { get; set; }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IRepository<Article> Articles
        {
            get
            {
                return this.GetDeletableEntityRepository<Article>();
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                return this.GetDeletableEntityRepository<Category>();
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return this.GetDeletableEntityRepository<Comment>();
            }
        }

        public IRepository<Photo> Photos
        {
            get
            {
                return this.GetDeletableEntityRepository<Photo>();
            }
        }

        public IRepository<Advertisment> Advertisments
        {
            get
            {
                return this.GetDeletableEntityRepository<Advertisment>();
            }
        }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.Context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(DeletableEntityRepository<T>), this.Context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeOfRepository];
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Context != null)
                {
                    this.Context.Dispose();
                }
            }
        }
    }
}
