using System.Collections;
using Endless_it1;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour{

    public GameObject creditsPopup;

    public void PopupCredits(){
        PopupManager.OpenPopup(creditsPopup);
    }

}