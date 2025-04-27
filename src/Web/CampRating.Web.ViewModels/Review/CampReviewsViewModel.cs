using System.Collections.Generic;

namespace CampRating.Web.ViewModels.Review;

public class CampReviewsViewModel
{
    public string CampId { get; set; }
    
    public string CampName { get; set; }
    
    public string CampImage { get; set; }
    
    public double AverageRating { get; set; }
    
    public int ReviewsCount { get; set; }
    
    public bool HasUserReviewed { get; set; }
    
    public ReviewFormModel NewReview { get; set; }
    
    public IEnumerable<ReviewInListViewModel> Reviews { get; set; }
}