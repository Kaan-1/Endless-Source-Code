using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using Endless_it1;
using Unity.VisualScripting;


public class GameScreenController : MonoBehaviour{
    public Image fadeImage;
    public float fadeDuration = 1.0f;
    private GameObject fadeImageParent;
    public TMP_Text imageChildText;
    private ObjectStore os;


    //initializes the text under the fade image and other variables
    private void Awake(){
        os = FindObjectOfType<ObjectStore>();
        if (fadeImage != null){
            fadeImage.gameObject.SetActive(true);
            fadeImageParent = fadeImage.transform.parent.gameObject;
            imageChildText = fadeImage.GetComponentInChildren<TMP_Text>();
        }
    }


    //fades screen to transparent
    public void FadeIn(){
        ImageServices.FadeOut(fadeImage, fadeDuration);
        AdjustEscToggleAllowed();
    }


    //fades screen to black
    public void FadeOut(){
        GameData.escToggleAllowed = false;
        fadeImageParent.SetActive(true);
        ImageServices.FadeIn(fadeImage, fadeDuration);
    }


    //fades screen to black and clears the screen at the end
    public void FadeOutAndClear(){
        imageChildText.text = "";
        StartCoroutine(os.messageUI.ClearMessages(1));
        FadeOut();
    }


    //fades screen to black with a text written on it
    public void FadeOutWithText(string text){
        imageChildText.text = text;
        imageChildText.gameObject.SetActive(true);
        FadeOut();
    }


    //gets called when the voice starts speaking and the image is ready
    //fades in screen if not already transparent
    public void VoiceAndImageReadyAlert(){
        if (fadeImage.color.a == 1f){
            FadeIn();
        }
    }


    public void ResetImages(){
        os.im.ResetImages();
    }


    public void FadeOutPlayAndQuitButtons(){
        ImageServices.FadeOut(os.gm.playButton, 1f);
        ImageServices.FadeOut(os.gm.quitButton, 1f);
    }

    public void FadeInPlayAndQuitButtons(){
        ImageServices.FadeIn(os.gm.playButton, 1f);
        ImageServices.FadeIn(os.gm.quitButton, 1f);
    }


    //fades the selection menu in or out
    public void FadeSelectionMenu(bool fadeIn){
        foreach (Transform child in os.gm.selectionMenu.transform){
            if (child.TryGetComponent<Button>(out var button)){
                if (fadeIn) ImageServices.FadeIn(child, 1f); else ImageServices.FadeOut(child, 1f);
            }
            else{
                foreach (Transform grandChild in child.transform){
                    if (fadeIn) ImageServices.FadeIn(grandChild, 1f); else ImageServices.FadeOut(grandChild, 1f);
                }
            }
        }
    }

    public void DisableFadeImageChildren(){
        foreach (Transform child in fadeImage.transform){
            child.gameObject.SetActive(false);
        }
    }

    public void ClearText(){
        imageChildText.text = "";
    }

    private void AdjustEscToggleAllowed(){
        if (os.eh != null){
            if (!os.eh.IsErrorPanelActive()){
                GameData.escToggleAllowed = true;
            }
        }
        else{
            GameData.escToggleAllowed = true;
        }
    }
}
