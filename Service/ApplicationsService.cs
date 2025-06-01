using College2Career.DTO;
using College2Career.HelperServices;
using College2Career.Models;
using College2Career.Repository;

namespace College2Career.Service
{
    public class ApplicationsService : IApplicationsService
    {
        private readonly IApplicationsRepository applicationsRepository;
        private readonly IStudentsRepository studentsRepository;
        private readonly ICompaniesRepository companiesRepository;

        public ApplicationsService(IApplicationsRepository applicationsRepository, IStudentsRepository studentsRepository, ICompaniesRepository companiesRepository)
        {
            this.applicationsRepository = applicationsRepository;
            this.studentsRepository = studentsRepository;
            this.companiesRepository = companiesRepository;
        }

        public async Task<ServiceResponse<string>> newApplications(ApplicationsDTO applicationsDTO, int usersId)
        {
            try
            {
                var response = new ServiceResponse<string>();

                var existStudent = await studentsRepository.getStudentsProfileByUsersId(usersId);

                if (existStudent == null)
                {
                    response.data = "0";
                    response.message = "Student not found for the user.";
                    response.status = false;
                    return response;
                }

                var studentId = existStudent.studentId;

                var applicationIsExist = await applicationsRepository.alreadyAppliedForVacancy(studentId, (int)applicationsDTO.vacancyId);

                if (applicationIsExist)
                {
                    response.data = "0";
                    response.message = "You have already applied for this vacancy.";
                    response.status = false;
                    return response;
                }

                var isOfferAccepted = await applicationsRepository.isOfferAccepted(studentId);
                if (isOfferAccepted)
                {
                    response.data = "0";
                    response.message = "You have already accepted an offer. You cannot apply for another vacancy.";
                    response.status = false;
                    return response;
                }
                var studentApplication = new Applications
                {
                    studentId = studentId,
                    vacancyId = applicationsDTO.vacancyId,
                };

                await applicationsRepository.newApplications(studentApplication);

                response.data = "1";
                response.message = "Application sent successfully.";
                response.status = true;

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in ApplicationsService in newApplications method: " + ex.Message);
                throw;
            }
        }

        public async Task<ServiceResponse<List<VacanciesAppliedStudentsDTO>>> getAllAppliedApplicationsByCompanyId(int usersId)
        {
            try
            {
                var response = new ServiceResponse<List<VacanciesAppliedStudentsDTO>>();

                var existCompany = await companiesRepository.getCompanyProfileByUsersId(usersId);

                Console.WriteLine("existCompany data:- " + existCompany.companyId + "  " + existCompany.companyName);
                if (existCompany == null || existCompany.status != "activated")
                {
                    response.data = null;
                    response.message = "Company not found or not activated.";
                    response.status = false;
                    return response;
                }

                var companyId = existCompany.companyId;

                Console.WriteLine("companyId in controller:- " + companyId);

                var allApplications = await applicationsRepository.getAllAppliedApplicationsByCompanyId(companyId);

                var dataOfAppliedStudents = allApplications.Select(a => new VacanciesAppliedStudentsDTO
                {
                    applicationId = a.applicationId,
                    applicationsAppliedAt = a.createdAt,
                    applicationStatus = a.status,
                    studentId = (int)a.studentId,
                    studentName = a.Students?.studentName,
                    studentEmail = a.Students?.Users?.email,
                    studentRollNumber = a.Students?.rollNumber,
                    course = a.Students?.course,
                    graduationYear = a.Students?.graduationYear,
                    resumeURL = a.Students?.resume,
                    vacancyId = (int)a.vacancyId,
                    title = a.Vacancies?.title,
                    description = a.Vacancies?.description,
                    eligibility_criteria = a.Vacancies?.eligibility_criteria,
                    totalVacancy = a.Vacancies?.totalVacancy,
                    locationType = a.Vacancies?.locationType,
                    vacancyStatus = a.Vacancies?.status
                }).ToList();

                response.data = dataOfAppliedStudents;
                response.message = "All applied applications by students for each vacancy retrieved successfully.";
                response.status = true;

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in ApplicationsService in getAllAppliedApplicationsByVacancyId method: " + ex.Message);
                throw;
            }
        }
    }
}
