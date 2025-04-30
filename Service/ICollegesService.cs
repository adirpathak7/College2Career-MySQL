using College2Career.DTO;
using College2Career.HelperServices;
using College2Career.Models;

namespace College2Career.Service
{
    public interface ICollegesService
    {
        Task<ServiceResponse<string>> addCollegesInformation(CollegesDTO collegesDTO);
    }
}
