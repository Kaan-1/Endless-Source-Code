using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Threading;
using System.IO;

public class APIconnector
{
    private readonly ObjectStore os;
    private readonly string logFilePath = "error_log.txt"; // Path to the log file
    private object requestBody;

    public APIconnector(ObjectStore os){
        this.os = os;
    }

    public async Task<string> getResponse(object[] inputs, object responseFormat = null){
        string apiKey = UrlsAPI.openAIAPIKey;
        string assistantMessage = "";
        using (HttpClient client = new HttpClient()){
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            if (responseFormat != null){
                requestBody = new{
                    model = UrlsAPI.gptModel, //type in the model to be used
                    messages = inputs,
                    response_format = responseFormat
                };
            }
            else{
                requestBody = new{
                    model = UrlsAPI.gptModel, //type in the model to be used
                    messages = inputs
                };
            }
            string json = JsonConvert.SerializeObject(requestBody);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            os.loadUI.SetLoadingText("Sent response request to OpenAI Chat Completion services...");
            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(15))){
                try{
                    HttpResponseMessage response = await client.PostAsync(UrlsAPI.textAPIUrl, content, cts.Token);
                    os.loadUI.SetLoadingText("Received response from OpenAI Chat Completion services...");
                    string responseString = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<OpenAIResponse>(responseString);
                    if (responseObject?.Choices == null){
                        LogError("Response null from OpenAI.");
                        os.loadUI.SetLoadingText("Response null from OpenAI.");
                        return null;
                    }
                    else if (responseObject.Choices.Count == 0){
                        LogError("Response empty from OpenAI.");
                        os.loadUI.SetLoadingText("Response empty from OpenAI.");
                        return null;
                    }
                    assistantMessage = responseObject.Choices[0].Message.Content;
                }
                catch (TaskCanceledException){
                    // Handle timeout
                    LogError("Request timed out.");
                    os.loadUI.SetLoadingText("Request timed out.");
                    return null;
                }
                catch (Exception ex){
                    // Handle other exceptions
                    LogError(ex.Message);
                    os.loadUI.SetLoadingText($"Error: {ex.Message}");
                    return null;
                }
            }
        }
        return assistantMessage;
    }

    private void LogError(string error)
    {
        try
        {
            using StreamWriter writer = new StreamWriter(logFilePath, true);
            writer.WriteLine($"{DateTime.Now}: {error}");
        }
        catch (Exception ex)
        {
            os.loadUI.SetLoadingText($"Failed to write to log file: {ex.Message}");
        }
    }
}
