using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Endless_it1;

public class ImageManager : MonoBehaviour{
    public RawImage displayImage1;
    public RawImage displayImage2;
    private ObjectStore os;


    //setups the background images
    void Start(){
        os = FindObjectOfType<ObjectStore>();
        if (displayImage1!=null){
            AdjustImageBrightness(displayImage1);
        }
        if (displayImage2!=null){
            AdjustImageBrightness(displayImage2);
            setAlpha(displayImage2, 0);
        }
    }


    //sets background with a new image without animations
    public async void UpdateConversationImage(string pmt){
        setAlpha(displayImage2, 0);
        Texture2D texture = await os.cmService.GetImage(pmt);
        displayImage1.texture = texture;
        VoiceImageLocker.UnlockFromImage(os);
    }


    //sets background with a new image with animations
    public async void UpdateProgressImage(string pmt){
        var betterPrompt = await os.cmService.getResponse(PromptStore.GetConversationImagePromptsPrompt(pmt));
        var task1 = os.cmService.GetImage(betterPrompt);
        var task2 = os.cmService.GetImage(betterPrompt);
        var textures = await Task.WhenAll(task1, task2);
        displayImage1.texture = textures[0];
        displayImage2.texture = textures[1];
        VoiceImageLocker.UnlockFromImage(os);
    }


    //darkends the image
    public void AdjustImageBrightness(RawImage img){
        Color originalColor = img.color;
        Color newColor = originalColor * 0.30f;
        newColor.a = originalColor.a;
        img.color = newColor;
    }


    public void AnimateImages(){
        os.ia.AnimateImages(displayImage1, displayImage2);
    }


    private void setAlpha(RawImage img, float alpha){
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }


    public void ResetImages(){
        os.ia.ResetImages(displayImage1, displayImage2);
    }
}
