namespace NewsSite.Web.Infrastructure.Services
{
    using System;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using NewsSite.Data.Common;
    using NewsSite.Data.Models;
    using NewsSite.Data.UnitOfWork;
    using NewsSite.Web.Areas.Administration.InputModels.Advertisments;
    using NewsSite.Web.Infrastructure.Interfaces;
    using NewsSite.Web.ViewModels.Advertisments;

    public class AdvertismentService : IAdvertismentService
    {
        private INewsSiteData Data { get; set; }
        private IPhotoService PhotoService { get; set; }

        public AdvertismentService(INewsSiteData data, IPhotoService photoService)
        {
            this.Data = data;
            this.PhotoService = photoService;
        }

        public AdvertismentEditViewModel GetAdForEdit(long id)
        {
            var ad = this.Data.Advertisments.GetById(id);

            return Mapper.Map<AdvertismentEditViewModel>(ad);
        }

        public IQueryable<AdvertismentAdminViewModel> GetAll()
        {
            return this.Data.Advertisments
                        .All()
                        .OrderBy(a => a.CreatedOn)
                        .Project()
                        .To<AdvertismentAdminViewModel>();
        }

        public bool Create(AdvertismentInputModel adModel)
        {
            if (adModel != null)
            {
                var dbAd = Mapper.Map<Advertisment>(adModel);

                this.Data.Advertisments.Add(dbAd);
                this.Data.SaveChanges();

                var photoId = this.PhotoService.UploadCoverPhotoAdvertisment(adModel.CoverPhoto, dbAd.Id);
                dbAd.PhotoId = photoId;
                this.Data.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Update(AdvertismentEditViewModel adModel)
        {
            try
            {
                var dbAd = this.Data.Advertisments.GetById(adModel.Id);
                dbAd.Link = adModel.Link;
                dbAd.Firm = adModel.Firm;
                dbAd.IsActive = adModel.IsActive;

                if (adModel.CoverPhoto != null)
                {
                    var photoId = this.PhotoService.UploadCoverPhotoAdvertisment(adModel.CoverPhoto, dbAd.Id);
                    dbAd.PhotoId = photoId;
                }

                this.Data.Advertisments.Update(dbAd);
                this.Data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public AdvertismentViewModel GetAdvertisment(AdvertismentType type)
        {
            var ads = this.Data.Advertisments
                .All()
                .Where(ad => ad.IsActive == true && ad.Type == type)
                .Project()
                .To<AdvertismentViewModel>()
                .ToList();

            if (ads.Count > 0)
            {
                return ads[RandomGenerator.RandomNumber(0, ads.Count - 1)];
            }
            return new AdvertismentViewModel();
        }
    }
}