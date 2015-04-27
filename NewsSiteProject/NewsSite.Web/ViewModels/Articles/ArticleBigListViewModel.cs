namespace NewsSite.Web.ViewModels.Articles
{
    using System;
    using System.Linq;

    using AutoMapper;

    using NewsSite.Data.Models;
    using NewsSite.Web.Infrastructure.Mapping;

    public class ArticleBigListViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public long PhotoId { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int CommentsCount { get; set; }

        public int ReadersCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleBigListViewModel>()
                .ForMember(m => m.CommentsCount, opt => opt.MapFrom(a => a.Comments.Where(c => c.IsDeleted == false).Count()))
                .ForMember(m => m.Content, opt => opt.MapFrom(a => a.Content.Substring(0, 100)));
        }
    }
}