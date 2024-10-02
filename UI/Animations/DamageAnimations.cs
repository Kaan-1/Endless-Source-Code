using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class DamageAnimations : MonoBehaviour
{
    private readonly float fadeDuration = 0.2f;
    private readonly float moveDuration = 0.15f;
    private RawImage image;
    private RawImage flasherImage;
    private Color originalFlasherColor;
    private RectTransform rectTransform;

    public void PlayDamageAnimation()
    {
        StartCoroutine(MoveCoroutine(image));
        StartCoroutine(FlashToWhiteCoroutine(flasherImage));
    }

    public void SetImages(RawImage image, RawImage flasherImage)
    {
        this.image = image;
        this.flasherImage = flasherImage;
        originalFlasherColor = flasherImage.color;
        rectTransform = image.GetComponent<RectTransform>();
    }

    private IEnumerator FlashToWhiteCoroutine(RawImage image)
    {
        // Set the initial alpha to 128/256 (0.5)
        Color initialColor = originalFlasherColor;
        initialColor.a = 0.6f;
        image.color = initialColor;

        // Fade back to alpha 0 over 1 second
        float elapsedTime = 0f;
        Color targetColor = initialColor;
        targetColor.a = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            image.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }

        // Ensure image alpha is set to 0
        image.color = targetColor;
    }

    private IEnumerator MoveCoroutine(RawImage image)
    {
        RectTransform currentRectTransform = image.GetComponent<RectTransform>();
        Vector2 startPos = currentRectTransform.anchoredPosition;
        Vector2 leftPos = new Vector2(0, rectTransform.anchoredPosition.y);
        Vector2 rightPos = new Vector2((rectTransform.rect.width-Screen.width)/2, rectTransform.anchoredPosition.y);
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, leftPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(leftPos, rightPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
