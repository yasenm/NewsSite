namespace NewsSite.Web.Areas.ViewModels
{
    using NewsSite.Data.Models;
    using NewsSite.Web.Infrastructure.Mapping;

    public class CategoryViewModel:IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}