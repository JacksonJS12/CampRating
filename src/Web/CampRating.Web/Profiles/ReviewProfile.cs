using AutoMapper;
using CampRating.Data.Models;
using CampRating.Web.ViewModels.Review;

namespace CampRating.Web.Profiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        this.CreateMap<ReviewFormModel, Review>();
            
        this.CreateMap<Review, ReviewInListViewModel>();
    }
}