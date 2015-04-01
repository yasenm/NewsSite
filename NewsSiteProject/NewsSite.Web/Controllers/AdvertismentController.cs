namespace NewsSite.Web.Controllers
{
    using System.Web.Mvc;

    using NewsSite.Data.Models;
    using NewsSite.Web.Controllers.Base;
    using NewsSite.Web.Infrastructure.Interfaces;

    public class AdvertismentController : BaseController
    {
        private IAdvertismentService AdvertismentService { get; set; }

        public AdvertismentController(IAdvertismentService advertismentService)
        {
            this.AdvertismentService = advertismentService;
        }

        public ActionResult HorizontalAd()
        {
            var ad = this.AdvertismentService.GetAdvertisment(AdvertismentType.Horizontal);
            return this.PartialView("~/Views/Advertisment/HorizontalAd.cshtml", ad);
        }

        public ActionResult VerticalAd()
        {
            var ad = this.AdvertismentService.GetAdvertisment(AdvertismentType.Vertical);
            return this.PartialView(ad);
        }

        public ActionResult SquareAd()
        {
            var ad = this.AdvertismentService.GetAdvertisment(AdvertismentType.Square);
            return this.PartialView(ad);
        }
    }
}