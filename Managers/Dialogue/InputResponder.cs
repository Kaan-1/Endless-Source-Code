using UnityEngine;
using System.Collections;
using Endless_it1;

public class InputResponder : MonoBehaviour{
    private ObjectStore os;


    void Start(){
        os = FindObjectOfType<ObjectStore>();
    }

    //waits for a certain amount of time before calling for a respond
    //shows the loading UI and clears the screen in the mean time
    public IEnumerator WaitAndRespond(float seconds){
        yield return new WaitForSeconds(seconds);
        if (ShouldActivateLoadingUI()){
            os.loadUI.ActivateLoadingUI(ShouldIncludeTip());
        }
        GameData.currentStep++;
        os.gsc.ResetImages();
        os.gfc.Respond();
    }


    //waits for a certain amount of time before calling for a respond with a string
    //shows the loading UI and clears the screen in the mean time
    //also adjusts the button availibilities
    public IEnumerator WaitAndRespondWithAnswer(float seconds, string s){
        yield return new WaitForSeconds(seconds);
        if (ShouldActivateLoadingUI()){
            os.loadUI.ActivateLoadingUI(ShouldIncludeTip());
        }
        GameData.currentStep++;
        os.gsc.ResetImages();
        os.dm.yesButton.gameObject.SetActive(false);
        os.dm.noButton.gameObject.SetActive(false);
        os.dm.continueButton.gameObject.SetActive(true);
        os.gfc.Respond(s);
    }


    //don't include tip if the next step is chapter transition or duel
    private bool ShouldIncludeTip(){
        if (GameData.currentStep == 0 || os.gfc.chapterSteps.Contains(GameData.currentStep)){
            return false;
        }
        return true;
    }

    private bool ShouldActivateLoadingUI(){
        if (os.gfc.progBeforeDuelSteps.Contains(GameData.currentStep)){
            return false;
        }
        return true;
    }
}