using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimations : MonoBehaviour
{

    private float animationDuration = 12f; // Duration of the animation
    private float transitionDuration = 5f;
    private Vector2 startPos;

    public IEnumerator MoveImageLeft(RawImage displayImage, RawImage targetImage)
    {
        displayImage.color = new Color(displayImage.color.r, displayImage.color.g, displayImage.color.b, 1f);
        RectTransform rectTransform = displayImage.GetComponent<RectTransform>();
        Vector2 startPos = new Vector2((rectTransform.rect.width-Screen.width)/2, rectTransform.anchoredPosition.y);
        Vector2 endPos = new Vector2((Screen.width-rectTransform.rect.width)/2, rectTransform.anchoredPosition.y);
        float elapsedTime = 0f;
        bool startedTransitioning = false;
        while (elapsedTime < animationDuration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            if ((animationDuration-elapsedTime <= transitionDuration) && !startedTransitioning)
            {
                startedTransitioning = true;
                StartCoroutine(Fade(displayImage, 1f, 0f));
                StartCoroutine(Fade(targetImage, 0f, 1f));
                StartCoroutine(MoveImageLeft(targetImage, displayImage));
            }
            yield return null;
        }

        rectTransform.anchoredPosition = startPos;
    }


    private IEnumerator Fade(RawImage img, float startAlpha, float targetAlpha){
        float elapsedTime = 0f;
        Color imageColor = img.color;
        while (elapsedTime < transitionDuration){
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / transitionDuration);
            img.color = new Color(imageColor.r, imageColor.g, imageColor.b, newAlpha);
            yield return null;
        }
        img.color = new Color(imageColor.r, imageColor.g, imageColor.b, targetAlpha);
    }


    public void AnimateImages(RawImage displayImage1, RawImage displayImage2)
    {
        RectTransform rectTransform = displayImage1.GetComponent<RectTransform>();
        startPos = new Vector2((rectTransform.rect.width-Screen.width)/2, rectTransform.anchoredPosition.y);
        StartCoroutine(MoveImageLeft(displayImage1, displayImage2));
    }


    public void ResetImages(RawImage displayImage1, RawImage displayImage2){
        StopAllCoroutines();
        //set display image 1 to startpos
        RectTransform rectTransform = displayImage1.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = startPos;
        //set display image 2 to startpos
        rectTransform = displayImage2.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = startPos;
        SetAlpha(displayImage1, 1f);
        SetAlpha(displayImage2, 0f);
    }

    private void SetAlpha(RawImage img, float alpha){
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
}
