using Aspose.Zip;
using Aspose.Zip.Saving;
using CreateZIPFile1;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ExportZIPFile
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Нужно положить эту картинку в зипфайл
            // https://codebeautify.org/base64-to-image-converter тут можно посмотреть картинку
            var image = await GisService.GetImageAsync();
            // Путь до изображения
            string pathImgFile = SaveImage(image);
            ExportDXF.CreateDXF(pathImgFile);

            // Добавить метод сохранения зип файла
        }
		private static string SaveImage(string sourceImage)
		{
			byte[] imageBytes = Convert.FromBase64String(sourceImage);
			string jpgFilePath = @"D:\image.jpg"; //путь до изображения
			File.WriteAllBytes(jpgFilePath, imageBytes);

			string destination = @"D:\map.zip"; //путь до zip
			using (FileStream zipFile = File.Open(destination, FileMode.Create)) {
				// Файл для добавления в архив
				using (FileStream source = File.Open(jpgFilePath, FileMode.Open, FileAccess.Read)) {
					using (var archive = new Archive(new ArchiveEntrySettings())) {
						// Добавить файл в архив
						archive.CreateEntry("image.jpg", source);
						// ZIP-файл
						archive.Save(zipFile);
					}
				}
			}
			return jpgFilePath; // возвращаю путь на jpg image, а не на zip, потому что конструктор ImageDefinition не поддерживает такой формат
		}
	}
}
