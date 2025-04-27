using System;

namespace CampRating.Web.ViewModels.Review;

public class ReviewInListViewModel
{
    public string Id { get; set; }
    
    public string Username { get; set; }
    
    public string UserId { get; set; }
    
    public int Rating { get; set; }
    
    public string Comment { get; set; }
    
    public DateTime CreatedOn { get; set; }
}