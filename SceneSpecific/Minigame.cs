using System.Collections;
using UnityEngine;
using Endless_it1;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Minigame : MonoBehaviour{

    public TMP_Text bigScreenText;
    public RawImage bigScreenImage;
    public RawImage bigScreenFlasherImage;
    private ObjectStore os;
    public bool gameOver;

    void Start(){
        GameData.imageAPIWorks = true; //TEMPORARY, MUST BE DELETED
        GameData.paused = true;
        os = FindObjectOfType<ObjectStore>();
        os.loadUI.ActivateLoadingUI(false);
        Debug.Log("Start is indeed called");
        _ = GetImageAndStart();
    }

    private IEnumerator Counter(){
        yield return new WaitForSeconds(2);
        bigScreenText.text = "3";
        SoundManager.PlayCountdownClick();
        yield return new WaitForSeconds(0.5f);
        bigScreenText.text = "";
        yield return new WaitForSeconds(0.5f);
        bigScreenText.text = "2";
        SoundManager.PlayCountdownClick();
        yield return new WaitForSeconds(0.5f);
        bigScreenText.text = "";
        yield return new WaitForSeconds(0.5f);
        bigScreenText.text = "1";
        SoundManager.PlayCountdownClick();
        yield return new WaitForSeconds(0.5f);
        bigScreenText.text = "";
        yield return new WaitForSeconds(0.5f);
        bigScreenText.text = "GO!";
        SoundManager.PlayCountdownHorn();
        yield return new WaitForSeconds(1);
        GameData.paused = false;
        bigScreenText.text = "";
    }


    private async Task GetImageAndStart(){
        string imgPrompt = (GameData.story==null || GameData.story.Count==1)
            ? "A duel arena between two characters from roma."
            : await os.cmService.GetDuelImagePrompt();
        Debug.Log(imgPrompt);
        Texture2D img = await os.cmService.GetImage(imgPrompt);
        bigScreenImage.texture = img;
        os.im.AdjustImageBrightness(bigScreenImage);
        os.da.SetImages(bigScreenImage, bigScreenFlasherImage);
        os.loadUI.DeactivateLoadingUI();
        os.gsc.FadeIn();
        StartCoroutine(Counter());
    }

    public void PlayerWon(){
        if (gameOver){return;}
        PlayerStats.isCollidingWithNPC = false;
        SoundManager.StopBurningLooped();
        GameData.paused = true;
        os.gsc.DisableFadeImageChildren();
        os.gsc.FadeOutWithText("WIN");
        GameData.story.Add(new {role = "system", content = "The player won the duel."});
        StartCoroutine(WaitAndContinue());
    }

    public void NPCWon(){
        if (gameOver){return;}
        PlayerStats.isCollidingWithNPC = false;
        SoundManager.StopBurningLooped();
        GameData.paused = true;
        os.gsc.DisableFadeImageChildren();
        os.gsc.FadeOutWithText("LOSS");
        GameData.story.Add(new {role = "system", content = "The player lost the duel."});
        StartCoroutine(WaitAndContinue());
    }

    private IEnumerator WaitAndContinue(){
        yield return new WaitForSeconds(3);
        os.mm.StartFadeOut(5f);
        ImageServices.FadeOut(os.gsc.imageChildText, 5f);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Scene0");
    }
}