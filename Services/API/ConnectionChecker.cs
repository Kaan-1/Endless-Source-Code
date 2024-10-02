using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using GoogleTextToSpeech.Scripts.Data;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Endless_it1;


public class ConnectionChecker : MonoBehaviour{
    public async Task CheckConnections(Action<string> updateLoadingText){
        var internetConnectionTask = CheckForInternetConnection(updateLoadingText);
        var chatGPTConnectionTask = CheckChatGPTConnection(updateLoadingText);
        var getimgConnectionTask = CheckGetimgConnection(updateLoadingText);
        var googleTTSConnectionTask = CheckGoogleTTSConnection(updateLoadingText);
        await Task.WhenAll(internetConnectionTask, chatGPTConnectionTask, getimgConnectionTask, googleTTSConnectionTask);
        GameData.internetWorks = internetConnectionTask.Result;
        GameData.textAPIWorks = chatGPTConnectionTask.Result;
        GameData.imageAPIWorks = getimgConnectionTask.Result;
        GameData.voiceAPIWorks = googleTTSConnectionTask.Result;
    }


    private async Task<bool> CheckForInternetConnection(Action<string> updateLoadingText){
        try{
            using (HttpClient client = new HttpClient()){
                updateLoadingText("Pinging google.com to check for internet connection...");
                client.Timeout = TimeSpan.FromSeconds(5);
                HttpResponseMessage response = await client.GetAsync("http://www.google.com");
                updateLoadingText("Internet connection verified...");
                return response.IsSuccessStatusCode;
            }
        }
        catch (Exception){
            updateLoadingText("Internet connection failed...");
            return false;
        }
    }


    private async Task<bool> CheckChatGPTConnection(Action<string> updateLoadingText){
        try{
            using HttpClient client = new();
            client.Timeout = TimeSpan.FromSeconds(5);
            updateLoadingText("Sending request to OpenAI chat completions API endpoint...");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer "+ UrlsAPI.openAIAPIKey);
            var content = new StringContent("{\"model\": \""+UrlsAPI.gptModel+"\", \"messages\": [{\"role\": \"system\", \"content\": \"ping\"}]}", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(UrlsAPI.textAPIUrl, content);
            updateLoadingText("OpenAI chat completions API endpoint is online...");
            return response.IsSuccessStatusCode;
        }
        catch (Exception){
            updateLoadingText("Connection with OpenAI chat completions API endpoint failed...");
            return false;
        }
    }


    private async Task<bool> CheckGetimgConnection(Action<string> updateLoadingText){
        try{
            using HttpClient client = new();
            client.Timeout = TimeSpan.FromSeconds(20);
            updateLoadingText("Sending request to GetImg stable diffusion API endpoint...");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UrlsAPI.GetImgAPIKey);
            var data = new
            {
                width = 256,
                height = 256,
                steps = 1,
                prompt = "test",
                response_format = "url"
            };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(UrlsAPI.imageAPIUrl, content);
            updateLoadingText("GetImg stable diffusion API endpoint is online...");
            return response.IsSuccessStatusCode;
        }
        catch (Exception){
            updateLoadingText("Connection with GetImg stable diffusion API endpoint failed...");
            return false;
        }
    }


    public async Task<bool> CheckGoogleTTSConnection(Action<string> updateLoadingText)
            {
                string url=UrlsAPI.voiceAPIUrl;
                string apiKey=UrlsAPI.googleCloudAPIKey;
                var headers = new Dictionary<string, string>{
                    { "X-Goog-Api-Key", apiKey },
                    { "Content-Type", "application/json; charset=utf-8" }
                };
                updateLoadingText("Sending request to Google Cloud TTS API endpoint...");

                var dummyData = new DataToSend
                {
                    // Add any required dummy data here
                    input = new GoogleTextToSpeech.Scripts.Data.Input { text = "test" },
                    voice = new Voice{ languageCode = "en-US" },
                    audioConfig = new AudioConfig { audioEncoding = "LINEAR16" }
                };

                try
                {
                    var request = new UnityWebRequest(url, "POST");
                    var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(dummyData));
                    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                    request.downloadHandler = new DownloadHandlerBuffer();

                    foreach (var header in headers)
                    {
                        request.SetRequestHeader(header.Key, header.Value);
                    }

                    var operation = request.SendWebRequest();

                    while (!operation.isDone)
                        await Task.Yield();

                    if (request.responseCode == 200 || request.responseCode == 201)
                    {
                        updateLoadingText("Google Cloud TTS API endpoint is online...");
                        return true;
                    }
                    else
                    {
                        Debug.Log($"Connection with Google Cloud TTS API check failed. Status code: {request.responseCode}, Error: {request.error}");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Exception during connection check for Google Cloud TTS API: {ex.Message}");
                    return false;
                }
            }
    
    public class ConnectionResult
    {
        public bool InternetConnection { get; set; }
        public bool ChatGPTConnection { get; set; }
        public bool GetimgConnection { get; set; }
        public bool GoogleTTSConnection { get; set; }
    }
}