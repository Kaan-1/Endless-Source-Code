using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStats : MonoBehaviour
{
    public float health;
    public float maxHealth;
    private HealthBarNPC healthBarNPC;
    private ObjectStore os;
    private Minigame mg;

    void Start()
    {
        maxHealth = GameRules.GetNPCHealth();
        health = maxHealth;
        healthBarNPC = FindObjectOfType<HealthBarNPC>();
        os = FindObjectOfType<ObjectStore>();
        mg = FindObjectOfType<Minigame>();
    }

    public void DecreaseHealth(float damage, bool hit)
    {
        if (hit){
            SoundManager.PlayHit();
        }
        health -= damage;
        healthBarNPC.UpdateHealthBar();
        if (health <= 0)
        {
            Die();
        }
    }

    public void IncreaseHealth(float heal)
    {
        health += heal;
        healthBarNPC.UpdateHealthBar();
    }

    protected void Die()
    {
        //handle dying
        Debug.Log("NPC died");
        mg.PlayerWon();
        mg.gameOver = true;
    }
}

