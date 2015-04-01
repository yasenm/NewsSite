namespace NewsSite.Web.ViewModels.Advertisments
{
    using AutoMapper;

    using NewsSite.Data.Models;
    using NewsSite.Web.Infrastructure.Mapping;

    public class AdvertismentViewModel : IMapFrom<Advertisment>
    {
        public string Link { get; set; }

        public long PhotoId { get; set; }

        public string Firm { get; set; }
    }
}