namespace NewsSite.Web.Infrastructure.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;

    using NewsSite.Web.ViewModels.Articles;
    using NewsSite.Web.Areas.Administration.ViewModels.Articles;
    using NewsSite.Web.Areas.Administration.InputModels.Articles;

    public interface IArticleService
    {
        bool Create(ArticleInputModel model, string userId);

        bool Delete(long id);

        bool Edit(long id, ArticleEditModel model);

        ArticleEditModel GetForEdit(long id);

        ArticleDetailsViewModel GetArticleDetails(long id);

        IQueryable<ArticleViewModel> GetArticlesForGrid();

        ICollection<ArticleListViewModel> LatestNews(int newsCount);

        ICollection<ArticleListViewModel> MostCommented(int newsCount);

        ICollection<ArticleListViewModel> MostImportant(int newsCount);

        ICollection<ArticleListViewModel> CarouselNews(int newsCount);

        IQueryable<ArticleBigListViewModel> AllImportant();

        IQueryable<ArticleBigListViewModel> AllCommented();

        IQueryable<ArticleBigListViewModel> AllFromTheLastDay();

        ICollection<ArticleListViewModel> GetArticlesListByCategoryId(long categoryId, int newsCount);

        IQueryable<ArticleBigListViewModel> GetArticlesListByCategoryId(long categoryId);

    }
}
