namespace NewsSite.Web.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using NewsSite.Data.UnitOfWork;
    using NewsSite.Web.Infrastructure.Interfaces;
    using NewsSite.Web.ViewModels.Articles;
    using System;
    using NewsSite.Web.Areas.Administration.ViewModels.Articles;
    using NewsSite.Web.Areas.Administration.InputModels.Articles;
    using NewsSite.Data.Models;

    public class ArticleService : IArticleService
    {
        private const double DAYS_BACK_FOR_COMMENTED_NEWS = -30;

        private INewsSiteData Data { get; set; }
        private IPhotoService PhotoService { get; set; }

        public ArticleService(INewsSiteData data, IPhotoService photoService)
        {
            this.Data = data;
            this.PhotoService = photoService;
        }

        public bool Create(ArticleInputModel model, string userId)
        {
            try
            {
                var article = Mapper.Map<Article>(model);
                article.AuthorId = userId;
                article.ReadersCount = 0;

                this.Data.Articles.Add(article);
                this.Data.SaveChanges();

                this.PhotoService.UploadCoverPhoto(model.CoverPhoto, article.Id);

                if (model.ArticlePhotos != null && model.ArticlePhotos != null)
                {
                    foreach (var photo in model.ArticlePhotos)
                    {
                        var photoId = this.PhotoService.UploadPhoto(photo, article.Id);
                        var dbPhoto = this.PhotoService.GetDbPhoto(photoId);
                        article.Photos.Add(dbPhoto);
                    }

                    this.Data.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(long id)
        {
            try
            {
                this.Data.Articles.Delete(id);
                this.Data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(long id, ArticleEditModel model)
        {
            try
            {
                var article = this.Data.Articles.All().FirstOrDefault(a => a.Id == id);
                Mapper.Map(model, article);

                this.Data.Articles.Update(article);
                this.Data.SaveChanges();

                if (model.CoverPhoto != null)
                {
                    this.PhotoService.UploadCoverPhoto(model.CoverPhoto, article.Id);
                }

                if (model.ArticlePhotos.Count > 1)
                {
                    foreach (var photo in model.ArticlePhotos)
                    {
                        var photoId = this.PhotoService.UploadPhoto(photo, article.Id);
                        var dbPhoto = this.PhotoService.GetDbPhoto(photoId);
                        article.Photos.Add(dbPhoto);
                    }

                    this.Data.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public ArticleEditModel GetForEdit(long id)
        {
            var articleToEdit = this.Data.Articles.All().FirstOrDefault(a => a.Id == id);
            if (articleToEdit != null)
            {
                var model = Mapper.Map<ArticleEditModel>(articleToEdit);
                return model;
            }

            return null;
        }

        public ArticleDetailsViewModel GetArticleDetails(long id)
        {
            var dbArticle = this.Data.Articles
                .GetById(id);
            dbArticle.ReadersCount++;
            Data.SaveChanges();

            var result = Mapper.Map<ArticleDetailsViewModel>(dbArticle);
            result.CommentsCount = this.Data.Comments.All().Where(c => c.ArticleId == id).Count();

            return result;
        }

        public IQueryable<ArticleViewModel> GetArticlesForGrid()
        {
            var articles = this.Data.Articles
                .All()
                .Project()
                .To<ArticleViewModel>()
                .OrderBy(a => a.CreatedOn);

            return articles;
        }

        public ICollection<ArticleListViewModel> LatestNews(int newsCount)
        {
            var collection = this.Data.Articles
                .All()
                .OrderByDescending(a => a.CreatedOn)
                .Take(newsCount)
                .Project()
                .To<ArticleListViewModel>()
                .ToList();

            return collection;
        }

        public ICollection<ArticleListViewModel> MostCommented(int newsCount)
        {
            var daysPassed = DateTime.Today.AddDays(DAYS_BACK_FOR_COMMENTED_NEWS);

            var collection = this.Data.Articles
                .All()
                .Where(a => a.CreatedOn > daysPassed)
                .OrderByDescending(a => a.Comments.Count())
                .Take(newsCount)
                .Project()
                .To<ArticleListViewModel>()
                .ToList();

            return collection;
        }

        public ICollection<ArticleListViewModel> MostImportant(int newsCount)
        {
            var collection = this.Data.Articles
                .All()
                .Where(a => a.IsImportant == true)
                .OrderByDescending(a => a.CreatedOn)
                .Take(newsCount)
                .Project()
                .To<ArticleListViewModel>()
                .ToList();

            return collection;
        }

        public ICollection<ArticleListViewModel> CarouselNews(int newsCount)
        {
            var collection = this.Data.Articles
                .All()
                .Where(a => a.IsHighlighted == true)
                .OrderByDescending(a => a.CreatedOn)
                .Take(newsCount)
                .Project()
                .To<ArticleListViewModel>()
                .ToList();

            return collection;
        }

        public ICollection<ArticleListViewModel> GetArticlesListByCategoryId(long categoryId, int newsCount)
        {
            var collection = this.Data.Articles
                .All()
                .Where(a => a.CategoryId == categoryId)
                .OrderByDescending(a => a.CreatedOn)
                .Take(newsCount)
                .Project()
                .To<ArticleListViewModel>()
                .ToList();

            return collection;
        }

        public IQueryable<ArticleBigListViewModel> GetArticlesListByCategoryId(long categoryId)
        {
            var collection = this.Data.Articles
                .All()
                .Where(a => a.CategoryId == categoryId)
                .OrderByDescending(a => a.CreatedOn)
                .Project()
                .To<ArticleBigListViewModel>();

            return collection;
        }

        public IQueryable<ArticleBigListViewModel> AllImportant()
        {
            var collection = this.Data.Articles
                .All()
                .Where(a => a.IsImportant == true)
                .OrderByDescending(a => a.CreatedOn)
                .Project()
                .To<ArticleBigListViewModel>();

            return collection;
        }

        public IQueryable<ArticleBigListViewModel> AllCommented()
        {
            var collection = this.Data.Articles
                .All()
                .Where(a => a.Comments.Count() > 0)
                .OrderByDescending(a => a.CreatedOn)
                .Project()
                .To<ArticleBigListViewModel>();

            return collection;
        }

        public IQueryable<ArticleBigListViewModel> AllFromTheLastDay()
        {
            var oneDayBefore = DateTime.Now.AddDays(-1);

            var collection = this.Data.Articles
                .All()
                .Where(a => a.CreatedOn > oneDayBefore)
                .OrderByDescending(a => a.CreatedOn)
                .Project()
                .To<ArticleBigListViewModel>();

            return collection;
        }
    }
}