using GameStore.Data;
using GameStore.Models;

namespace GameStore.Repositories;

public class ReviewRepositorie : BaseRepositorie<Review>,IReviewRepositorie
{
    public ReviewRepositorie(AppDbContext context) : base(context)
    {
    }
}