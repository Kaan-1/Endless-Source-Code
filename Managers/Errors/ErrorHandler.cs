using Endless_it1;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorHandler : MonoBehaviour{
    public GameObject errorPanel;
    public TMP_Text errorText;
    public Button quitButton;
    public Button continueButton;
    private ObjectStore os;


    void Start(){
        os = FindObjectOfType<ObjectStore>();
    }


    //locks everthing and shows error message
    public void ShowError(){
        UILockManager.LockFromDialogue();
        if (!GameData.internetWorks){
            errorText.text = ErrorTexts.noInternet;
            quitButton.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(false);
        }
        else if (!GameData.textAPIWorks){
            errorText.text = ErrorTexts.noTextAPI;
            quitButton.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(false);
        }
        else if (!GameData.imageAPIWorks && !GameData.voiceAPIWorks){
            errorText.text = ErrorTexts.noImageAndVoiceAPI;
            quitButton.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(true);
        }
        else if (!GameData.imageAPIWorks){
            errorText.text = ErrorTexts.noImageAPI;
            quitButton.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(true);
        }
        else if (!GameData.voiceAPIWorks){
            errorText.text = ErrorTexts.noVoiceAPI;
            quitButton.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(true);
        }
        errorPanel.SetActive(true);
    }
    

    //gets called when the continue button in the error panel is pressed in the starting scene
    public void ContinueButton(){
        errorPanel.SetActive(false);
        UILockManager.UnlockFromDialogue();
    }

    public bool IsErrorPanelActive(){
        return errorPanel.activeSelf;
    }
}