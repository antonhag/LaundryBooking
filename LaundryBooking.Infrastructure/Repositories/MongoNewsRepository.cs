using LaundryBooking.Domain.Entities;
using LaundryBooking.Domain.Interfaces;
using LaundryBooking.Infrastructure.Data;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace LaundryBooking.Infrastructure.Repositories;

public class MongoNewsRepository : INewsRepository
{
    private readonly IMongoCollection<NewsPost> _newsPosts;

    public MongoNewsRepository(MongoDbContext context)
    {
        _newsPosts = context.NewsPosts;
    }
    
    public async Task<List<NewsPost>> GetAllPostsAsync()
    {
        List<NewsPost> newsPosts = await _newsPosts.AsQueryable().ToListAsync();
        return newsPosts;
    }

    public async Task<List<NewsPost>> GetPostsByHousingCooperativeAsync(string housingCooperativeId)
    {
        return await _newsPosts.Find(p => p.HousingCooperativeId == housingCooperativeId).ToListAsync();                 
    }

    public async Task CreatePostAsync(NewsPost post)
    {
        await _newsPosts.InsertOneAsync(post);
    }

    public async Task DeletePostAsync(string id)
    {
        await _newsPosts.DeleteOneAsync(p => p.Id == id);
    }
}