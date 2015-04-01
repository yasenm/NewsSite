namespace NewsSite.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Recaptcha.Web;
    using Recaptcha.Web.Mvc;

    using NewsSite.Constants;
    using NewsSite.Data.Models;
    using NewsSite.Data.UnitOfWork;
    using NewsSite.Web.Controllers.Base;
    using NewsSite.Web.ViewModels.Comments;
    using NewsSite.Web.Infrastructure.Interfaces;

    public class CommentController : BaseController
    {
        private ICommentService CommentService { get; set; }

        public CommentController(ICommentService commentService)
        {
            this.CommentService = commentService;
        }

        [HttpGet]
        public ActionResult Create(long articleId)
        {
            var model = new CreateCommentModel { ArticleId = articleId };

            return this.PartialView("CreateCommentPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCommentModel commentModel)
        {
            RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper(GlobalConstants.RecaptchaSecretKey);

            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                this.ModelState.AddModelError("Captcha", "Invalid captcha");
            }

            if (commentModel != null && this.ModelState.IsValid)
            {
                var created = this.CommentService.Create(commentModel);
                if (created)
                {
                    return this.CommentsForArticle(commentModel.ArticleId);
                }
            }

            throw new HttpException(404, "Comment could not be created!");
        }

        public ActionResult CommentsForArticle(long articleId)
        {
            var collection = this.CommentService.CommentsForArticle(articleId);

            return this.PartialView("ListOfComments", collection);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public bool DeleteComment(string id)
        {
            return this.CommentService.DeleteComment(id);
        }
    }
}