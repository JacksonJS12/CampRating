namespace CampRating.Web.Areas.Administration.Controllers
{
    using CampRating.Common;
    using CampRating.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        
    }
}
