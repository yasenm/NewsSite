namespace NewsSite.Web.Areas.Administration.InputModels.Advertisments
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using AutoMapper;

    using NewsSite.Data.Models;
    using NewsSite.Web.Infrastructure.Mapping;

    public class AdvertismentEditViewModel : IMapFrom<Advertisment>, IHaveCustomMappings
    {
        public long Id { get; set; }

        [Display(Name = "Снимка/Анимация")]
        public HttpPostedFileBase CoverPhoto { get; set; }

        [Display(Name = "Линк за рекламата")]
        [Required(ErrorMessage = "Задължително поле е линк към страницата за рекламата!")]
        public string Link { get; set; }

        [Display(Name = "Име на фирма")]
        [Required(ErrorMessage = "Задължително поле е името на фирма за рекламата!")]
        public string Firm { get; set; }

        [Display(Name = "Активност")]
        public bool IsActive { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Advertisment, AdvertismentInputModel>().ReverseMap();
        }
    }
}