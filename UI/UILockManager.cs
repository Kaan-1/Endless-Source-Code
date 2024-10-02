using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UILockManager : MonoBehaviour{
    private static bool lockFromDialogue;
    private static bool lockFromOptions;
    public static List<Selectable> Selectables;

    void Start(){
        ResetLocks();
        InitializeSelectables();
    }

    public static void LockFromDialogue(){
        ChangeUIState(false);
        lockFromDialogue = true;
    }

    public static void LockFromOptions(){
        ChangeUIState(false);
        lockFromOptions = true;
    }

    public static void UnlockFromDialogue(){
        if (!lockFromOptions){
            ChangeUIState(true);
        }
        lockFromDialogue = false;
    }

    public static void UnlockFromOptions(){
        if (!lockFromDialogue){
            ChangeUIState(true);
        }
        lockFromOptions = false;
    }

    private static void ChangeUIState(bool state){
        if (Selectables == null){
            Debug.Log("Selectables is still null sadly");
        }
        foreach (Selectable selectable in Selectables){
            selectable.interactable = state;
        }
    }

    public static void ResetLocks(){
        lockFromDialogue = false;
        lockFromOptions = false;
    }

    private static void InitializeSelectables(){
        switch (SceneManager.GetActiveScene().name){
            case "Scene0":
                Selectables = Scene0Selectables.GetAllSelectables();
                break;
            case "StartingScene":
                Selectables = FindObjectOfType<ObjectStore>().sss.GetAllSelectables();
                break;
            case "Minigame":
                Selectables = FindObjectOfType<ObjectStore>().mg.GetAllSelectables();
                break;
        }
    }
}