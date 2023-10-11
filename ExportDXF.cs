using netDxf;
using netDxf.Entities;
using netDxf.Objects;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace CreateZIPFile1
{
	internal class ExportDXF
	{
		public static void CreateDXF(string sourceImage)
		{
			string imagePath = SaveImage(sourceImage);
			
			DxfDocument dxf = new DxfDocument();
			dxf.Entities.Add(CreateImage(imagePath));
			
			//сохранение dxf во временную директорию
			string dxfFilePath = Path.Combine(Environment.CurrentDirectory, "map.dxf");
			dxf.Save(dxfFilePath);

			// Обновляем путь в DXF файле
			string relativeImagePath = "Images/" + Path.GetFileName(imagePath);
			UpdateDxfFileWithRelativePath(dxfFilePath, relativeImagePath, imagePath);
			//куда хотим сохранять арихв
			string archivePath = @"D:\archive.zip";
			if (File.Exists(archivePath)) {
				using (var archive = ZipFile.Open(archivePath, ZipArchiveMode.Update)) {
					ZipArchiveEntry mapEntry = archive.GetEntry("map.dxf");
					mapEntry.Delete(); //удаляю если уже есть
					archive.CreateEntryFromFile(dxfFilePath, Path.GetFileName(dxfFilePath));
					archive.CreateEntryFromFile(imagePath, "Images/" + Path.GetFileName(imagePath));
				}
			} else {
				using (var archive = ZipFile.Open(archivePath, ZipArchiveMode.Create)) {
					archive.CreateEntryFromFile(dxfFilePath, Path.GetFileName(dxfFilePath));
					archive.CreateEntryFromFile(imagePath, "Images/" + Path.GetFileName(imagePath));
				}
			}

			File.Delete(dxfFilePath);
			File.Delete(imagePath);
		}

		private static string SaveImage(string sourceImage)
		{
			byte[] imageBytes = Convert.FromBase64String(sourceImage);
			string jpgFilePath = Path.Combine(Environment.CurrentDirectory, "image.jpg"); //путь до изображения во временную диреткорию
			File.WriteAllBytes(jpgFilePath, imageBytes);

			return jpgFilePath;
		}

		private static void UpdateDxfFileWithRelativePath(string dxfFilePath, string relativeImagePath, string imagePath)
		{
			// Читаем содержимое DXF файла
			string dxfContent = File.ReadAllText(dxfFilePath);

			// Заменяем абсолютный путь на релевантный путь
			string updatedDxfContent = dxfContent.Replace(imagePath, relativeImagePath);

			// Перезаписываем DXF файл с обновленным содержимым
			File.WriteAllText(dxfFilePath, updatedDxfContent, Encoding.Default);
		}

		private static EntityObject CreateImage(string pathImgFile)
		{
			ImageDefinition imageDefenition = new ImageDefinition(pathImgFile);
			Image image = new Image(imageDefenition, new Vector2(-800, -800), 1600, 1600);

			return image;
		}
	}
}