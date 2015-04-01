namespace NewsSite.Web.Infrastructure.Services
{
    using System;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using NewsSite.Data.Models;
    using NewsSite.Data.UnitOfWork;
    using NewsSite.Web.Infrastructure.Interfaces;
    using NewsSite.Web.ViewModels.Comments;
    using System.Collections.Generic;

    public class CommentService : ICommentService
    {
        private INewsSiteData Data { get; set; }

        public CommentService(INewsSiteData data)
        {
            this.Data = data;
        }

        public bool Create(CreateCommentModel commentModel)
        {
            try
            {
                var dbComment = Mapper.Map<Comment>(commentModel);

                this.Data.Comments.Add(dbComment);
                this.Data.SaveChanges();

                this.Data.Articles.GetById(commentModel.ArticleId).Comments.Add(dbComment);
                this.Data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteComment(string id)
        {
            try
            {
                this.Data.Comments.Delete(Guid.Parse(id));
                this.Data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ICollection<CommentViewModel> CommentsForArticle(long articleId)
        {
            var article = this.Data.Articles.GetById(articleId);

            var collection = this.Data.Comments.All()
                .Where(c => c.ArticleId == articleId)
                .OrderByDescending(c => c.CreatedOn)
                .AsQueryable()
                .Project()
                .To<CommentViewModel>()
                .ToList();

            return collection;
        }
    }
}