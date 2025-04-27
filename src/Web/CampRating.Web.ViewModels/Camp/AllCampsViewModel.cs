using System.Collections.Generic;

namespace CampRating.Web.ViewModels.Camp;

public class AllCampsViewModel
{
    public IEnumerable<CampInListViewModel> Camps { get; set; }
}