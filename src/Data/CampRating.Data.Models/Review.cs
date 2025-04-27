namespace CampRating.Data.Models;

using System;
using System.ComponentModel.DataAnnotations;

using CampRating.Data.Common.Models;

using static CampRating.Common.EntityValidationConstants.Rating;

public class Review : BaseDeletableModel<string>
{
    public Review()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    [Required]
    public string UserId { get; set; }
    
    [Required]
    public ApplicationUser User { get; set; }
    
    [MaxLength(CommentMaxLenght)]
    public string Comment { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    [Range(RatingMinValue, RatingMaxValue)]
    public int Rating { get; set; }
    [Required]
    public string CampId { get; set; }
    
    [Required]
    public virtual Camp Camp { get; set; }
}