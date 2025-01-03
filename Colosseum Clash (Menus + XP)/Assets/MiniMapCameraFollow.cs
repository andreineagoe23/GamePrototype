using UnityEngine;

public class MiniMapCameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float height = 50f; // Height above the player

    void LateUpdate()
    {
        if (player != null)
        {
            // Follow the player's X and Z position, keeping Y constant
            transform.position = new Vector3(player.position.x, height, player.position.z);
        }
    }
}
