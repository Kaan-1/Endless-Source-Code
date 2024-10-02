using UnityEngine;
using UnityEngine.UI;

public class HealthBarNPC : MonoBehaviour
{
    public Image healthBarForeground; // Assign this via the Inspector
    private NPCStats npcStats; // Reference to the NPC's stats

    void Start()
    {
        // Find the NPC in the scene
        npcStats = FindObjectOfType<NPCStats>(); // Adjust if you have multiple NPCs
    }

    public void UpdateHealthBar()
    {
        float healthPercent = npcStats.health / npcStats.maxHealth;
        RectTransform rt = healthBarForeground.rectTransform;
        rt.localScale = new Vector3(healthPercent, 1, 1);
    }
}
