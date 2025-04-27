using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CampRating.Data.Common.Repositories;
using CampRating.Data.Models;
using CampRating.Services.Interfaces;
using CampRating.Web.ViewModels.Review;
using Microsoft.EntityFrameworkCore;

namespace CampRating.Services;

public class ReviewService : IReviewService
{
    private readonly IDeletableEntityRepository<Review> _reviewRepository;
    private readonly IMapper _mapper;

    public ReviewService(
        IDeletableEntityRepository<Review> reviewRepository,
        IMapper mapper)
    {
        this._reviewRepository = reviewRepository;
        this._mapper = mapper;
    }

    public async Task CreateAsync(ReviewFormModel model, string userId, string username)
    {
        var review = new Review
        {
            CampId = model.CampId,
            UserId = userId,
            Username = username,
            Rating = model.Rating,
            Comment = model.Comment
        };
        
        await this._reviewRepository.AddAsync(review);
        await this._reviewRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<ReviewInListViewModel>> GetAllByCampIdAsync(string campId)
    {
        var reviews = await this._reviewRepository.AllAsNoTracking()
            .Where(r => r.CampId == campId && r.IsDeleted == false)
            .OrderByDescending(r => r.CreatedOn)
            .ToListAsync();
            
        return this._mapper.Map<IEnumerable<ReviewInListViewModel>>(reviews);
    }

    public async Task<double> GetAverageRatingByCampIdAsync(string campId)
    {
        var reviews = await this._reviewRepository.AllAsNoTracking()
            .Where(r => r.CampId == campId && r.IsDeleted == false)
            .ToListAsync();
            
        if (!reviews.Any())
        {
            return 0;
        }
        
        return Math.Round(reviews.Average(r => r.Rating), 1);
    }

    public async Task<int> GetReviewsCountByCampIdAsync(string campId)
    {
        return await this._reviewRepository.AllAsNoTracking()
            .Where(r => r.CampId == campId && r.IsDeleted == false)
            .CountAsync();
    }

    public async Task<bool> HasUserReviewedCampAsync(string userId, string campId)
    {
        return await this._reviewRepository.AllAsNoTracking()
            .AnyAsync(r => r.UserId == userId && r.CampId == campId && r.IsDeleted == false);
    }

    public async Task DeleteAsync(string reviewId, string userId)
    {
        var review = await this._reviewRepository.All()
            .FirstOrDefaultAsync(r => r.Id == reviewId && r.IsDeleted == false);
            
        if (review == null)
        {
            throw new InvalidOperationException($"Review with ID {reviewId} not found.");
        }
        
        // Check if the user is the owner of the review
        if (review.UserId != userId)
        {
            throw new InvalidOperationException("You can only delete your own reviews.");
        }
        
        this._reviewRepository.Delete(review);
        await this._reviewRepository.SaveChangesAsync();
    }
}