using System.Collections;
using Endless_it1;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DuelToggle : MonoBehaviour{

    public Toggle toggle;
    public Image toggleBackground;
    public RawImage toggleBall;

    void Start(){
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }


    public void OnToggleValueChanged(bool isOn)
    {
        Debug.Log("Toggle is " + (isOn ? "On" : "Off"));
        if (isOn){
            TurnOnToggle();
        }
        else{
            TurnOffToggle();
        }
    }

    private void TurnOnToggle(){
        GameData.duels = true;
        float brightenBy = 1.25f;
        ImageServices.MoveImage(toggleBall, 32, 0);
        ImageServices.ChangeImageBrightness(toggleBackground, brightenBy);
        ImageServices.ChangeImageBrightness(toggleBall, brightenBy);
    }

    private void TurnOffToggle(){
        GameData.duels = false;
        float darkenBy = 0.8f;
        ImageServices.MoveImage(toggleBall, -32, 0);
        ImageServices.ChangeImageBrightness(toggleBackground, darkenBy);
        ImageServices.ChangeImageBrightness(toggleBall, darkenBy);
    }

}