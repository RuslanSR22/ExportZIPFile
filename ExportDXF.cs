using netDxf;
using netDxf.Entities;
using netDxf.Objects;

namespace CreateZIPFile1
{
    internal class ExportDXF
    {
        public static void CreateDXF(string pathImgFile)
        {
            // Создание нового DXF файла
            DxfDocument dxf = new DxfDocument();

            // Нужно тут добавить путь к изображению
            dxf.Entities.Add(CreateImage("pathImgFile"));
            // Сохранение документа в файл
            // Нужно указать путь на зипфайл
            dxf.Save("");
        }

        private static EntityObject CreateImage(string pathImgFile)
        {
            var imageDefenition = new ImageDefinition(pathImgFile);
            var image = new Image(imageDefenition, new Vector2(-800, -800), 1600, 1600);

            return image;
        }
    }
}
