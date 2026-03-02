using LaundryBooking.Application.Interfaces;
using LaundryBooking.Domain.Enums;
using LaundryBooking.Domain.Interfaces;
using LaundryBooking.Domain.Entities;

namespace LaundryBooking.Application.Services
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepository;

        public IssueService(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        public async Task<List<IssueReport>> GetAllIssuesAsync()
        {
            var issueReports = await _issueRepository.GetAllIssuesAsync();
            return issueReports;
        }

        public async Task CreateIssueAsync(IssueReport issue)
        {
            await _issueRepository.CreateIssueAsync(issue);
        }

        public async Task UpdateIssueStatusAsync(string id, IssueStatus status)
        {
            await _issueRepository.UpdateIssueStatusAsync(id, status);
        }

        public async Task DeleteIssueAsync(string id)
        {
            await _issueRepository.DeleteIssueAsync(id);
        }
    }
}
