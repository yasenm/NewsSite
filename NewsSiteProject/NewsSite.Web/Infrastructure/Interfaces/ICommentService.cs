namespace NewsSite.Web.Infrastructure.Interfaces
{
    using System.Collections.Generic;

    using NewsSite.Web.ViewModels.Comments;

    public interface ICommentService
    {
        bool Create(CreateCommentModel commentModel);

        bool DeleteComment(string id);

        ICollection<CommentViewModel> CommentsForArticle(long articleId);
    }
}
