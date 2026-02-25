using LaundryBooking.Core.Enums;
using LaundryBooking.Core.Models;

namespace LaundryBooking.Core.Interfaces
{
    public interface IIssueRepository
    {
        Task<List<IssueReport>> GetAllIssuesAsync();
        Task CreateIssueAsync(IssueReport issue);
        Task UpdateIssueStatusAsync(string id, IssueStatus status);
        Task DeleteIssueAsync(string id);
    }
}
