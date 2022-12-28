using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RunGroopsWebSite.Helpers;
using RunGroopsWebSite.Interfaces;
using System.Net.Http.Headers;

namespace RunGroopsWebSite.Services
{
    public class PhotoService : IPhotoService
    {
        public readonly FileStack _fileStack;

        public PhotoService(IConfiguration config)
        {
            _fileStack = config.GetSection("FileStack").Get<FileStack>();
        }

        public Task<string> DeletePhotoAsync(string url)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdatePhotoAsync(IFormFile file, string url)
        {
            string[] names = url.Split("/");
            string name = names[names.Length - 1];

            using (var read = file.OpenReadStream())
            {
                byte[] fileBytes = new byte[read.Length];
                read.Read(fileBytes, 0, (int)read.Length);

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_fileStack.BaseUrl + "file/");
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/jpeg"));


                    var responseMessage = await client.PostAsync($"{name}?policy={_fileStack.Policy}&signature={_fileStack.Signature}", new ByteArrayContent(fileBytes));

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        ResponsePhotoModel data = await responseMessage.Content.ReadFromJsonAsync<ResponsePhotoModel>();
                        Console.WriteLine(data.url + "\n" + data.filename);
                        return data.url;
                    }
                    else
                    {
                        Console.WriteLine("Error Code" + responseMessage.StatusCode + " : Message - " + responseMessage.ReasonPhrase);
                        throw new Exception("Error Code" + responseMessage.StatusCode + " : Message - " + responseMessage.ReasonPhrase);
                    }
                }
            }
        }

        public async Task<string> UploadPhotoAsync(IFormFile file)
        {
            using(var read = file.OpenReadStream())
            {
                byte[] fileBytes = new byte[read.Length];
                read.Read(fileBytes, 0, (int)read.Length);

                using (var client = new HttpClient())
                {
                    
                    client.BaseAddress = new Uri(_fileStack.BaseUrl + "store/");
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/jpeg"));


                    var responseMessage = await client.PostAsync($"S3?key={_fileStack.Apikey}", new ByteArrayContent(fileBytes));

                if (responseMessage.IsSuccessStatusCode)
                    {
                        ResponsePhotoModel data = await responseMessage.Content.ReadFromJsonAsync<ResponsePhotoModel>();
                        Console.WriteLine(data.url + "\n" + data.filename);
                        return data.url;
                    }
                    else
                    {
                        Console.WriteLine("Error Code" + responseMessage.StatusCode + " : Message - " + responseMessage.ReasonPhrase);
                        throw new Exception("Error Code" + responseMessage.StatusCode + " : Message - " + responseMessage.ReasonPhrase);
                    }
                }
            }
        }
    }
}
