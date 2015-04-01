namespace NewsSite.Web.Infrastructure.Interfaces
{
    using System.Web.Mvc;

    public interface IDropDownListService
    {
        SelectList GetCategoriesList();
    }
}
