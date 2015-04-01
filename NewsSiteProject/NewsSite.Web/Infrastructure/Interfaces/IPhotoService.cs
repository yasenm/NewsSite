namespace NewsSite.Web.Infrastructure.Interfaces
{
    using System.Collections.Generic;
    using System.Web;

    using NewsSite.Data.Models;
    using NewsSite.Web.ViewModels.Photos;

    public interface IPhotoService
    {
        Photo GetDbPhoto(long photoId);

        byte[] GetPhoto(long photoId);

        long UploadCoverPhoto(HttpPostedFileBase photo, long articleId);

        long UploadCoverPhotoAdvertisment(HttpPostedFileBase photo, long adId);

        long UploadPhoto(HttpPostedFileBase photo, long articleId);

        ICollection<PhotoViewModel> GetArticlePhotos(long articleId);
    }
}
