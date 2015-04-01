namespace NewsSite.Web.ViewModels.Articles
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    using AutoMapper;

    using NewsSite.Data.Models;
    using NewsSite.Web.Infrastructure.Mapping;

    public class ArticleDetailsViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public long CategoryId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int ReadersCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CommentsCount { get; set; }

        public string AuthorName { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public long PhotoId { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleDetailsViewModel>()
                .ForMember(m => m.CommentsCount, opt => opt.MapFrom(a => a.Comments.Count));
        }
    }
}