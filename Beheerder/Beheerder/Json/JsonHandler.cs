using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Beheerder.Json
{
    public static class JsonHandler
    {

        private static readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        public static async Task<List<T>> GetAll<T>()
        {
            string filepath = $"{typeof(T).Name.ToLower()}.json";
            string content = await JsonHandler.GetFileContents(filepath);

            if (content == string.Empty)
                return new();

            try
            {
                var result = JsonSerializer.Deserialize<List<T>>(content, JsonHandler._options);
                return result ?? new();
            }
            catch (JsonException ex)
            {
                Console.WriteLine(ex.GetBaseException());
                return new();
            }
        }

        public static async Task Add<T>(T model)
        {
            if (model == null)
                return;


            var models = await JsonHandler.GetAll<T>();
            models.Add(model);

            string filepath = $"{typeof(T).Name.ToLower()}.json";

            try
            {
                string json = JsonSerializer.Serialize(models, JsonHandler._options);
                await JsonHandler.SaveFileContents(filepath, json);
            }
            catch (JsonException ex)
            {
                return;
            }
        }


        private static async Task<string> GetFileContents(string filepath)
        {
            if (!File.Exists(filepath)) 
                return string.Empty;

            using FileStream stream = new(filepath, FileMode.Open, FileAccess.Read);
            using (StreamReader reader = new(stream, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private static async Task<bool> SaveFileContents(string filepath, string content)
        {
            using FileStream stream = new(filepath, FileMode.Create, FileAccess.Write);
            using (StreamWriter writer = new(stream, Encoding.UTF8))
            {
                try
                {
                    await writer.WriteAsync(content);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

    }
}