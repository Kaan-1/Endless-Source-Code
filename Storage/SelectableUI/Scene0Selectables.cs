using System.Collections;
using System.Collections.Generic;
using Endless_it1;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene0Selectables : MonoBehaviour{

    public TMP_InputField inputField;
    public Button continueButton;
    public Button yesButton;
    public Button noButton;
    public Button optionsButton;
    private List<Selectable> selectables;
    private static Scene0Selectables Instance;

    void Awake(){
        Instance = this;
    }

    public static List<Selectable> GetAllSelectables(){
        if (Instance==null){
            Debug.Log("Instance is null");
        }
        else if (Instance.selectables==null){
            Debug.Log("Instance.selectables is null");
        }
        Instance.selectables ??= new List<Selectable>{
            Instance.inputField,
            Instance.continueButton,
            Instance.yesButton,
            Instance.noButton,
            Instance.optionsButton
        };
        return Instance.selectables;
    }

}