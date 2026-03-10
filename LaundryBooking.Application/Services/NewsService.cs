using LaundryBooking.Application.Interfaces;
using LaundryBooking.Domain.Entities;
using LaundryBooking.Domain.Interfaces;

namespace LaundryBooking.Application.Services;

public class NewsService : INewsService
{
    private readonly INewsRepository _newsRepository;

    public NewsService(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }
    
    public async Task<List<NewsPost>> GetAllPostsAsync()
    {
        var newsPosts = await _newsRepository.GetAllPostsAsync();
        return newsPosts;
    }

    public async Task<List<NewsPost>> GetPostsByHousingCooperativeAsync(string housingCooperativeId)
    {
        return await _newsRepository.GetPostsByHousingCooperativeAsync(housingCooperativeId);
    }

    public async Task CreatePostAsync(NewsPost post)
    {
        await _newsRepository.CreatePostAsync(post);
    }

    public async Task DeletePostAsync(string id)
    {
        await _newsRepository.DeletePostAsync(id);
    }
}