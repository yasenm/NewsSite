namespace NewsSite.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using NewsSite.Data.Models;
    using NewsSite.Web.Infrastructure.Mapping;

    public class CreateCommentModel : IMapFrom<Comment>
    {
        [Display(Name = "Име на автор :")]
        public string AuthorName { get; set; }

        [Display(Name = "Вашия коментар :")]
        public string Content { get; set; }

        public long ArticleId { get; set; }
    }
}