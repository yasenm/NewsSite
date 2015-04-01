namespace NewsSite.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using PagedList;
    using PagedList.Mvc;

    using NewsSite.Web.Controllers.Base;
    using NewsSite.Data.UnitOfWork;
    using NewsSite.Web.ViewModels.Articles;
    using NewsSite.Web.Infrastructure.Interfaces;

    public class ArticleController : Controller
    {
        private const int DEFAULT_START_PAGE_NUMBER = 1;
        private const int DEFAULT_BIG_LIST_PAGESIZE = 15;

        private const string SMALL_NEWS_LIST_PARTIAL = "NewsSmallListPartial";
        private const string MEDIUM_NEWS_LIST_PARTIAL = "NewsMediumListPartial";

        private const string LARGE_CAROUSEL_PARTIAL = "LargeCarousel";

        private IArticleService ArticleService { get; set; }

        public ArticleController(IArticleService articleService)
        {
            this.ArticleService = articleService;
        }

        public ActionResult Details(long id)
        {
            var model = this.ArticleService.GetArticleDetails(id);

            return this.View(model);
        }

        public ActionResult LatestNews(int newsCount)
        {
            var collection = this.ArticleService.LatestNews(newsCount);

            return this.PartialView(MEDIUM_NEWS_LIST_PARTIAL, collection);
        }

        public ActionResult MostCommented(int newsCount)
        {
            var collection = this.ArticleService.MostCommented(newsCount);

            return this.PartialView(MEDIUM_NEWS_LIST_PARTIAL, collection);
        }

        public ActionResult MostImportant(int newsCount)
        {
            var collection = this.ArticleService.MostImportant(newsCount);

            return this.PartialView(MEDIUM_NEWS_LIST_PARTIAL, collection);
        }

        public ActionResult ListOfTheSameCategory(long categoryId, int newsCount)
        {
            var collection = this.ArticleService.GetArticlesListByCategoryId(categoryId, newsCount);

            return this.PartialView(MEDIUM_NEWS_LIST_PARTIAL, collection);
        }

        public ActionResult RenderSmallList(long categoryId, int newsCount)
        {
            var collection = this.ArticleService.GetArticlesListByCategoryId(categoryId, newsCount);

            return this.PartialView(SMALL_NEWS_LIST_PARTIAL, collection);
        }

        public ActionResult ArticlesByCategoryPage(long categoryId, int? page, int? itemsPerPage)
        {
            var articles = this.ArticleService.GetArticlesListByCategoryId(categoryId);

            this.ViewBag.CategoryId = categoryId;

            return this.View(articles.ToPagedList(pageNumber: page ?? DEFAULT_START_PAGE_NUMBER, pageSize: itemsPerPage ?? DEFAULT_BIG_LIST_PAGESIZE));
        }

        public ActionResult LargeCarousel(int newsCount)
        {
            var collection = this.ArticleService.CarouselNews(newsCount);

            return this.PartialView(LARGE_CAROUSEL_PARTIAL, collection);
        }

        public ActionResult MostImportantNews(int? page, int? itemsPerPage)
        {
            var collection = this.ArticleService.AllImportant();

            this.ViewBag.CategoryId = 1;

            return this.View(collection.ToPagedList(pageNumber: page ?? DEFAULT_START_PAGE_NUMBER, pageSize: itemsPerPage ?? DEFAULT_BIG_LIST_PAGESIZE));
        }

        public ActionResult MostCommentedNews(int? page, int? itemsPerPage)
        {
            var collection = this.ArticleService.AllCommented();

            this.ViewBag.CategoryId = 1;

            return this.View(collection.ToPagedList(pageNumber: page ?? DEFAULT_START_PAGE_NUMBER, pageSize: itemsPerPage ?? DEFAULT_BIG_LIST_PAGESIZE));
        }

        public ActionResult NewsOfTheDay(int? page, int? itemsPerPage)
        {
            var collection = this.ArticleService.AllFromTheLastDay();

            this.ViewBag.CategoryId = 1;

            return this.View(collection.ToPagedList(pageNumber: page ?? DEFAULT_START_PAGE_NUMBER, pageSize: itemsPerPage ?? DEFAULT_BIG_LIST_PAGESIZE));
        }
    }
}