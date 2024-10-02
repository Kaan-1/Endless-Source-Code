using System.Collections;
using Endless_it1;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{
    public Button playButton;
    public Button quitButton;
    public GameObject selectionMenu;
    public TMP_Dropdown storyMoodDropdown;
    public TMP_Dropdown storyTypeDropdown;
    private ObjectStore os;


    //sets options panel listener based on the scene
    private void Start(){
        os = FindObjectOfType<ObjectStore>();
    }


    //gets called when play button gets pressed in StartingScene
    public void PlayGame(){
        StartCoroutine(PlayGameWithFades());
    }


    //gets called when the back button gets pressed in StartingScene
    public void BackButton(){
        StartCoroutine(BackButtonWithFades());
    }


    //gets called when the confirm button gets pressed in StartingScene
    public void Confirm(){
        StartCoroutine(StartGame());
    }


    //locks elements, fades out screen and music, resets the locks and starts the game
    private IEnumerator StartGame(){
        UILockManager.LockFromDialogue();
        SaveDropdownValues();
        os.gsc.FadeOut();
        os.mm.StartFadeOut(1f);
        yield return new WaitForSeconds(os.gsc.fadeDuration);
        UILockManager.ResetLocks();
        SceneManager.LoadScene("Scene0");
    }


    private void SaveDropdownValues(){
        GameData.storyMood = storyMoodDropdown.options[storyMoodDropdown.value].text;
        GameData.storyType = storyTypeDropdown.options[storyTypeDropdown.value].text;
    }


    public void QuitGame(){
        Application.Quit();
    }


    //gets called when quit button gets pressed in game scene
    //resets eveything and sends user back to StartingScene
    public void QuitStory(){
        GameData.escToggleAllowed = false;
        os.messageUI.ClearMessages();
        ResetStaticData();
        UILockManager.ResetLocks();
        VoiceImageLocker.resetLocks();
        SceneManager.LoadScene("StartingScene");
    }


    //fades out play and quit buttons
    //fades in back and confirm buttons and selection menu
    private IEnumerator PlayGameWithFades(){
        UILockManager.LockFromDialogue();
        os.gsc.FadeOutPlayAndQuitButtons();
        yield return new WaitForSeconds(1f);
        DeactivatePlayAndQuitButtons();
        UILockManager.UnlockFromDialogue();
        selectionMenu.SetActive(true);
        os.gsc.FadeSelectionMenu(true);
    }


    //even though the lock logic is implemented by UILockManager, since the play and quit buttons and
    //back and confirm buttons are on top of each other, they need to be activated and deactivated accordingly
    private void DeactivatePlayAndQuitButtons(){
        playButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }


    private void ActivatePlayAndQuitButtons(){
        playButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }


    //fades out back and confirm buttons and selection menu
    //fades in play and quit buttons
    private IEnumerator BackButtonWithFades(){
        UILockManager.LockFromDialogue();
        os.gsc.FadeSelectionMenu(false);
        yield return new WaitForSeconds(1f);
        selectionMenu.SetActive(false);
        UILockManager.UnlockFromDialogue();
        ActivatePlayAndQuitButtons();
        os.gsc.FadeInPlayAndQuitButtons();
    }


    private void ResetStaticData(){
        GameData.chapter = 1;
        GameData.story = null;
        GameData.currentStep = 0;
        GameData.paused = false;
        GameData.storyTypesInitialized=false;
        GameData.storyTypes = null;
    }
}
