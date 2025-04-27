using System.ComponentModel.DataAnnotations;
using static CampRating.Common.EntityValidationConstants.Camp;

namespace CampRating.Web.ViewModels.Camp
{
    public class CampFormModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Name must be between {2} and {1} characters.")]
        [Display(Name = "Camp Name")]
        public string Name { get; set; }
 
        [Required(ErrorMessage = "Description is required")]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = "Description must be between {2} and {1} characters.")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Latitude is required")]
        [Display(Name = "Latitude")]
        public string Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is required")]
        [Display(Name = "Longitude")]
        public string Longitude { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [Url(ErrorMessage = "Please enter a valid URL for the image.")]
        [Display(Name = "Image URL")]
        public string ImgUrl { get; set; }
    }
}