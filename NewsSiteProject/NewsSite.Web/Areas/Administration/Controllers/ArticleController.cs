namespace NewsSite.Web.Areas.Administration.Controllers
{
    using System.Web;
    using System.Web.Mvc;

    using NewsSite.Constants;
    using NewsSite.Web.Areas.Administration.InputModels.Articles;
    using NewsSite.Web.Controllers.Base;
    using NewsSite.Web.Infrastructure.Interfaces;

    public class ArticleController : BaseAdminController
    {
        private IArticleService ArticleService { get; set; }
        private IDropDownListService DropDownListService { get; set; }

        public ArticleController(
            IArticleService articleService,
            IDropDownListService dropDownListService)
        {
            this.ArticleService = articleService;
            this.DropDownListService = dropDownListService;
        }

        // GET: Administration/Article
        public ActionResult Index()
        {
            var articles = this.ArticleService.GetArticlesForGrid();

            return this.View(articles);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ArticleInputModel();

            ViewBag.Categories = this.DropDownListService.GetCategoriesList();

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleInputModel model)
        {
            if (ModelState.IsValid)
            {
                var created = this.ArticleService.Create(model, this.GetUserId());
                if (created)
                {
                    return this.Redirect("~/");
                }
            }

            ViewBag.Categories = this.DropDownListService.GetCategoriesList();
            return this.View(model);
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            this.ArticleService.Delete(id);
            return this.RedirectToIndex();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var model = this.ArticleService.GetForEdit(id);
            if (model != null)
            {
                ViewBag.Categories = this.DropDownListService.GetCategoriesList();
                return this.View(model);
            }

            throw new HttpException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, ArticleEditModel model)
        {
            if (ModelState.IsValid)
            {
                var result = this.ArticleService.Edit(id, model);
                if (result)
                {
                    return this.RedirectToIndex();
                }
            }

            ViewBag.Categories = this.DropDownListService.GetCategoriesList();
            return this.View(model);
        }

        private ActionResult RedirectToIndex()
        {
            return this.RedirectToAction(GlobalConstants.IndexPage);
        }
    }
}