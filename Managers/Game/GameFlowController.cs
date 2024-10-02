using System.Collections;
using System.Collections.Generic;
using Endless_it1;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameFlowController : MonoBehaviour{
    public bool conversationMode; //story is either in conversation mode or progression mode
    private bool askQuestion;
    public readonly List<int> startingChapterStep = new(){0};
    public readonly List<int> defaultProgStep = new(){1};
    public readonly List<int> convAfterStartStep = new(){2};
    private readonly List<int> progNewSteps = new(){3,10};
    public readonly List<int> convSteps = new(){4,9,11,16,18};
    private readonly List<int> progYesOrNoSteps = new(){5,12,19};
    public readonly List<int> endOfChapterSteps = new(){6,13};
    public readonly List<int> chapterSteps = new(){7,14};
    private readonly List<int> progAfterChapSteps = new(){8};
    private readonly List<int> progAfterChapStepsWithNew = new(){15};
    private readonly List<int> progSteps = new(){17};
    public readonly List<int> endOfStoryStep = new(){20};
    private readonly List<int> endOfGameStep = new(){21};
    public List<int> progBeforeDuelSteps = new(){};
    private List<int> progAfterDuelSteps = new(){};
    public List<int> duelSteps = new(){};
    private List<List<int>> stepsList;
    private ObjectStore os;
    private System.Random random;


    void Awake(){
        os = FindObjectOfType<ObjectStore>();
        FindDm();
        askQuestion = true;
        random = new();
        stepsList = new(){startingChapterStep, defaultProgStep, convAfterStartStep, progNewSteps, convSteps, progYesOrNoSteps, endOfChapterSteps, chapterSteps, progAfterChapSteps, progAfterChapStepsWithNew, progSteps, endOfStoryStep, endOfGameStep, progBeforeDuelSteps, progAfterDuelSteps, duelSteps};
        if (GameData.duels) {DuelsFlowOn();}
    }


    //responds to the user based on the chapter in conversation
    //is responsible for controlling the flow of the game
    //processes the current step when called
    public async void Respond(string s=""){
        os.ie.UpdateAvailibilityIcons();
        Debug.Log("Current step: "+GameData.currentStep);
        string response = "";
        if (conversationMode){
            Debug.Log("Asking question: "+askQuestion);
            CharacterResponse structuredResponse = (askQuestion)? await os.cmStory.ConversationResponder(PromptStore.GetDuringConversationPromptWithQuestion(), s) : await os.cmStory.ConversationResponder(PromptStore.GetDuringConversationPrompt(), s);
            askQuestion = random.Next(2) == 1;
            Debug.Log("New askQuestion variable: "+askQuestion);
            os.dm.ConversationMessageAdder(structuredResponse);
        }
        else if(startingChapterStep.Contains(GameData.currentStep)){
            StartCoroutine(os.ir.WaitAndRespond(3f));
        }
        else if (defaultProgStep.Contains(GameData.currentStep)){
            os.dm.ProgressionMessageAdder(os.dm.defaultStory);
            os.dm.continueButton.interactable = true;
        }
        else if (convAfterStartStep.Contains(GameData.currentStep)){
            CharacterResponse structuredResponse = await os.cmStory.ConversationResponder(PromptStore.GetConversationAfterStartingPrompt());
            os.dm.ConversationMessageAdder(structuredResponse);
            conversationMode = true;
        }
        else if (endOfStoryStep.Contains(GameData.currentStep)){
            response = await os.cmStory.StoryProgressor(PromptStore.GetEndOfStory(), s);
            os.dm.ProgressionMessageAdder(response);
        }
        else if (endOfChapterSteps.Contains(GameData.currentStep)){
            GameData.chapter++;
            response = await os.cmStory.StoryProgressor(PromptStore.GetEndOfChapter(), s);
            os.dm.ProgressionMessageAdder(response);
        }
        else if (progAfterChapSteps.Contains(GameData.currentStep)){
            response = await os.cmStory.StoryProgressor(PromptStore.GetProgressionAfterChapter());
            os.dm.ProgressionMessageAdder(response);
        }
        else if (progAfterChapStepsWithNew.Contains(GameData.currentStep)){
            response = await os.cmStory.StoryProgressor(PromptStore.GetProgressionAfterChapterWithNew());
            os.dm.ProgressionMessageAdder(response);
        }
        else if (progSteps.Contains(GameData.currentStep)){
            response = await os.cmStory.StoryProgressor(PromptStore.GetProgressionAfterConversation());
            os.dm.ProgressionMessageAdder(response);
        }
        else if (progNewSteps.Contains(GameData.currentStep)){
            response = await os.cmStory.StoryProgressor(PromptStore.GetProgressionAfterConversationWithNew());
            os.dm.ProgressionMessageAdder(response);
        }
        else if (convSteps.Contains(GameData.currentStep)){
            CharacterResponse structuredResponse = await os.cmStory.ConversationResponder(PromptStore.GetConversationAfterProgression());
            os.dm.ConversationMessageAdder(structuredResponse);
            conversationMode = true;
        }
        else if (chapterSteps.Contains(GameData.currentStep)){
            os.loadUI.DeactivateLoadingUI();
            os.mm.SetCurrentAudioSource();
            StartCoroutine(os.ir.WaitAndRespond(3f));
        }
        else if (progYesOrNoSteps.Contains(GameData.currentStep)){
            os.dm.continueButton.gameObject.SetActive(false);
            os.dm.yesButton.gameObject.SetActive(true);
            os.dm.noButton.gameObject.SetActive(true);
            response = await os.cmStory.StoryProgressor(PromptStore.GetProgressionYesOrNo());
            os.dm.ProgressionMessageAdder(response);
        }
        else if (endOfGameStep.Contains(GameData.currentStep)){
            os.loadUI.DeactivateLoadingUI();
            os.gsc.FadeOutWithText("THE END");
            StartCoroutine(QuitGame());
        }
        else if (progBeforeDuelSteps.Contains(GameData.currentStep)){
            response = await os.cmStory.StoryProgressor(PromptStore.GetProgressionBeforeDuel());
            os.dm.ProgressionMessageAdder(response);
        }
        else if (progAfterDuelSteps.Contains(GameData.currentStep)){
            os.gsc.ClearText();
            os.loadUI.ActivateLoadingUI(true);
            response = await os.cmStory.StoryProgressor(PromptStore.GetProgressionAfterDuel());
            os.dm.ProgressionMessageAdder(response);
        }
        else if (duelSteps.Contains(GameData.currentStep)){
            StartCoroutine(DuelRoutine());
        }
        if (!conversationMode){
            UILockManager.UnlockFromDialogue();
        } 
    }


    //waits for 10 seconds and then quits the game
    public IEnumerator QuitGame(){
        yield return new WaitForSeconds(5f);
        os.mm.StartFadeOut(5f);
        ImageServices.FadeOut(os.gsc.imageChildText, 5f);
        yield return new WaitForSeconds(5);
        os.gm.QuitStory();
    }

    
    //finds the dialogue manager if it is not already found
    private void FindDm(){
        if (os.dm == null){
            os.dm = FindObjectOfType<DialogueManager>();
        }
    }

    private void DuelsFlowOn(){
        progNewSteps.Clear();
        progSteps.Clear();
        foreach (List<int> steps in stepsList){
            for (int i = 0; i < steps.Count; i++) {
                if (steps[i] > 3) {
                    steps[i] += 2;
                    if (steps[i] > 12){
                        steps[i] += 2;
                        if (steps[i] > 21){
                            steps[i] += 2;
                        } 
                    }
                }
            }
        }
        progBeforeDuelSteps = new List<int>(){3,12,21};
        duelSteps = new List<int>(){4,13,22};
        progAfterDuelSteps = new List<int>(){5,14,23};
    }

    private IEnumerator DuelRoutine(){
        os.mm.StartFadeOut(2f);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Minigame");
    }
}
