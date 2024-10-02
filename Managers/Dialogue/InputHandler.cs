using UnityEngine;
using TMPro;
using Endless_it1;

public class InputHandler : MonoBehaviour{
    private ObjectStore os;


    //initializes game objects and variables for input handler
    void Start(){
        os = FindObjectOfType<ObjectStore>();
    }


    //saves and and displays user input then calls for a response
    public void HandleInput(string userInput){
        os.dm.inputField.text = "";
        os.messageUI.AddMessage(userInput, "green");
        os.gfc.Respond(userInput);
    }

    //gets called when the continue button is pressed
    //is responsible for triggering the next step in the story
    //closes conversation mode if needed
    public void ContinueStory(){
        ButtonRoutine();
        StartCoroutine(os.ir.WaitAndRespond(1f));
    }


    //get called when the yes button is pressed
    //triggers the next step in the story with the answer "yes"
    public void YesButton(){
        ButtonRoutine();
        StartCoroutine(os.ir.WaitAndRespondWithAnswer(1f, "yes"));
    }


    //get called when the no button is pressed
    //triggers the next step in the story with the answer "no"
    public void NoButton(){
        ButtonRoutine();
        StartCoroutine(os.ir.WaitAndRespondWithAnswer(1f, "no"));
    }

    
    //responsible for the common functionality of all buttons
    public void ButtonRoutine(){
        VoiceController.StopVoice();
        if (!ShouldShowChapter()){
            os.gsc.FadeOutAndClear();
        }
        else{
            StartCoroutine(os.messageUI.ClearMessages(1));
            os.gsc.FadeOutWithText("CHAPTER "+GameData.chapter);
        }
        if (os.gfc.convSteps.Contains(GameData.currentStep) || GameData.currentStep==2){
            os.gfc.conversationMode = false;
        }
        UILockManager.LockFromDialogue();
        VoiceImageLocker.resetLocks();
    }


    private bool ShouldShowChapter(){
        return os.gfc.endOfChapterSteps.Contains(GameData.currentStep);
    }
}
