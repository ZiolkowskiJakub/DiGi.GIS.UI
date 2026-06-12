using System.IO;
using System.Net.Http;

namespace DiGi.GIS.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// Asynchronously attempts to download a file from the specified URL and save it to the provided local path.
        /// </summary>
        /// <param name="url">The URL of the resource to download.</param>
        /// <param name="path">The destination file path where the downloaded content will be saved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the download was successful; otherwise, <see langword="false"/>.</returns>
        public static async Task<bool> TryDownload(string url, string path)
        {
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            bool result = true;

            try
            {
                using HttpClient httpClient = new();
                try
                {
                    HttpRequestMessage httpRequestMessage = new(HttpMethod.Head, url);
                    HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        return false;
                    }

                    httpResponseMessage = await httpClient.GetAsync(url);
                    httpResponseMessage.EnsureSuccessStatusCode();

                    Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();
                    if (stream != null)
                    {
                        string? directory = Path.GetDirectoryName(path);
                        if (!string.IsNullOrWhiteSpace(directory))
                        {
                            if (!Directory.Exists(directory))
                            {
                                Directory.CreateDirectory(directory);
                            }

                            if (Directory.Exists(directory))
                            {
                                using FileStream fileStream = File.Create(path);
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
            catch
            {
                result = false;
            }

            return result;
        }
    }
}