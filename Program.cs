using CreateZIPFile1;
using System.Threading.Tasks;

namespace ExportZIPFile
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			// Нужно положить эту картинку в зипфайл
			// https://codebeautify.org/base64-to-image-converter тут можно посмотреть картинку
			string image = await GisService.GetImageAsync();
			ExportDXF.CreateDXF(image);
		}
	}
}
