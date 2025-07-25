namespace Stylo_Spin.Helper
{
    public class ImageHelper
    {
        public async static Task<byte[]> ConvertToBytesAsync(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

    }
}
