namespace NewsSite.Web.Areas.Administration.InputModels.Category
{
    using NewsSite.Data.Models;
    using NewsSite.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class CategoryInputModel : IHaveCustomMappings
    {
        [Display(Name="Име")]
        [Required(ErrorMessage="Моля, въведете име за категорията!")]
        public string Name { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<CategoryInputModel, Category>().ReverseMap();
        }
    }
}