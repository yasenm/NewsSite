namespace NewsSite.Web.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Web;

    using AutoMapper.QueryableExtensions;

    using NewsSite.Data.Models;
    using NewsSite.Data.UnitOfWork;
    using NewsSite.Web.Infrastructure.Interfaces;
    using NewsSite.Web.ViewModels.Photos;

    public class PhotoService : IPhotoService
    {
        private INewsSiteData Data { get; set; }

        public PhotoService(INewsSiteData data)
        {
            this.Data = data;
        }

        public Photo GetDbPhoto(long photoId)
        {
            return this.Data.Photos.GetById(photoId);
        }

        public byte[] GetPhoto(long photoId)
        {
            var photoContent = this.Data.Photos.GetById(photoId).Content;

            return photoContent;
        }

        public long UploadCoverPhoto(HttpPostedFileBase photo, long articleId)
        {
            using (var memory = new MemoryStream())
            {
                photo.InputStream.CopyTo(memory);

                var dbPhoto = new Photo()
                {
                    Content = memory.GetBuffer(),
                    Extension = "jpeg",
                };

                this.Data.Photos.Add(dbPhoto);
                this.Data.SaveChanges();

                var dbArticle = this.Data.Articles.GetById(articleId);
                dbArticle.PhotoId = dbPhoto.Id;
                this.Data.SaveChanges();

                return dbPhoto.Id;
            }
        }

        public long UploadPhoto(HttpPostedFileBase photo, long articleId)
        {
            using (var memory = new MemoryStream())
            {
                photo.InputStream.CopyTo(memory);

                var dbPhoto = new Photo()
                {
                    Content = memory.GetBuffer(),
                    Extension = "jpeg",
                    ArticleId = articleId,
                };

                this.Data.Photos.Add(dbPhoto);
                this.Data.SaveChanges();

                return dbPhoto.Id;
            }
        }

        public long UploadCoverPhotoAdvertisment(HttpPostedFileBase photo, long adId)
        {
            using (var memory = new MemoryStream())
            {
                photo.InputStream.CopyTo(memory);

                var dbPhoto = new Photo()
                {
                    Content = memory.GetBuffer(),
                    Extension = "gif",
                    AdvertismentId = adId,
                };

                this.Data.Photos.Add(dbPhoto);
                this.Data.SaveChanges();

                return dbPhoto.Id;
            }
        }

        public ICollection<PhotoViewModel> GetArticlePhotos(long articleId)
        {
            var result = this.Data.Photos.All()
                .Where(p => p.ArticleId == articleId)
                .Project().To<PhotoViewModel>()
                .ToList();

            return result;
        }
    }
}