using System.Collections.Generic;
using Endless_it1;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour{
    public TMP_InputField inputField;
    public string defaultStory;
    public Button continueButton;
    public Button yesButton;
    public Button noButton;
    private ObjectStore os;
    

    //initializes the dialogue manager
    void Start(){
        StartRoutine();
    }


    //calls the textAPI to get the default story
    //initalizes the connector manager with the default story
    //calls the Respond() of the game flow controller to start the story since the default story is ready
    private async void StartRoutine(){
        os = FindObjectOfType<ObjectStore>();
        bool temp = GameData.story == null;
        if (GameData.story == null){
            var prompt = (GameData.storyType == "history") ? PromptStore.GetHistoricalDefaultStory() : PromptStore.GetDefaultStory();
            defaultStory = await os.cmService.getResponse(prompt);
            os.cmStory = new ConnectorManager(defaultStory, os);
        }
        else{
            GameData.currentStep++; //since we are continuing, take the next step in the story after the duel
            os.cmStory = new ConnectorManager("filler", os); //since the story was already initialized, it means that ConnectorManager will not try to override the current story with the default story
        }
        os.gfc.Respond();
    }


    //calls for response when the user presses enter, calls locker
    //calls the input handler to handle the input
    public void OnTextChange(string s){
        if (s.Contains("\n")){
            SoundManager.PlayEnterClick();
            UILockManager.LockFromDialogue();
            os.ih.HandleInput(s.Replace("\n", ""));
        }
    }


    //creates and saves a message in the conversation
    //calls the voice controller to vocalize the text
    //calls the image manager to update the background image
    public void ConversationMessageAdder(CharacterResponse response){
        if (response==null){
            Debug.Log("response was null mannn");
        }
        else{
            Debug.Log("response was not null mannn");
            Debug.Log(response);
            Debug.Log("name: "+response.Name);
            Debug.Log("response: "+response.Response);
        }
        os.messageUI.AddMessage(response.Name + ": " + response.Response);
        VoiceController.SynthesizeText(response.Response, response.Gender);
        if (!os.gfc.conversationMode){
            inputField.gameObject.SetActive(true);
            os.messageUI.BottomLeftAlignMessages();
            os.im.UpdateConversationImage(PromptStore.GetConversationImagePrompt(response.Name));
        }
    }


    //creates and saves a message in the progression
    //calls the voice controller to vocalize the text
    //calls the image manager to update the background image
    public void ProgressionMessageAdder(string response){
        inputField.gameObject.SetActive(false);
        os.messageUI.CenterAlignMessages();
        os.messageUI.AddMessage(response);
        VoiceController.SynthesizeText(response);
        os.im.UpdateProgressImage(response);
    }


    //get called when the voice and image are both ready
    //adds the message to the screen and unlocks the UI
    public void VoiceAndImageReadyAlert(){
        VoiceController.PlayVoice();
        if (os.gfc.conversationMode){
            UILockManager.UnlockFromDialogue();
        }
    }
}
