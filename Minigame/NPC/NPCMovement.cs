using Endless_it1;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed;
    public float blitzForce;
    public float blitzFreq; //between 0 and 1
    public float blitzDuration = 1f; // How long the blitz lasts
    private Rigidbody2D rb;
    private bool isBlitzing = false;
    private float blitzEndTime;

    void Start()
    {
        moveSpeed = GameRules.GetNPCSpeed();
        rb = GetComponent<Rigidbody2D>();
        blitzForce = GameRules.GetBlitzForce();
        blitzFreq = GameRules.GetBlitzFreq();
        ScheduleBlitz();
    }

    void FixedUpdate()
    {
        if (!GameData.paused)
        {
            if (isBlitzing)
            {
                if (Time.time >= blitzEndTime)
                {
                    isBlitzing = false;
                }
            }

            // Calculate direction towards the player
            Vector2 direction = (player.position - transform.position).normalized;

            if (!isBlitzing)
            {
                // Normal movement
                rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * direction);
            }
        }
    }

    void ScheduleBlitz()
    {
        float nextBlitzInterval = Random.Range(5f*blitzFreq, 10f*blitzFreq);
        Invoke(nameof(Blitz), nextBlitzInterval);
    }

    void Blitz()
    {
        if (!GameData.paused){
            // Calculate the direction from the NPC to the player
            Vector2 direction = (player.position - transform.position);
            direction = direction.normalized;

            // Apply the blitz force
            rb.AddForce(direction * blitzForce, ForceMode2D.Impulse);

            // Set blitzing state
            isBlitzing = true;
            blitzEndTime = Time.time + blitzDuration;
        }

        // Schedule the next blitz
        ScheduleBlitz();
    }
}
