using System.Collections.Generic;
using System.Threading.Tasks;
using CampRating.Web.ViewModels.Review;

namespace CampRating.Services.Interfaces;

public interface IReviewService
{
    Task CreateAsync(ReviewFormModel model, string userId, string username);
    
    Task<IEnumerable<ReviewInListViewModel>> GetAllByCampIdAsync(string campId);
    
    Task<double> GetAverageRatingByCampIdAsync(string campId);
    
    Task<int> GetReviewsCountByCampIdAsync(string campId);
    
    Task<bool> HasUserReviewedCampAsync(string userId, string campId);
    
    Task DeleteAsync(string reviewId, string userId);
}