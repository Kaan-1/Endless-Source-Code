using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogoScene : MonoBehaviour{
    public Image logo;
    public AudioSource iceCrack;


    void Start(){
        StartCoroutine(StartLogoScene());
    }


    //waits, flashes logo, waits, and then loads the starting scene
    private IEnumerator StartLogoScene(){
        yield return new WaitForSeconds(2f);
        iceCrack.Play();
        SetAlpha(logo, 1);
        logo.CrossFadeAlpha(0, 1f, false);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("StartingScene");
    }

    private void SetAlpha(Image img, float alpha){
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
}
