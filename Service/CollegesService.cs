using College2Career.DTO;
using College2Career.HelperServices;
using College2Career.Models;
using College2Career.Repository;

namespace College2Career.Service
{
    public class CollegesService : ICollegesService
    {
        private readonly ICollegesRepository collegesRepository;
        private readonly ICloudinaryService cloudinaryService;

        public CollegesService(ICollegesRepository collegesRepository, ICloudinaryService cloudinaryService)
        {
            this.collegesRepository = collegesRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<ServiceResponse<string>> addCollegesInformation(CollegesDTO collegesDTO)
        {
            try
            {
                var response = new ServiceResponse<string>();

                var imageURL = await cloudinaryService.uploadImages(collegesDTO.profilePicture);

                var newCollege = new Colleges
                {
                    collegeName = collegesDTO.collegeName,
                    establishedDate = collegesDTO.establishedDate,
                    contactNumber = collegesDTO.contactNumber,
                    profilePicture = imageURL,
                    address = collegesDTO.address,
                    city = collegesDTO.city,
                    state = collegesDTO.state
                };

                await collegesRepository.addCollegesInformation(newCollege);

                response.data = "1";
                response.message = "College profile created successfully.";
                response.status = true;

                return response;
            }catch(Exception ex)
            {
                Console.WriteLine("ERROR in college service in addCollegeInformation method: " + ex.Message);
                throw;
            }
        }
    }
}
