namespace Cars_automobile.Helpers
{
    public static class ImageHelper
    {
        public static bool IsCorrectType(this IFormFile imageFile)
            => imageFile.ContentType.Contains("image");

        public static bool IsCorrectSize(this IFormFile imageFile, float kb = 200)
            => imageFile.Length <= kb * 1024;

        public static async Task<string> SaveImageFileAsync(this IFormFile imageFile, string imageToSavePath)
        {
            string imageFileName = Path.Combine(Guid.NewGuid() + imageFile.FileName);

            string imageFilePath = Path.Combine(PathConstants.RootPath, imageToSavePath, imageFileName);

            using (FileStream fs = new(imageFilePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fs);
            }

            return imageFileName;
        }
    }
}
