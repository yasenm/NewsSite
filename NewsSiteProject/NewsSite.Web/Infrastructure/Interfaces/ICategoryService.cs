namespace NewsSite.Web.Infrastructure.Interfaces
{
    using System.Linq;

    using NewsSite.Web.Areas.ViewModels;
    using NewsSite.Web.Areas.Administration.InputModels.Category;
    using NewsSite.Web.Areas.Administration.InputModels;

    public interface ICategoryService
    {
        IQueryable<CategoryViewModel> GetCategories();

        bool Create(CategoryInputModel model);

        CategoryEditInputModel GetForEdit(int id);

        bool Edit(int id, CategoryEditInputModel model);

        bool Delete(int id);
    }
}
