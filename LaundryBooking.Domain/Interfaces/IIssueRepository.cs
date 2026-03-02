using LaundryBooking.Domain.Enums;
using LaundryBooking.Domain.Entities;

namespace LaundryBooking.Domain.Interfaces
{
    public interface IIssueRepository
    {
        Task<List<IssueReport>> GetAllIssuesAsync();
        Task CreateIssueAsync(IssueReport issue);
        Task UpdateIssueStatusAsync(string id, IssueStatus status);
        Task DeleteIssueAsync(string id);
    }
}
