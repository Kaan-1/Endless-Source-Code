using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using Endless_it1;
using Unity.VisualScripting;

public class StartingScene : MonoBehaviour{
    private ObjectStore os;
    public RawImage loadingIcon;
    public RawImage internetConnectionIcon;
    public RawImage textConnectionIcon;
    public RawImage imageConnectionIcon;
    public RawImage voiceConnectionIcon;
    public TMP_Text loadingText;
    private ConnectionChecker cc;
    public Toggle toggle;
    public Image toggleBackground;
    public RawImage toggleBall;


    //initialises objects, checks connections then fades in
    void Start(){
        os = FindObjectOfType<ObjectStore>();
        cc = gameObject.AddComponent<ConnectionChecker>();
        StartCoroutine(CheckConnectionsThenFadeIn());
    }


    //checks connections, updates the connections panel, then fades in
    private IEnumerator CheckConnectionsThenFadeIn(){
        yield return WaitForTask(cc.CheckConnections(UpdateLoadingText));
        UpdateConnectionPanel();
        loadingIcon.gameObject.SetActive(false);
        loadingText.gameObject.SetActive(false);
        if (!GameData.internetWorks || !GameData.textAPIWorks || !GameData.imageAPIWorks || !GameData.voiceAPIWorks){
            os.eh.ShowError();
        }
        MusicManager.PlayMusic();
        os.gsc.FadeIn();
        GameData.escToggleAllowed = true;
        yield return null;
    }


    private void UpdateLoadingText(string text){
        loadingText.text = text;
    }


    // Helper function to wait for a Task
    private static IEnumerator WaitForTask(Task task){
        while (!task.IsCompleted){
            yield return null;
        }
        if (task.IsFaulted){
            throw task.Exception;
        }
    }


    //updates the connection panel based on the connection status
    private void UpdateConnectionPanel(){
        internetConnectionIcon.color = GameData.internetWorks ? Color.green : Color.red;
        textConnectionIcon.color = GameData.textAPIWorks ? Color.green : Color.red;
        imageConnectionIcon.color = GameData.imageAPIWorks ? Color.green : Color.red;
        voiceConnectionIcon.color = GameData.voiceAPIWorks ? Color.green : Color.red;
    }
}
