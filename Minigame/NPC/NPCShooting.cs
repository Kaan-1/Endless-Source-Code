using Endless_it1;
using UnityEngine;

public class NPCShooting : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform playerTransform; // Reference to the player's transform
    public Transform ballSpawnPoint;
    public float ballSpeed;
    private float shootingFreq; //between 0 and 1

    private void Start()
    {
        ballSpeed = 15f;
        shootingFreq = GameRules.GetNPCShootingFrequency();
        Invoke(nameof(Shoot), 1f*shootingFreq); // Start the shooting cycle
    }

    void Shoot()
    {
        float nextShootInterval;
        if (GameData.paused){
            // Schedule the next shot with a random interval between 1 and 2 seconds
            nextShootInterval = Random.Range(1f*shootingFreq, 2f*shootingFreq);
            Invoke(nameof(Shoot), nextShootInterval);
            return;
        }
        // Calculate the direction from the NPC to the player
        Vector2 direction = (playerTransform.position - ballSpawnPoint.position);
        direction = direction.normalized;

        // Create the bullet
        GameObject bullet = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);

        // Set the bullet's rotation to face the direction of travel
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Add velocity to the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * ballSpeed;

        // Optionally, destroy the bullet after a certain time
        Destroy(bullet, 5f);

        // Schedule the next shot with a random interval between 1 and 2 seconds
        nextShootInterval = Random.Range(1f*shootingFreq, 2f*shootingFreq);
        Invoke(nameof(Shoot), nextShootInterval);
    }
}
