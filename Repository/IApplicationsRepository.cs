using College2Career.DTO;
using College2Career.Models;

namespace College2Career.Repository
{
    public interface IApplicationsRepository
    {
        Task<bool> alreadyAppliedForVacancy(int studentId, int vacancyId);
        Task<bool> isOfferAccepted(int studentId);
        Task newApplications(Applications applications);
        Task<List<Applications>> getAllAppliedApplicationsByCompanyId(int companyId);
    }
}