namespace NewsSite.Web.Infrastructure.Interfaces
{
    using System.Linq;

    using NewsSite.Data.Models;
    using NewsSite.Web.Areas.Administration.InputModels.Advertisments;
    using NewsSite.Web.ViewModels.Advertisments;

    public interface IAdvertismentService
    {
        AdvertismentEditViewModel GetAdForEdit(long id);

        IQueryable<AdvertismentAdminViewModel> GetAll();

        bool Create(AdvertismentInputModel adModel);

        bool Update(AdvertismentEditViewModel adModel);

        AdvertismentViewModel GetAdvertisment(AdvertismentType type);
    }
}