using College2Career.Models;

namespace College2Career.Repository
{
    public interface ICollegesRepository
    {
        Task addCollegesInformation(Colleges colleges);
    }
}
