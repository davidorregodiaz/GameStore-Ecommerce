using GameStore.Models;
using GameStore.Repositories;

namespace GameStore.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepositorie _reviewRepository;

    public ReviewService(IReviewRepositorie reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }


    public async Task AddReview(Review review)
    {
        await _reviewRepository.AddAsync(review);
        await _reviewRepository.SaveAsync();
    }
}