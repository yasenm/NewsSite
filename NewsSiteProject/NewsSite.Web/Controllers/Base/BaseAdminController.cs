namespace NewsSite.Web.Controllers.Base
{
    using System.Web.Mvc;

    using NewsSite.Constants;

    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class BaseAdminController : BaseController
    {
    }
}