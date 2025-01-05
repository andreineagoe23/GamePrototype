using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    public GameObject player;  // Reference to the player GameObject
    public GameObject tutorialPopup;  // Reference to the tutorial popup panel
    public Transform textTransform;  // Reference to the TextMeshPro component or UI text inside the popup

    private bool isTutorialActive = false;  // Track if the tutorial is active
    public float distanceInFront = 2f;  // Distance the popup appears in front of the player
    public float heightOffset = 1.5f;  // Height of the popup above the ground

    public void Start()
    {
        // Initially show the tutorial for 7 seconds when the game starts
        ShowTutorial();
    }

    // Position the tutorial in front of the player with adjustable distance and height
    public void PositionTutorialInFrontOfPlayer()
    {
        if (player != null && tutorialPopup != null)
        {
            // Get the player’s position and facing direction
            Vector3 playerPosition = player.transform.position;
            Vector3 forward = player.transform.forward; // Direction the player is facing

            // Calculate the position in front of the player with the specified distance and height
            Vector3 popupPosition = playerPosition + forward * distanceInFront; // Horizontal distance
            popupPosition.y = playerPosition.y + heightOffset; // Adjust height

            // Position the popup at that location
            tutorialPopup.transform.position = popupPosition;

            // Rotate the popup to face the player (but keep text inside unaffected)
            tutorialPopup.transform.LookAt(player.transform.position);
            tutorialPopup.transform.rotation = Quaternion.Euler(0, tutorialPopup.transform.rotation.eulerAngles.y + 180, 0); // Apply 180-degree rotation

            // Keep the text inside the popup oriented properly
            if (textTransform != null)
            {
                textTransform.rotation = Quaternion.identity; // Reset the rotation of the text
            }
        }
    }

    // Show the tutorial for 7 seconds
    public void ShowTutorial()
    {
        if (tutorialPopup != null)
        {
            // Activate the tutorial
            tutorialPopup.SetActive(true);

            // Position it in front of the player
            PositionTutorialInFrontOfPlayer();

            // Set the tutorial as active
            isTutorialActive = true;

            // Hide the tutorial after 7 seconds
            Invoke("HideTutorial", 7f);
        }
    }

    // Hide the tutorial
    public void HideTutorial()
    {
        if (tutorialPopup != null)
        {
            tutorialPopup.SetActive(false);
            isTutorialActive = false;  // Set tutorial as inactive
        }
    }

    // Toggle the tutorial visibility when "T" key is pressed
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))  // Detect if the "T" key is pressed
        {
            if (isTutorialActive)
            {
                HideTutorial();  // If the tutorial is active, hide it
            }
            else
            {
                ShowTutorial();  // If the tutorial is not active, show it
            }
        }

        // If the tutorial is active, keep updating its position based on the player’s rotation
        if (isTutorialActive)
        {
            PositionTutorialInFrontOfPlayer();  // Continuously position the tutorial in front of the player
        }
    }
}
