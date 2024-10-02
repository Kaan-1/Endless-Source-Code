using System.Collections;
using TMPro;
using UnityEngine;
using Endless_it1;

public class MessageUI : MonoBehaviour{

    public TMP_Text dialogueBoxText;

    public void AddMessage(string message, string color="white"){
        string coloredMessage;
        if (color.ToLower() == "green") {
            coloredMessage = "<color=#00FF00>" + message + "</color>";
        }
        else{
            coloredMessage = "<color=#FFFFFF>" + message + "</color>";
        }
        dialogueBoxText.text = dialogueBoxText.text + "\n" + coloredMessage;
    }

    public void ClearMessages(){
        dialogueBoxText.text = "";
    }

    public void CenterAlignMessages(){
        dialogueBoxText.alignment = TextAlignmentOptions.Center;
    }

    public void BottomLeftAlignMessages(){
        dialogueBoxText.alignment = TextAlignmentOptions.BottomLeft;
    }


    public IEnumerator ClearMessages(float delay){
        yield return new WaitForSeconds(delay);
        ClearMessages();
    }
}