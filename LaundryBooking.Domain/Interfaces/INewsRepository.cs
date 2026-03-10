using LaundryBooking.Domain.Entities;
using LaundryBooking.Domain.Enums;

namespace LaundryBooking.Domain.Interfaces;

public interface INewsRepository
{
    Task<List<NewsPost>> GetAllPostsAsync();
    Task<List<NewsPost>> GetPostsByHousingCooperativeAsync(string housingCooperativeId);
    Task CreatePostAsync(NewsPost post);
    Task DeletePostAsync(string id);
}