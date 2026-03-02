using LaundryBooking.Domain.Entities;
using LaundryBooking.Domain.Enums;

namespace LaundryBooking.Application.Interfaces;

public interface IIssueService
{
    Task<List<IssueReport>> GetAllIssuesAsync();
    Task CreateIssueAsync(IssueReport issue);
    Task UpdateIssueStatusAsync(string id, IssueStatus status);
    Task DeleteIssueAsync(string id);
}
