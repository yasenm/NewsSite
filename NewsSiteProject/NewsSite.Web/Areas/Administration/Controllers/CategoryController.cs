namespace NewsSite.Web.Areas.Administration.Controllers
{
    using System.Web;
    using System.Web.Mvc;

    using PagedList;

    using NewsSite.Constants;
    using NewsSite.Web.Areas.ViewModels;
    using NewsSite.Web.Areas.Administration.InputModels.Category;
    using NewsSite.Web.Areas.Administration.InputModels;
    using NewsSite.Web.Controllers.Base;
    using NewsSite.Web.Infrastructure.Interfaces;

    public class CategoryController : BaseAdminController
    {
        private ICategoryService CategoryService { get; set; }

        public CategoryController(ICategoryService categoryService)
        {
            this.CategoryService = categoryService;
        }

        public ActionResult Index(int page = 1)
        {
            if (page > 0)
            {
                var categories = this.CategoryService.GetCategories();

                var model = new PagedList<CategoryViewModel>(categories, page, GlobalConstants.AdminPageSize);

                return this.View(model);
            }

            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CategoryInputModel();

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryInputModel model)
        {
            if (ModelState.IsValid)
            {
                var created = this.CategoryService.Create(model);
                if (created)
                {
                    return RedirectToAction(GlobalConstants.IndexPage);
                }
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var modelForEdit = this.CategoryService.GetForEdit(id);
            if (modelForEdit != null)
            {
                return this.View(modelForEdit);
            }

            throw new HttpException(404, "Category not found");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryEditInputModel model)
        {
            if (ModelState.IsValid)
            {
                var edited = this.CategoryService.Edit(id, model);

                if (edited)
                {
                    return RedirectToAction(GlobalConstants.IndexPage);
                }
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                var deleted = this.CategoryService.Delete(id);
                if (deleted)
                {
                    return RedirectToAction(GlobalConstants.IndexPage);
                }
            }

            throw new HttpException();
        }
    }
}