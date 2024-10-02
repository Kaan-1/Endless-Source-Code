using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Threading;


namespace Endless_it1{
public class ImageAPI
{
    private static readonly HttpClient httpClient = new();
    private readonly string  apiKey = "Bearer "+ UrlsAPI.GetImgAPIKey;
    private readonly ObjectStore os;


    public ImageAPI(ObjectStore os){
        this.os = os;
    }

    public async Task<Texture2D> getImage(string pmt){
        Debug.Log("sent image url request");
        os.loadUI.SetLoadingText("Sent image url request to GetImg Image Generation services...");
        var imgUrl = await GetImageUrlAsync(pmt);
        if (imgUrl == "ImageAPIOffline"){
            os.loadUI.SetLoadingText("GetImg API services are offline. Opting to default background...");
            Debug.Log("GetImg API services are offline. Opting to default background...");
            return os.iStore.ImageAPIOfflineImage.texture as Texture2D;
        }
        else{
            os.loadUI.SetLoadingText("Image url received from the GetImg Image Generation services...");
            os.loadUI.SetLoadingText("Sent download request to the image url by GetImg Image Generation services...");
            Debug.Log("sent download request from the image url");
            var texture = await DownloadImageAsync(imgUrl);
            os.loadUI.SetLoadingText("Image successfully downloaded from the GetImg Image Generation services...");
            Debug.Log("image download completed");
            return texture;
        }
    }


    private async Task<string> GetImageUrlAsync(string pmt)
    {
        if (GameData.imageAPIWorks){
            var data = new
            {
                width = 512,
                height = 256,
                steps = 20,
                prompt = pmt,
                response_format = "url"
            };

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey.Split(' ')[1]);
            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20))){
                try{
                    var response = await httpClient.PostAsync(UrlsAPI.imageAPIUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();
                        var responseData = JsonConvert.DeserializeObject<ApiResponse>(responseJson);
                        return responseData.Url;
                    }
                    else
                    {
                        Debug.LogError($"Failed to retrieve data from API. Status code: {response.StatusCode}");
                        GameData.imageAPIWorks = false;
                        return "ImageAPIOffline";
                    }
                }
                catch (TaskCanceledException){
                    // Handle timeout
                    os.loadUI.SetLoadingText("Request timed out.");
                    GameData.imageAPIWorks = false;
                    return "ImageAPIOffline";
                }
                catch (Exception ex){
                    // Handle other exceptions
                    os.loadUI.SetLoadingText($"Error: {ex.Message}");
                    GameData.imageAPIWorks = false;
                    return "ImageAPIOffline";
                }
            }
        }
        else {
            return "ImageAPIOffline";
        }
    }

    private async Task<Texture2D> DownloadImageAsync(string imageUrl)
    {
        using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10))){
            try{
                using (var webRequest = UnityWebRequestTexture.GetTexture(imageUrl))
                {
                    var operation = webRequest.SendWebRequest();
                    while (!operation.isDone)
                    {
                        await Task.Yield();
                    }

                    if (webRequest.result == UnityWebRequest.Result.Success)
                    {
                        return DownloadHandlerTexture.GetContent(webRequest);
                    }
                    else
                    {
                        Debug.LogError($"Failed to retrieve image. Status code: {webRequest.responseCode}");
                        GameData.imageAPIWorks = false;
                        return os.iStore.ImageAPIOfflineImage.texture as Texture2D;
                    }
                }
            }
            catch (TaskCanceledException){
                // Handle timeout
                os.loadUI.SetLoadingText("Request timed out.");
                GameData.imageAPIWorks = false;
                return os.iStore.ImageAPIOfflineImage.texture as Texture2D;
            }
            catch (Exception ex){
                // Handle other exceptions
                os.loadUI.SetLoadingText($"Error: {ex.Message}");
                GameData.imageAPIWorks = false;
                return os.iStore.ImageAPIOfflineImage.texture as Texture2D;
            }
        }
        
    }

    private class ApiResponse
    {
        public string Url { get; set; }
    }
}
}