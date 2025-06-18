using System.IO;
using System.Net.Http;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        public static async Task<bool> TryDownload(string url, string path)
        {
            if(string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(path))
            {
                return false;
            }
            
            bool result = true;

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    try
                    {
                        HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Head, url);
                        HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                        if (!httpResponseMessage.IsSuccessStatusCode)
                        {
                            return false;
                        }

                        httpResponseMessage = await httpClient.GetAsync(url);
                        httpResponseMessage.EnsureSuccessStatusCode();

                        Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();
                        if(stream != null)
                        {
                            string directory = Path.GetDirectoryName(path);
                            if(!Directory.Exists(directory))
                            {
                                Directory.CreateDirectory(directory);
                            }

                            if (Directory.Exists(directory))
                            {
                                using (FileStream fileStream = File.Create(path))
                                {
                                    await stream.CopyToAsync(fileStream);
                                }
                            }
                        }
                    }
                    catch
                    {
                        result = false;
                    }
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
