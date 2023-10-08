using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            string pathImgFile = "";
            ExportDXF.CreateDXF(pathImgFile);

            // Добавить метод сохранения зип файла
        }
    }
}
