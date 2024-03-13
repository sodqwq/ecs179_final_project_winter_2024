using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform player; // Set this to the player's transform in the Inspector
    public float smoothSpeed = 0.125f; // Adjust this value to change how quickly the camera follows
    public Vector2 offset; // Only X and Y offset

    void LateUpdate()
    {
        if (player != null) // Check if the player reference is not null
        {
            Vector3 desiredPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
        else
        {
            // Optionally output a warning to the console if the player reference is lost
            Debug.LogWarning("Player transform is null.");
        }
    }

}
