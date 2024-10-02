using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer : MonoBehaviour
{
    public Image healthBarForeground; // Assign this via the Inspector
    private PlayerStats playerStats; // Reference to the NPC's stats

    void Start()
    {
        // Find the NPC in the scene
        playerStats = FindObjectOfType<PlayerStats>(); // Adjust if you have multiple NPCs
    }

    public void UpdateHealthBar()
    {
        float healthPercent = playerStats.health / playerStats.maxHealth;
        RectTransform rt = healthBarForeground.rectTransform;
        rt.localScale = new Vector3(healthPercent, 1, 1);
    }
}
