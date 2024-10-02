using System.Collections;
using System.Collections.Generic;
using Endless_it1;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float damagePerSecond;
    private HealthBarPlayer healthBarPlayer;
    public static bool isCollidingWithNPC;
    private ObjectStore os;
    private Minigame mg;

    void Start()
    {
        os = FindObjectOfType<ObjectStore>();
        mg = FindObjectOfType<Minigame>();
        maxHealth = 400f;
        health = maxHealth;
        damagePerSecond = 15f;
        isCollidingWithNPC = false;
        healthBarPlayer = FindObjectOfType<HealthBarPlayer>();
    }

    void Update()
    {
        if (isCollidingWithNPC)
        {
            if (!GameData.paused){
                DecreaseHealth(damagePerSecond * Time.deltaTime, false);
            }
        }
    }

    public void DecreaseHealth(float damage, bool hit)
    {
        if (hit)
        {
            SoundManager.PlayDamage();
            os.da.PlayDamageAnimation();
        }
        health -= damage;
        healthBarPlayer.UpdateHealthBar();
        if (health <= 0)
        {
            Die();
        }
    }

    public void IncreaseHealth(float heal)
    {
        health += heal;
        healthBarPlayer.UpdateHealthBar();
    }

    protected void Die()
    {
        //handle dying
        Debug.Log("Player died");
        mg.NPCWon();
        mg.gameOver = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            if (!GameData.paused && !mg.gameOver){
                SoundManager.PlayBurningLooped();
            }
            isCollidingWithNPC = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            SoundManager.StopBurningLooped();
            isCollidingWithNPC = false;
        }
    }
}
