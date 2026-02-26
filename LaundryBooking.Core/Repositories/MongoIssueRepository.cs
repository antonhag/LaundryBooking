using LaundryBooking.Core.Data;
using LaundryBooking.Core.Enums;
using LaundryBooking.Core.Interfaces;
using LaundryBooking.Core.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace LaundryBooking.Core.Repositories
{
    public class MongoIssueRepository : IIssueRepository
    {
        private readonly IMongoCollection<IssueReport> _issues; // Kan bara bli initierad en gång, i konstruktorn

        public MongoIssueRepository(string connectionString)
        {
            _issues = MongoDbContext.GetIssueReportCollection(connectionString);
        }

        public async Task<List<IssueReport>> GetAllIssuesAsync()
        {
            List<IssueReport> issues = await _issues.AsQueryable().ToListAsync();
            return issues;
        }

        public async Task CreateIssueAsync(IssueReport issue)
        {
            await _issues.InsertOneAsync(issue);
        }

        public async Task UpdateIssueStatusAsync(string id, IssueStatus status)
        {
            var update = Builders<IssueReport>.Update.Set(i => i.Status, status);
            await _issues.UpdateOneAsync(i => i.Id == id, update);
        }

        public async Task DeleteIssueAsync(string id)
        {
            await _issues.DeleteOneAsync(i => i.Id == id);
        }
    }
}
