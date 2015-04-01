namespace NewsSite.Web.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using NewsSite.Data.UnitOfWork;
    using NewsSite.Web.Infrastructure.Interfaces;

    public class DropDownListService : IDropDownListService
    {
        private INewsSiteData Data { get; set; }

        public DropDownListService(INewsSiteData data)
        {
            this.Data = data;
        }

        public SelectList GetCategoriesList()
        {
            var categories = this.Data.Categories.All().ToList();

            List<SelectListItem> ddl = new List<SelectListItem>();
            ddl.AddRange(new SelectList(categories, "Id", "Name"));

            return new SelectList(ddl, "Value", "Text");
        }
    }
}