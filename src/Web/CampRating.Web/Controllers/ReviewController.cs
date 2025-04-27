using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CampRating.Common;
using CampRating.Services.Interfaces;
using CampRating.Web.ViewModels.Camp;
using CampRating.Web.ViewModels.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampRating.Web.Controllers;

public class ReviewController : BaseController
{
    private readonly IReviewService _reviewService;
    private readonly ICampService _campService;

    public ReviewController(IReviewService reviewService, ICampService campService)
    {
        this._reviewService = reviewService;
        this._campService = campService;
    }

    [HttpGet]
    public async Task<IActionResult> All(string campId)
    {
        if (string.IsNullOrEmpty(campId))
        {
            return BadRequest();
        }

        var camp = this._campService.GetById<CampDetailsViewModel>(campId);
        
        if (camp == null)
        {
            return NotFound();
        }

        string userId = null;
        bool hasUserReviewed = false;
        
        if (this.User.Identity.IsAuthenticated)
        {
            userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            hasUserReviewed = await this._reviewService.HasUserReviewedCampAsync(userId, campId);
        }
        
        var viewModel = new CampReviewsViewModel
        {
            CampId = campId,
            CampName = camp.Name,
            CampImage = camp.ImgUrl,
            AverageRating = await this._reviewService.GetAverageRatingByCampIdAsync(campId),
            ReviewsCount = await this._reviewService.GetReviewsCountByCampIdAsync(campId),
            HasUserReviewed = hasUserReviewed,
            NewReview = new ReviewFormModel() { CampId = campId },
            Reviews = await this._reviewService.GetAllByCampIdAsync(campId)
        };

        foreach (var review in viewModel.Reviews)
        {
            review.UserId = userId;
        }

        return View(viewModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> SubmitReview(string CampId, int Rating, string Comment)
    {
        if (string.IsNullOrEmpty(CampId) || Rating < 1 || Rating > 5)
        {
            TempData["ErrorMessage"] = "Invalid review data. Please provide a valid rating between 1 and 5.";
            return RedirectToAction(nameof(All), new { campId = CampId });
        }

        var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var username = this.User.Identity.Name;

        try
        {
            var hasUserReviewed = await this._reviewService.HasUserReviewedCampAsync(userId, CampId);
            
            if (hasUserReviewed)
            {
                TempData["ErrorMessage"] = "You have already reviewed this camp.";
                return RedirectToAction(nameof(All), new { campId = CampId });
            }
            
            var model = new ReviewFormModel()
            {
                CampId = CampId,
                Rating = Rating,
                Comment = Comment
            };
            
            await this._reviewService.CreateAsync(model, userId, username);
            
            TempData["SuccessMessage"] = "Your review has been added successfully!";
            return RedirectToAction(nameof(All), new { campId = CampId });
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error creating review: {ex.Message}";
            return RedirectToAction(nameof(All), new { campId = CampId });
        }
    }

    [HttpPost]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public async Task<IActionResult> DeleteReview(string id, string campId)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest();
        }

        try
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this._reviewService.DeleteAsync(id, userId);
            
            TempData["SuccessMessage"] = "Your review has been deleted successfully!";
            return RedirectToAction(nameof(All), new { campId });
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error deleting review: {ex.Message}";
            return RedirectToAction(nameof(All), new { campId });
        }
    }
}