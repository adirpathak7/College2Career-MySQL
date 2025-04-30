
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

public interface ICloudinaryService
{
    Task<string> uploadImages(IFormFile file);
}

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary cloudinary;
    public CloudinaryService(IConfiguration configuration)
    {
        var cloudName = configuration["Cloudinary:CloudName"];
        var apiKey = configuration["Cloudinary:ApiKey"];
        var apiSecret = configuration["Cloudinary:ApiSecret"];

        var account = new Account(cloudName, apiKey, apiSecret);
        cloudinary = new Cloudinary(account);
    }
    public async Task<string> uploadImages(IFormFile file)
    {
        if (file.Length > 0)
        {
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Width(250).Height(250).Crop("fill")
                };
                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                return uploadResult.SecureUrl.ToString();
            }
        }
        return null;
    }
}