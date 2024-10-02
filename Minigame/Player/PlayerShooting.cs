using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using Endless_it1;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public Button optionsButton;
    public float bulletSpeed = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            if (!IsPointerOverUIElement(optionsButton.gameObject))
            {
                if (!GameData.paused){
                    Shoot();
                }
            }
        }
    }

    void Shoot()
    {
        // Calculate the direction from the player to the mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 direction = (mousePos - bulletSpawnPoint.position).normalized;
        Vector2 direction = mousePos - bulletSpawnPoint.position;
        direction = direction.normalized;

        // Create the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        // Add velocity to the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;

        // Optionally, destroy the bullet after a certain time
        Destroy(bullet, 2f);
    }

    private bool IsPointerOverUIElement(GameObject uiElement)
    {
        // Create a pointer event data to hold the mouse position
        PointerEventData pointerEventData = new(EventSystem.current)
        {
            position = Input.mousePosition
        };

        // Raycast to find UI elements under the pointer
        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(pointerEventData, results);

        // Check if the specified UI element is in the results
        foreach (RaycastResult result in results)
        {
            if (result.gameObject == uiElement)
            {
                return true; // Pointer is over the specified UI element
            }
        }

        return false; // Pointer is not over the specified UI element
    }
}