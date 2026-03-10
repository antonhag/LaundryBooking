using LaundryBooking.Domain.Entities;

namespace LaundryBooking.Application.Interfaces;

public interface INewsService
{
    Task<List<NewsPost>> GetAllPostsAsync();
    Task<List<NewsPost>> GetPostsByHousingCooperativeAsync(string housingCooperativeId);
    Task CreatePostAsync(NewsPost post);
    Task DeletePostAsync(string id);
}