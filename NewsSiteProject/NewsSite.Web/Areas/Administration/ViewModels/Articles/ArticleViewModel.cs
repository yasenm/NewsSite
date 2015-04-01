namespace NewsSite.Web.Areas.Administration.ViewModels.Articles
{
    using System;

    using AutoMapper;

    using NewsSite.Data.Models;
    using NewsSite.Web.Infrastructure.Mapping;

    public class ArticleViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public bool IsImportant { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string Category { get; set; }

        public bool IsHighlighted { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleViewModel>()
                .ForMember(dest => dest.Category, opts => opts.MapFrom(src => src.Category.Name));
        }
    }
}