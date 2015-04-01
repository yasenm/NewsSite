namespace NewsSite.Web.ViewModels.Comments
{
    using System;

    using NewsSite.Data.Models;
    using NewsSite.Web.Infrastructure.Mapping;

    public class CommentViewModel : IMapFrom<Comment>
    {
        public string Id { get; set; }

        public string AuthorName { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}