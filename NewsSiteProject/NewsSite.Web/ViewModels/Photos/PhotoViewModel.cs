namespace NewsSite.Web.ViewModels.Photos
{
    using NewsSite.Data.Models;
    using NewsSite.Web.Infrastructure.Mapping;

    public class PhotoViewModel : IMapFrom<Photo>
    {
        public int Id { get; set; }
    }
}