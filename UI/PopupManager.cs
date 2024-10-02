using UnityEngine;
using Endless_it1;
using UnityEngine.SceneManagement;

public class PopupManager : MonoBehaviour{

    private static GameObject LastPopup;
    private static bool Closable;
    private ObjectStore os;

    void Start(){
        os = FindObjectOfType<ObjectStore>();
    }

    public static void OpenPopup(GameObject popup, bool closable=true){
        popup.SetActive(true);
        LastPopup = popup;
        Closable = closable;
        GameServices.PauseGame();
    }

    public static void ClosePopup(){
        if(LastPopup != null && Closable){
            LastPopup.SetActive(false);
            GameServices.ResumeGame();
        }
    }

    //toggles the options popup when escape key is pressed
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameData.escToggleAllowed){
                if (IsPopupActiveAndClosable()){
                    ClosePopup();
                }
                else{
                    if (!IsPopupActive()){
                        os.o.OpenPopup();
                    }
                }
            }
        }
    }

    private bool IsPopupActiveAndClosable(){
        if (LastPopup == null){
            return false;
        }
        else{
            return LastPopup.activeSelf && Closable;
        }
    }

    private bool IsPopupActive(){
        return LastPopup != null && LastPopup.activeSelf;
    }

}