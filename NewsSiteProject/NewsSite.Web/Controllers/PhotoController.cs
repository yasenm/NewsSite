namespace NewsSite.Web.Controllers
{
    using System.Web.Mvc;

    using NewsSite.Web.Infrastructure.Interfaces;
    using NewsSite.Data.Models;
    using NewsSite.Web.ViewModels.Photos;

    public class PhotoController : Controller
    {
        private IPhotoService PhotoService { get; set; }

        public PhotoController(IPhotoService photoService)
        {
            this.PhotoService = photoService;
        }

        public ActionResult Photo(long photoId)
        {
            var photo = this.PhotoService.GetPhoto(photoId);

            return this.File(photo, "image/jpeg");
        }

        public ActionResult AdPhoto(long? photoId)
        {
            var photo = this.PhotoService.GetPhoto((long)photoId);

            return this.File(photo, "image/gif");
        }

        public ActionResult ArticleAlbumGalery(long articleId)
        {
            var collection = this.PhotoService.GetArticlePhotos(articleId);
            return this.PartialView(collection);
        }

        public ActionResult AlbumPhotoLink(long photoId)
        {
            return this.PartialView(photoId);
        }
    }
}