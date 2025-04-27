namespace CampRating.Web.ViewModels.Camp;

public class CampDetailsViewModel
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Latitude { get; set; }
    
    public string Longitude { get; set; }
    
    public string ImgUrl { get; set; }
    
    public double AverageRating { get; set; }
    
    public int ReviewsCount { get; set; }
    
}