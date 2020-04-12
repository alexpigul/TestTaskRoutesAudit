using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestTaskRoutesAudit.Services
{
    public interface IJsonFileService
    {
        Task<Models.InputJson.Json> ReadJsonFileAsync(string filePath);
        Task WriteJsonFileAsync(Models.OutputJson.OutputJson json, string outputPath);
    }

    public class JsonFileService : IJsonFileService
    {
        public async Task<Models.InputJson.Json> ReadJsonFileAsync(string filePath)
        {
            try
            {
                string stringJson = await File.ReadAllTextAsync(filePath);

                Models.InputJson.Json json = JsonConvert.DeserializeObject<Models.InputJson.Json>(stringJson);

                return json;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception while reading json by path: {filePath}", ex);
            }
        }

        public async Task WriteJsonFileAsync(Models.OutputJson.OutputJson json, string outputPath)
        {
            try
            {
                string stringJson = JsonConvert.SerializeObject(json);

                await File.WriteAllTextAsync(outputPath, stringJson);
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception while writing json by path: {outputPath}", ex);
            }
        }
    }
}
