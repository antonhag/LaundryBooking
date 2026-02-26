using LaundryBooking.Core.Enums;
using LaundryBooking.Core.Interfaces;
using LaundryBooking.Core.Models;

namespace LaundryBooking.Core.Services
{
    public class IssueService
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
