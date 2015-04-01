namespace NewsSite.Web.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using NewsSite.Data.UnitOfWork;
    using NewsSite.Web.Areas.ViewModels;
    using NewsSite.Web.Areas.Administration.InputModels.Category;
    using NewsSite.Web.Areas.Administration.InputModels;
    using NewsSite.Web.Infrastructure.Interfaces;
    using NewsSite.Data.Models;

    public class CategoryService : ICategoryService
    {
        private INewsSiteData Data { get; set; }

        public CategoryService(INewsSiteData data)
        {
            this.Data = data;
        }

        public IQueryable<CategoryViewModel> GetCategories()
        {
            var categories = this.Data.Categories.All()
                .OrderByDescending(x => x.CreatedOn)
                .Project()
                .To<CategoryViewModel>();

            return categories;
        }

        public bool Create(CategoryInputModel model)
        {
            try
            {
                var newCategory = Mapper.Map<Category>(model);

                this.Data.Categories.Add(newCategory);
                this.Data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public CategoryEditInputModel GetForEdit(int id)
        {
            var catToEdit = this.Data.Categories.GetById(id);

            if (catToEdit == null)
            {
                return null;
            }

            var model = Mapper.Map<CategoryEditInputModel>(catToEdit);

            return model;
        }

        public bool Edit(int id, CategoryEditInputModel model)
        {
            try
            {
                var catToEdit = this.Data.Categories.GetById(id);
                Mapper.Map(model, catToEdit);

                this.Data.Categories.Update(catToEdit);
                this.Data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                this.Data.Categories.Delete(id);
                this.Data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}