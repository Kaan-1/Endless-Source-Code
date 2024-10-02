using UnityEngine;

public class Ball : MonoBehaviour
{
    private readonly int damage = GameRules.GetNPCDamage();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent<PlayerStats>(out var playerStats))
            {
                playerStats.DecreaseHealth(damage, true);
            }
            Destroy(gameObject);
        }
        else if (!GameRules.GetBallsCollideWithBoundaries()){
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Horizontal Boundary"))
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }
        else if (other.gameObject.CompareTag("Vertical Boundary"))
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }
    }
}
