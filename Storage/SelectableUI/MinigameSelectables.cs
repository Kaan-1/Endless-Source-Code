using System.Collections;
using System.Collections.Generic;
using Endless_it1;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinigameSelectables : MonoBehaviour{

    public Button optionsButton;
    private List<Selectable> selectables;

    public List<Selectable> GetAllSelectables(){
        selectables ??= new List<Selectable>{
            optionsButton
        };
        return selectables;
    }

}