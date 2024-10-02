using System.Collections;
using System.Collections.Generic;
using Endless_it1;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartingSceneSelectables : MonoBehaviour{

    public Button optionsButton;
    public Button playButton;
    public Button quitButton;
    public Button backButton;
    public Button confirmButton;
    public TMP_Dropdown storyMoodDropdown;
    public TMP_Dropdown storyTypeDropdown;
    public Toggle toggle; //duel toggle
    public Button creditsButton;
    private List<Selectable> selectables;

    public List<Selectable> GetAllSelectables(){
        selectables ??= new List<Selectable>{
            optionsButton,
            playButton,
            quitButton,
            backButton,
            confirmButton,
            storyMoodDropdown,
            storyTypeDropdown,
            toggle,
            creditsButton
        };
        return selectables;
    }

}