using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Endless_it1;
using UnityEngine.SceneManagement;

public class IngameErrorsAPI : MonoBehaviour{
    public RawImage imageConnectionIcon;
    public TMP_Text imageConnectionText;
    public RawImage voiceConnectionIcon;
    public TMP_Text voiceConnectionText;


    //updates the availability icons for the image and voice API mid game
    public void UpdateAvailibilityIcons(){
        if (SceneManager.GetActiveScene().name == "Scene0"){
            if (GameData.imageAPIWorks){
                imageConnectionIcon.enabled = false;
                imageConnectionText.enabled = false;
            } else {
                imageConnectionIcon.enabled = true;
                imageConnectionText.enabled = true;
                imageConnectionIcon.color = Color.red;
            }
            if (GameData.voiceAPIWorks){
                voiceConnectionIcon.enabled = false;
                voiceConnectionText.enabled = false;
            } else {
                voiceConnectionIcon.enabled = true;
                voiceConnectionText.enabled = true;
                voiceConnectionIcon.color = Color.red;
            }
        }
    }

}