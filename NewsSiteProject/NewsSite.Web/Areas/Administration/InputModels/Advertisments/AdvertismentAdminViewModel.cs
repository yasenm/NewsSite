namespace NewsSite.Web.Areas.Administration.InputModels.Advertisments
{
    using NewsSite.Constants;
    using NewsSite.Data.Models;
    using NewsSite.Web.Infrastructure.Mapping;

    public class AdvertismentAdminViewModel : IMapFrom<Advertisment>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public string Link { get; set; }

        public bool IsActive { get; set; }

        public string Firm { get; set; }

        public AdvertismentType TypeOfAd { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Advertisment, AdvertismentAdminViewModel>().ReverseMap();
        }
    }
}