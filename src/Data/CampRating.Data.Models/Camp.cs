using System;
using System.ComponentModel.DataAnnotations;
using CampRating.Data.Common.Models;
using Microsoft.EntityFrameworkCore;
using static CampRating.Common.EntityValidationConstants.Camp;

namespace CampRating.Data.Models;

public class Camp : BaseDeletableModel<string>
{
    public Camp()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; }
 
    [Required]
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
    public string Description { get; set; }
    
    [Required]
    [Precision(DecimalPrecision, DecimalScale)]
    public string Latitude { get; set; } // width

    [Required]
    [Precision(DecimalPrecision, DecimalScale)]
    public string Longitude { get; set; } // length

    [Required]
    public string ImgUrl { get; set; }
}
