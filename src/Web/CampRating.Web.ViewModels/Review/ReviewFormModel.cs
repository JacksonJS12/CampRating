using System.ComponentModel.DataAnnotations;
using static CampRating.Common.EntityValidationConstants.Rating;

namespace CampRating.Web.ViewModels.Review;

public class ReviewFormModel
{
    
    [Required]
    public string CampId { get; set; }
    
    [Required]
    [Range(RatingMinValue, RatingMaxValue, ErrorMessage = "Rating must be between {1} and {2}.")]
    public int Rating { get; set; }
    
    [MaxLength(CommentMaxLenght, ErrorMessage = "Comment cannot exceed {1} characters.")]
    public string Comment { get; set; }
}