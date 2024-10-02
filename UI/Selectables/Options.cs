using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Endless_it1;


public class Options : MonoBehaviour{

    public GameObject optionsPopup;

    public void OpenPopup(){
        PopupManager.OpenPopup(optionsPopup);
    }
}