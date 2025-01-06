using UnityEngine;

public class MinimapCameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPosition = player.position;
            newPosition.y = transform.position.y; // Maintain the camera's height
            transform.position = newPosition;
        }
    }
}
