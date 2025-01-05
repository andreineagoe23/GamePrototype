using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    public GameObject player;
    public GameObject tutorialPopup;
    public Transform textTransform;

    private bool isTutorialActive = false;
    public float distanceInFront = 2f;
    public float heightOffset = 1.5f;

    public void Start()
    {
        ShowTutorial();
    }

    public void PositionTutorialInFrontOfPlayer()
    {
        if (player != null && tutorialPopup != null)
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 forward = player.transform.forward;

            // Calculate the position in front of the player
            Vector3 popupPosition = playerPosition + forward * distanceInFront;
            popupPosition.y = playerPosition.y + heightOffset;

            tutorialPopup.transform.position = popupPosition;

            // Make the tutorialPopup face the player with 180-degree rotation
            Vector3 directionToPlayer = player.transform.position - tutorialPopup.transform.position;
            directionToPlayer.y = 0; // Ignore the vertical axis
            tutorialPopup.transform.rotation = Quaternion.LookRotation(-directionToPlayer);
            tutorialPopup.transform.Rotate(0, 180, 0); // Add 180-degree rotation

            // Lock the textTransform to ensure it remains upright and facing the player correctly
            if (textTransform != null)
            {
                textTransform.LookAt(player.transform);
                textTransform.rotation = Quaternion.Euler(0, textTransform.rotation.eulerAngles.y + 180, 0); // Add 180-degree rotation
            }
        }
    }

    public void ShowTutorial()
    {
        if (tutorialPopup != null)
        {
            tutorialPopup.SetActive(true);
            PositionTutorialInFrontOfPlayer();
            isTutorialActive = true;
            Invoke("HideTutorial", 7f);
        }
    }

    public void HideTutorial()
    {
        if (tutorialPopup != null)
        {
            tutorialPopup.SetActive(false);
            isTutorialActive = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isTutorialActive)
            {
                HideTutorial();
            }
            else
            {
                ShowTutorial();
            }
        }

        if (isTutorialActive)
        {
            PositionTutorialInFrontOfPlayer();
        }
    }
}
