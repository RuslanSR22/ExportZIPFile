using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CreateZIPFile1
{
    internal class GisService
    {
        public static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(10);

            return client;
        }
        public static async Task<string> GetImageAsync()
        {
            string mapServiceUrl = "https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/export?";

            string parameters = "bbox=37.620035570966834%2C55.7891700442175%2C37.64560422903317%2C55.80363995578249&bboxSR=4326&size=1600%2C1600&format=jpg&f=image";

            string fullUrl = $"{mapServiceUrl}{parameters}";

            // Инициализация HttpClient
            using (var httpClient = CreateHttpClient())
            {
                try
                {
                    // Выполнение GET-запроса
                    HttpResponseMessage response = await httpClient.GetAsync(fullUrl);

                    // Проверка успешности запроса
                    if (response.IsSuccessStatusCode)
                    {
                        using (var imageStream = await response.Content.ReadAsStreamAsync())
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                await imageStream.CopyToAsync(memoryStream);
                                byte[] imageBytes = memoryStream.ToArray();
                                string base64String = Convert.ToBase64String(imageBytes);
                                return base64String;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка при загрузке карты. Код ответа: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            return null;
        }
    }
}
