using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
    public class AdPlatformServiceTests
    {
        [Fact]
        // проверка корректности вывода на /ru/svrd/revda
        public void GetPlatformsForLocation_ShouldReturnCorrectPlatforms()
        {
            var service = new AdPlatformService();
            string testFilePath = "Ad.txt";
            // Создание файла вручную
            var fileContent = "Яндекс.Директ:/ru\nРевдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik\nГазета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl";
            File.WriteAllText(testFilePath, fileContent);

            service.LoadFromFile(testFilePath);

            var platforms = service.GetPlatformsForLocation("/ru/svrd/revda");

            Assert.Contains("Яндекс.Директ", platforms);
            Assert.Contains("Ревдинский рабочий", platforms);
            Assert.DoesNotContain("Газета уральских москвичей", platforms);

            File.Delete(testFilePath);
        }

        [Fact]
        // Проверка на несуществующий путь
        public void GetPlatformsForLocation_ShouldReturnEmptyListForNonExistingLocation()
        {
            var service = new AdPlatformService();
            string testFilePath = "Ad.txt";
            // Использование существующего файла
            service.LoadFromFile(testFilePath);

            var platforms = service.GetPlatformsForLocation("/svrd/unknown");

            Assert.Empty(platforms);

            File.Delete(testFilePath);
        }
    }
