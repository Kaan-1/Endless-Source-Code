using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using Endless_it1;
using Unity.VisualScripting;

public class ImageServices : MonoBehaviour{

    private static ImageServices Instance;

    void Awake(){
        // Ensure that only one instance exists and don't destroy on load
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void MoveImage(RawImage image, float xDelta, float yDelta){
        RectTransform rt = image.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x + xDelta, rt.anchoredPosition.y + yDelta);
    }

    public static void MoveImage(Image image, float xDelta, float yDelta){
        RectTransform rt = image.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x + xDelta, rt.anchoredPosition.y + yDelta);
    }

    public static void ChangeImageBrightness(RawImage image, float brightness){
        Color originalColor = image.color;
        image.color = new Color(originalColor.r * brightness, originalColor.g * brightness, originalColor.b * brightness, originalColor.a);
    }

    public static void ChangeImageBrightness(Image image, float brightness){
        Color originalColor = image.color;
        image.color = new Color(originalColor.r * brightness, originalColor.g * brightness, originalColor.b * brightness, originalColor.a);
    }


    public static void FadeOut(Graphic graphic, float duration){
        Instance.StartCoroutine(FadeOutGraphicRoutine(graphic, duration));
        Instance.StartCoroutine(SetComponentStatus(graphic, false, duration));
    }


    public static void FadeIn(Graphic graphic, float duration){
        graphic.gameObject.SetActive(true);
        Instance.StartCoroutine(FadeInGraphicRoutine(graphic, duration));
    }


    public static void FadeOut(Component component, float duration){
        Graphic graphic = component.GetComponent<Graphic>();
        if (component.TryGetComponent<TMP_Text>(out var text)){
            Instance.StartCoroutine(FadeOutGraphicRoutine(text, duration));
        }
        else{
            Instance.StartCoroutine(FadeOutGraphicRoutine(graphic, duration));
            Instance.StartCoroutine(SetComponentStatus(component, false, duration));
        }
    }

    public static void FadeIn(Component component, float duration){
        component.gameObject.SetActive(true);
        if (component.TryGetComponent<TMP_Text>(out var text)){
            Instance.StartCoroutine(FadeInGraphicRoutine(text, duration));
        }
        else{
            Graphic graphic = component.GetComponent<Graphic>();
            Instance.StartCoroutine(FadeInGraphicRoutine(graphic, duration));
        }
    }



    private static IEnumerator FadeOutGraphicRoutine(Graphic graphic, float duration){
        TMP_Text ImageText = graphic.GetComponentInChildren<TMP_Text>();
        if (ImageText != null){
            Instance.StartCoroutine(FadeGraphic(ImageText, 0f, duration));
        }
        Instance.StartCoroutine(FadeGraphic(graphic, 0f, duration));
        yield return new WaitForSeconds(duration);
    }

    private static IEnumerator FadeInGraphicRoutine(Graphic graphic, float duration){
        TMP_Text ImageText = graphic.GetComponentInChildren<TMP_Text>();
        if (ImageText != null){
            Instance.StartCoroutine(FadeGraphic(ImageText, 1f, duration));
        }
        Instance.StartCoroutine(FadeGraphic(graphic, 1f, duration));
        yield return new WaitForSeconds(duration);
    }



    private static IEnumerator FadeGraphic(Graphic graphic, float targetAlpha, float duration){
        Color originalColor = graphic.color;
        float elapsedTime = 0f;
        while (elapsedTime < duration){
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(originalColor.a, targetAlpha, elapsedTime / duration);
            graphic.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        graphic.color = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha);
    }


    private static IEnumerator SetComponentStatus(Component component, bool status, float duration){
        yield return new WaitForSeconds(duration);
        component.gameObject.SetActive(status);
    }
}