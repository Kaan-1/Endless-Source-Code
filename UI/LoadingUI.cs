using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Endless_it1;

public class LoadingUI : MonoBehaviour{

    public RawImage loadingImage;
    public TMP_Text loadingText;
    public GameObject tipBox;
    public RawImage tipBosBig;
    public RawImage tipBoxSmall;
    public TMP_Text tip;

    public void ActivateLoadingUI(bool withTip){
        loadingImage.gameObject.SetActive(true);
        loadingText.gameObject.SetActive(true);
        if (withTip){
            tip.text = TipController.GetTip();
            tipBox.SetActive(true);
        }
    }


    public void DeactivateLoadingUI(){
        loadingImage.gameObject.SetActive(false);
        loadingText.gameObject.SetActive(false);
        if (tipBox!=null){
            tipBox.SetActive(false);
        }
    }


    public void SetLoadingText(string text){
        loadingText.text = text;
    }

}