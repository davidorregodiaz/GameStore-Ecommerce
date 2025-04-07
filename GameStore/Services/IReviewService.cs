using GameStore.Models;

namespace GameStore.Services;

public interface IReviewService
{
    Task AddReview(Review review);
}