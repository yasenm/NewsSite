namespace NewsSite.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using NewsSite.Web.Areas.Administration.InputModels.Advertisments;
    using NewsSite.Web.Controllers.Base;
    using NewsSite.Web.Infrastructure.Interfaces;
    using NewsSite.Data.Models;
    using System;

    public class AdvertismentController : BaseAdminController
    {
        private IAdvertismentService AdvertismentService { get; set; }

        public AdvertismentController(IAdvertismentService advertismentService)
        {
            this.AdvertismentService = advertismentService;
        }

        public ActionResult Index()
        {
            var collection = this.AdvertismentService.GetAll();

            return this.View(collection);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new AdvertismentInputModel();

            return this.View();
        }

        [HttpPost]
        public ActionResult Create(AdvertismentInputModel adModel)
        {
            if (this.ModelState.IsValid)
            {
                // TODO: Save advertisment
               var adCreated = this.AdvertismentService.Create(adModel);
               if (adCreated)
               {
                   return this.RedirectToAction("Index", "Advertisment", new { Area = "Administration" });
               }
            }

            return this.View(adModel);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var adModel = this.AdvertismentService.GetAdForEdit(id);

            return this.View(adModel);
        }

        [HttpPost]
        public ActionResult Edit(AdvertismentEditViewModel adModel)
        {
            if (this.ModelState.IsValid)
            {
                // TODO: Save advertisment
                var adUpdated = this.AdvertismentService.Update(adModel);
                if (adUpdated)
                {
                    return this.RedirectToAction("Index", "Advertisment", new { Area = "Administration" });
                }
            }

            return this.View(adModel);
        }

        public ActionResult GetAdvetrismentTypesDropDown()
        {
            var types = Enum.GetNames(typeof(AdvertismentType)).ToList();
            return this.PartialView(types);
        }
    }
}