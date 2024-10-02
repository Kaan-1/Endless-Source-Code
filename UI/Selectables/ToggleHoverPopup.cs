using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleHoverPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject popupText;  // Assign the popup text GameObject in the Inspector

    // When the mouse pointer enters the Toggle area
    public void OnPointerEnter(PointerEventData eventData)
    {
        popupText.SetActive(true); // Show the popup
    }

    // When the mouse pointer exits the Toggle area
    public void OnPointerExit(PointerEventData eventData)
    {
        popupText.SetActive(false); // Hide the popup
    }
}
