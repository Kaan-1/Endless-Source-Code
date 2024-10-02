using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Endless_it1;
using Newtonsoft.Json;

public class ConnectorManager{
    public APIconnector apiConnector;
    public ImageAPI imgAPI;
    private readonly ObjectStore os;


    //sets up the connection and initializes the story with the default story together with the starting prompt
    public ConnectorManager(string defaultStory, ObjectStore os){
        this.os = os;
        apiConnector = new APIconnector(os);
        imgAPI = new ImageAPI(os);
        if (defaultStory!=" "){
            GameData.story ??= new ArrayList{
                new { role = "system", content = PromptStore.GetStartingPrompt() + defaultStory }
            };
        }
    }


    //takes in the latest response from the user and returns a response
    public async Task<string> StoryProgressor(string systemPrompt, string playerResponse=null){
        string apiResponse;
        if (playerResponse != null) GameData.story.Add(new {role = "user", content = playerResponse});
        ArrayList storyAndPrompt = new(GameData.story){
            new { role = "system", content = systemPrompt }
        };
        apiResponse = await apiConnector.getResponse(storyAndPrompt.ToArray());
        //storyJson = JsonConvert.SerializeObject(storyAndPrompt, Formatting.Indented);
        //Debug.Log(storyJson);
        if (apiResponse == null){
            os.gm.QuitStory();
            return null;
        }
        GameData.story.Add(new {role = "assistant", content = apiResponse});
        return apiResponse;
    }

    public async Task<CharacterResponse> ConversationResponder(string systemPrompt, string playerResponse = null){
        string apiResponse;
        if (playerResponse != null) GameData.story.Add(new {role = "user", content = playerResponse});
        ArrayList storyAndPrompt = new(GameData.story){
            new { role = "system", content = systemPrompt }
        };
        apiResponse = await apiConnector.getResponse(storyAndPrompt.ToArray(), new
                    {
                        type = "json_schema",
                        json_schema = new
                        {
                            name = "character_response",
                            strict = true,
                            schema = new
                            {
                                type = "object",
                                properties = new
                                {
                                    name = new { type = "string" },
                                    gender = new{ type = "string", _enum = new string[] { "man", "woman", "other" }},
                                    response = new{ type = "string" }
                                },
                                required = new string[] { "name", "gender", "response" },
                                additionalProperties = false
                            }
                        }
                    });
        if (apiResponse == null){
            os.gm.QuitStory();
            return null;
        }
        CharacterResponse response = JsonConvert.DeserializeObject<CharacterResponse>(apiResponse);
        GameData.story.Add(new {role = "assistant", content = response.Response});
        return response;
    }


    public async Task<string> getResponse(string pmt){
        ArrayList temp = new(){
            new { role = "system", content = pmt }
        };
        return await apiConnector.getResponse(temp.ToArray());
    }


    public async Task<Texture2D> GetImage(string pmt){
        return await imgAPI.getImage(pmt);
    }

    public async Task<string> GetNewStoryType(string availableStoryTypes){
        ArrayList storyAndPrompt = new(GameData.story){
            new { role = "system", content = "Considering the events up until now, which category would you place the above story in?\n" + availableStoryTypes + "Your response should include only your choice.\n Your choice:" }
        };
        string response = await apiConnector.getResponse(storyAndPrompt.ToArray());
        return TextParser.GetLastWord(response);
    }

    public async Task<string> GetNewStoryMood(){
        ArrayList storyAndPrompt = new(GameData.story){
            new { role = "system", content = "Considering the events up until now, would you categorize this story to be positive or negative? Your response should only include one word\n Your choice:" }
        };
        string response = await apiConnector.getResponse(storyAndPrompt.ToArray());
        return TextParser.GetLastWord(response);
    }

    public async Task<string> GetDuelImagePrompt(){
        ArrayList storyAndPrompt = new(GameData.story){
            new { role = "system", content = "Considering the events that happened till now in the story, summarize the aesthetics of the last duel situation in 2-3 sentences." }
        };
        return await apiConnector.getResponse(storyAndPrompt.ToArray());
    }

    public void AddConversationResponseFormatError(){
        GameData.story.Add(new {role = "system", content = "Your response was not in the correct format. Please reply again with the following format: PersonsName; Gender; “openingSentence”"});
    }
}