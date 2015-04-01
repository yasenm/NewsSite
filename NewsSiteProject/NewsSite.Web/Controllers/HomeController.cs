namespace NewsSite.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using NewsSite.Web.Controllers.Base;
    using NewsSite.Web.Infrastructure.Interfaces;

    public class HomeController : BaseController
    {
        private ICategoryService CategoryService { get; set; }

        public HomeController(ICategoryService categoryService)
        {
            this.CategoryService = categoryService;
        }

        public ActionResult Index()
        {
            var categories = this.CategoryService.GetCategories().ToList();

            return this.View(categories);
        }

        public ActionResult CategoryNavigationPartial()
        {
            var categories = this.CategoryService.GetCategories().ToList();

            return this.PartialView(categories);
        }

        [HttpGet]
        public ActionResult GetFooter()
        {
            return this.PartialView("_Footer");
        }

        [HttpGet]
        public ActionResult AboutUs()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult Contacts()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult Advertisement()
        {
            return this.View();
        }
    }
}