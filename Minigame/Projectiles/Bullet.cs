using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private readonly int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            if (other.gameObject.TryGetComponent<NPCStats>(out var npcStats))
            {
                npcStats.DecreaseHealth(damage, true);
            }
            Destroy(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
}
